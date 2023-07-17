using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class NetworkManager : MonoBehaviour, INetworkRunnerCallbacks
{
    // Singleton instance of NetworkManager
    public static NetworkManager Instance { get; private set; }

    // The network runner responsible for managing networking functionality
    public NetworkRunner SessionRunner { get; private set; }

    [SerializeField] private GameObject _runnerPrefab; // Prefab used to create the network runner

    private void Awake()
    {
        // Singleton pattern implementation
        // If an instance of NetworkManager already exists, destroy the current instance
        // Otherwise, set the current instance as the singleton and mark it as persistent throughout scenes
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    async void Start()
    {
        // Create the network runner when the game starts
        CreateRunner();

        // Connect to the network
        await Connect();
    }

    public void CreateRunner()
    {
        // Instantiate the network runner prefab and get the NetworkRunner component from it
        SessionRunner = Instantiate(_runnerPrefab, transform).GetComponent<NetworkRunner>();

        // Register this NetworkManager as the callback receiver for the network events
        SessionRunner.AddCallbacks(this);
    }

    private async Task Connect()
    {
        // Create StartGameArgs object with necessary parameters for starting the game
        var args = new StartGameArgs()
        {
            GameMode = GameMode.Shared, // Example: Set the game mode to "Shared"
            SessionName = "BrightSession", // Example: Set the session name to "BrightSession"
            SceneManager = GetComponent<NetworkSceneManagerDefault>(), // Get the NetworkSceneManagerDefault component from this object
        };

        // Start the game asynchronously using the SessionRunner and provided args
        var result = await SessionRunner.StartGame(args);

        if (result.Ok)
        {
            // If the game starts successfully, log a message
            Debug.Log("Start Game Successful");
        }
        else
        {
            // If there was an error starting the game, log the error message
            Debug.LogError(result.ErrorMessage);
        }
    }


    // Implement the INetworkRunnerCallbacks interface to handle network events

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        // This method is called when a new player joins the session.
        // Add your custom logic here, such as updating player counts or initializing player-specific data.
        // Example:
        Debug.Log("A new player joined this session. Player ID: " + player.PlayerId);
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        // This method is called when a player leaves the session.
        // Add your custom logic here, such as updating player counts or handling player disconnections.
        // Example:
        Debug.Log("A player left the session. Player ID: " + player.PlayerId);
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        // This method is called when network input is received from a player.
        // Add your custom logic here, such as processing the input or triggering player actions.
        // Example:
        //Debug.Log("Received network input from player: " + input.ToString());
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        // This method is called when network input is missing from a player.
        // Add your custom logic here, such as handling the missing input or implementing fallback behaviors.
        // Example:
        Debug.Log("Missing network input from player: " + player.PlayerId);
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        // This method is called when the NetworkRunner is shut down.
        // Add your custom logic here, such as handling clean-up tasks or saving game data.
        // Example:
        Debug.Log("Runner Shutdown. Reason: " + shutdownReason.ToString());
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        // This method is called when the local player is connected to the server.
        // Add your custom logic here, such as initializing player data or displaying connection success message.
        // Example:
        Debug.Log("Connected to the server");
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        // This method is called when the local player is disconnected from the server.
        // Add your custom logic here, such as cleaning up player data or showing a disconnection message.
        // Example:
        Debug.Log("Disconnected from the server");
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        // This method is called when a connect request is received from a remote client.
        // Add your custom logic here, such as validating the connect request or performing custom authentication.
        // Example:
        Debug.Log("Received connect request. Token length: " + token.Length);
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        // This method is called when the connection to the server fails.
        // Add your custom logic here, such as displaying an error message or handling reconnect attempts.
        // Example:
        Debug.Log("Connect failed. Reason: " + reason.ToString());
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        // This method is called when a user simulation message is received.
        // Add your custom logic here, such as processing the message or triggering game-specific actions.
        // Example:
        Debug.Log("Received user simulation message. Message type: " + message.ToString());
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        // This method is called when the session list is updated.
        // Add your custom logic here, such as refreshing the available sessions or updating UI elements.
        // Example:
        Debug.Log("Updated session list. Count: " + sessionList.Count);
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        // This method is called when a custom authentication response is received from the server.
        // Add your custom logic here, such as handling the authentication result or updating player data based on the response.
        // Example:
        Debug.Log("Received custom authentication response. Data count: " + data.Count);
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        // This method is called when a host migration occurs during gameplay.
        // Add your custom logic here, such as handling the migration process or updating relevant game data.
        // Example:
        Debug.Log("Host migration occurred");
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
        // This method is called when reliable data is received from a player.
        // Add your custom logic here, such as processing the data or triggering game events based on the received data.
        // Example:
        Debug.Log("Received reliable data from player: " + player.PlayerId + ", Data length: " + data.Count);
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        // This method is called when the scene load is completed.
        // Add your custom logic here, such as initializing game objects or triggering post-load actions.
        // Example:
        Debug.Log("Scene load completed");
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        // This method is called when the scene load starts.
        // Add your custom logic here, such as showing loading indicators or preparing for scene transition.
        // Example:
        Debug.Log("Scene load started");
    }
}
