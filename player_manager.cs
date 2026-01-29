using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.Services.Lobbies.Models;


public class player_manager : NetworkBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool IsInMenu = true;
    public bool IsInLobby = false;
    public bool IsInGame = false;
    public GameObject PlayerPrefab;
    public GameObject PlayerUI; 
    public GameObject PlayerCamera;
    public bool LobbyActive = false;
    public GameObject pressEText;
    void Start()
    {
        if (!IsServer)
        {
            // If not the server, disable this script
            enabled = false;
            return;
        }
        // Subscribe to sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Set initial state based on current scene
        UpdateStateForScene(SceneManager.GetActiveScene().name);
    }

    public override void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        base.OnDestroy();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateStateForScene(scene.name);
    }

    private void UpdateStateForScene(string sceneName)
    {
        // Adjust these names to match your actual scene names
        if (sceneName == "Store Main Menu" && !LobbyActive) // Change "Store Main Menu" to your actual menu scene name if different
        {
            if (LobbyActive)
            {
                IsInMenu = false;
                IsInLobby = true;
                IsInGame = false;
            }
            else
            {
                IsInMenu = true;
                IsInLobby = false;
                IsInGame = false;
            }
        }
        else if (sceneName == "Store Game") // Support both names
        {
            IsInMenu = false;
            IsInLobby = false;
            IsInGame = true;
        }
        else
        {
            // Default fallback
            IsInMenu = false;
            IsInLobby = false;
            IsInGame = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Remove scene name checks from Update, rely on sceneLoaded event instead

            if (IsInMenu)
            {
                if (PlayerPrefab != null) PlayerPrefab.SetActive(false);
                if (PlayerCamera != null) PlayerCamera.SetActive(false);
            }
            else if (IsInLobby)
            {
                if (PlayerPrefab != null) PlayerPrefab.SetActive(false);
                if (PlayerCamera != null) PlayerCamera.SetActive(false);
            }
            else if (IsInGame)
            {
                // Handle game logic
                if (PlayerPrefab != null && PlayerUI != null && PlayerCamera != null)
                {
                    PlayerPrefab.SetActive(true);
                    PlayerCamera.SetActive(true);
                }
            }
        }
    public void TakeDamage(int amount)
{
    // Implement your health system here
    Debug.Log($"Player took {amount} damage!");
    // e.g., currentHealth -= amount;
}
}