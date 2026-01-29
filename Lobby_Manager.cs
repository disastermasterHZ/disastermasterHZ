using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LobbyManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text playerListText;
    [SerializeField] private Button startGameButton;
    [SerializeField] private TMP_Text lobbyIdText;
    [SerializeField] private Button readyButton;

    private string lobbyId;
    public static readonly Dictionary<string, List<ulong>> activeLobbies = new Dictionary<string, List<ulong>>();

    // Ready system
    private Dictionary<ulong, bool> playerReadyStates = new Dictionary<ulong, bool>();

    private void Start()
    {
        StartCoroutine(WaitForNetworkManagerAndInitialize());
    }


    private IEnumerator WaitForNetworkManagerAndInitialize()
    {
        while (NetworkManager.Singleton == null)
        {
            yield return null;
        }

        if (!ValidateUIReferences()) yield break;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        startGameButton.gameObject.SetActive(false); // Always hide here
        readyButton.gameObject.SetActive(true);
        readyButton.onClick.AddListener(OnReadyClicked);

        // Null check for player_manager
        var pm = FindFirstObjectByType<player_manager>();
        if (pm != null)
        {
            pm.LobbyActive = true;
        }
        else
        {
            Debug.LogError("LobbyManager: Could not find any player_manager component in the scene!");
        }

        UpdatePlayerList();

        if (NetworkManager.Singleton.SceneManager != null)
        {
            NetworkManager.Singleton.SceneManager.OnSceneEvent += OnSceneEvent;
        }

        startGameButton.onClick.AddListener(StartGame);
    }

    private bool ValidateUIReferences()
    {
        if (playerListText && startGameButton && lobbyIdText && readyButton)
            return true;

        Debug.LogError("UI references are not assigned to LobbyManager!");
        return false;
    }

    private void GenerateLobbyId()
    {
        if (!NetworkManager.Singleton.IsHost) return;

        // Generate a unique lobby ID
        lobbyId = System.Guid.NewGuid().ToString("N").Substring(0, 6);
        LobbyManager.activeLobbies[lobbyId] = new List<ulong>();
        lobbyIdText.text = $"Lobby ID: {lobbyId}";

        Debug.Log($"Lobby created with ID: {lobbyId}");
    }

    private void AddPlayerToLobby(ulong playerId)
    {
        if (!NetworkManager.Singleton.IsHost || string.IsNullOrEmpty(lobbyId)) return;

        // Ensure the lobby ID exists in the dictionary
        if (!activeLobbies.ContainsKey(lobbyId))
        {
            Debug.LogWarning($"Lobby ID '{lobbyId}' does not exist in the dictionary. Creating a new entry.");
            activeLobbies[lobbyId] = new List<ulong>();
        }

        if (!activeLobbies[lobbyId].Contains(playerId))
        {
            activeLobbies[lobbyId].Add(playerId);
            playerReadyStates[playerId] = false; // Not ready by default
            UpdatePlayerList();
        }
    }

    private void UpdatePlayerList()
    {
        if (playerListText == null || string.IsNullOrEmpty(lobbyId)) return;

        // Ensure the lobby ID exists in the dictionary
        if (!activeLobbies.ContainsKey(lobbyId))
        {
            Debug.LogWarning($"Lobby ID '{lobbyId}' does not exist in the dictionary. Cannot update player list.");
            return;
        }

        var playerList = activeLobbies[lobbyId];
        playerListText.text = "Players in Lobby:\n";

        foreach (var playerId in playerList)
        {
            string playerName = $"Player {playerId}";
            string readyStatus = playerReadyStates.ContainsKey(playerId) && playerReadyStates[playerId] ? " (Ready)" : " (Not Ready)";
            playerListText.text += $"{playerName}{readyStatus}\n";
        }
    }

    public void UpdatePlayerListText(string playerList)
    {
        if (playerListText != null)
        {
            playerListText.text = playerList;
        }
    }

    // Called when local player clicks ready
    private void OnReadyClicked()
    {
        SetReadyServerRpc(NetworkManager.Singleton.LocalClientId);
        readyButton.interactable = false; // Prevent spamming
    }

    [Rpc(SendTo.Server)]
    private void SetReadyServerRpc(ulong playerId)
    {
        if (!playerReadyStates.ContainsKey(playerId))
            playerReadyStates[playerId] = false;

        playerReadyStates[playerId] = true;
        UpdatePlayerListClientRpc();

        // Only the host checks and starts the game
        if (NetworkManager.Singleton.IsHost && AllPlayersReady())
        {
            StartGame();
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void UpdatePlayerListClientRpc()
    {
        UpdatePlayerList();
    }

    private bool AllPlayersReady()
    {
        if (string.IsNullOrEmpty(lobbyId) || !activeLobbies.ContainsKey(lobbyId))
            return false;

        var playerList = activeLobbies[lobbyId];
        foreach (var playerId in playerList)
        {
            if (!playerReadyStates.ContainsKey(playerId) || !playerReadyStates[playerId])
                return false;
        }
        return true;
    }

    public void StartGame()
    {
        if (!NetworkManager.Singleton.IsHost)
        {
            Debug.LogError("Only the host can start the game.");
            return;
        }

        Debug.Log("All players ready! Loading Store Game scene...");
        NetworkManager.Singleton.SceneManager.LoadScene("Store Game", LoadSceneMode.Single);
    }

    private void OnSceneEvent(SceneEvent sceneEvent)
    {
    if (sceneEvent.SceneEventType != SceneEventType.LoadComplete)
    {
        Debug.Log($"Scene loaded: {sceneEvent.SceneName}");
    }
}

private void OnDestroy()
{
    if (NetworkManager.Singleton != null && NetworkManager.Singleton.SceneManager != null)
    {
        NetworkManager.Singleton.SceneManager.OnSceneEvent -= OnSceneEvent;
    }

    if (NetworkManager.Singleton.IsHost && !string.IsNullOrEmpty(lobbyId))
    {
        activeLobbies.Remove(lobbyId);
    }
}

public void HostSetup()
{
    if (NetworkManager.Singleton.IsHost)
    {
        GenerateLobbyId();
        AddPlayerToLobby(NetworkManager.Singleton.LocalClientId);
        startGameButton.gameObject.SetActive(true); // Enable Start button for host
        Debug.Log("HostSetup: Start button enabled for host.");
    }
}
}