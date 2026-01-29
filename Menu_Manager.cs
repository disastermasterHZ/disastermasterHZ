using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;
using System.Collections;

public class MainMenu : NetworkBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_InputField playerNameInputField;
    [SerializeField] private TMP_InputField lobbyIdInputField;
    [SerializeField] private Button hostButton;
    [SerializeField] private Button joinButton;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject lobbyPanel;
    [SerializeField] private GameObject lobbyManager; // Drag your Lobby Manager here in the Inspector

    public static class PlayerData
    {
        public static string PlayerName = "Player";
    }

    private void Start()
    {
        if (!ValidateUIReferences()) return;

        hostButton.onClick.AddListener(OnHostButtonClicked);
        joinButton.onClick.AddListener(OnJoinButtonClicked);
        mainMenuPanel.SetActive(true);
        lobbyPanel.SetActive(false);

        if (playerNameInputField != null)
        {
            playerNameInputField.text = GameManager.Instance != null ? GameManager.Instance.PlayerName : PlayerData.PlayerName;
            SavePlayerName(playerNameInputField.text);
            playerNameInputField.onEndEdit.AddListener(SavePlayerName);
        }
    }

    private void SavePlayerName(string name)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.PlayerName = name.Trim();
                Debug.Log($"Player name saved: {GameManager.Instance.PlayerName}");
            }
            else
            {
                Debug.LogError("GameManager instance is missing. Cannot save player name.");
            }
        }
        else
        {
            Debug.LogWarning("Player name is empty or invalid. Please enter a valid name.");
        }
    }

    private bool ValidateUIReferences()
    {
        bool isValid = true;
        if (!playerNameInputField)
        {
            Debug.LogError("Player Name Input Field is not assigned! Please assign it in the Inspector.");
            isValid = false;
        }
        if (!lobbyIdInputField)
        {
            Debug.LogError("Lobby ID Input Field is not assigned! Please assign it in the Inspector.");
            isValid = false;
        }
        if (!hostButton)
        {
            Debug.LogError("Host Button is not assigned! Please assign it in the Inspector.");
            isValid = false;
        }
        if (!joinButton)
        {
            Debug.LogError("Join Button is not assigned! Please assign it in the Inspector.");
            isValid = false;
        }
        if (!isValid)
        {
            Debug.LogError("UI references are not assigned to MainMenu! Check the Inspector.");
        }
        return isValid;
    }

    public void OnHostButtonClicked()
    {
        if (NetworkManager.Singleton.IsHost)
        {
            ShowLobbyPanel();
            Debug.LogWarning("Host is already running. No need to start it again.");
            return;
        }

        Debug.Log("Attempting to start host...");
        if (NetworkManager.Singleton.StartHost())
        {
            ShowLobbyPanel();
            Debug.Log("Host started successfully.");
            
        }
        else
        {
            StartCoroutine(RestartHost());
            Debug.LogError("Failed to start host.");
        }
    }

    private IEnumerator RestartHost()
    {
        var nm = NetworkManager.Singleton;
        nm.Shutdown();
        yield return new WaitWhile(() => nm.IsServer || nm.IsClient);
        Debug.Log("Host shutdown complete. Restarting host...");
    }

    public void OnJoinButtonClicked()
    {
        string lobbyId = lobbyIdInputField.text.Trim();

        if (string.IsNullOrEmpty(lobbyId))
        {
            Debug.LogError("Please enter a lobby ID.");
            return;
        }

        Debug.Log($"Attempting to join lobby with ID: {lobbyId}...");

        if (NetworkManager.Singleton.IsServer || NetworkManager.Singleton.IsHost)
        {
            Debug.LogWarning("NetworkManager is already running as a host or server. Shutting it down before starting client...");
            NetworkManager.Singleton.Shutdown();
        }

        StartCoroutine(WaitForShutdownAndStartClient());
    }

    private IEnumerator WaitForShutdownAndStartClient()
    {
        yield return new WaitWhile(() => NetworkManager.Singleton.IsServer || NetworkManager.Singleton.IsClient);

        if (NetworkManager.Singleton.StartClient())
        {
            Debug.Log("Client started successfully. Joining lobby...");
            ShowLobbyPanel();
        }
    }

    private void ShowLobbyPanel()
    {
        Debug.Log("Switching to Lobby Panel...");
        mainMenuPanel.SetActive(false);
        lobbyPanel.SetActive(true);

        // Enable Lobby Manager directly
        if (lobbyManager != null)
        {
            lobbyManager.SetActive(true);
            Debug.Log("Lobby Manager activated.");
        }

        StartCoroutine(WaitForHostAndSetupLobby());
    }

    private IEnumerator WaitForHostAndSetupLobby()
    {
        // Wait until the host is fully started
        while (!NetworkManager.Singleton.IsHost)
        {
            yield return null;
        }

        // Use the serialized reference, not FindFirstObjectByType
        if (lobbyManager != null)
        {
            var lobbyMgrScript = lobbyManager.GetComponent<LobbyManager>();
            if (lobbyMgrScript != null)
            {
                lobbyMgrScript.HostSetup();
            }
            else
            {
                Debug.LogError("LobbyManager script not found on lobbyManager GameObject!");
            }
        }
        else
        {
            Debug.LogError("lobbyManager GameObject reference is not set in MainMenu!");
        }
    }
}

