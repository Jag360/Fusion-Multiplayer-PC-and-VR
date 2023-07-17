using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;

public class PlayerSpawner : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private NetworkPrefabRef _playerPrefab; // Reference to the network prefab for spawning players

    private void Start()
    {
        // Add this PlayerSpawner as a callback receiver to the NetworkRunner instance in NetworkManager
        NetworkManager.Instance.SessionRunner.AddCallbacks(this);
    }

    //[------------------------------------------------------------------------]
    // Implement the INetworkRunnerCallbacks interface to handle network events

    public void OnConnectedToServer(NetworkRunner runner)
    {
        // This method is called when the local player is successfully connected to the server.
        // Add your custom logic here, such as initializing player data or displaying connection success message.
        // Example:
        Debug.Log("Connected to the server");
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        // This method is called when the connection to the server fails.
        // Add your custom logic here, such as displaying an error message or handling reconnect attempts.
        // Example:
        Debug.Log("Connect failed. Reason: " + reason.ToString());
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        // This method is called when a connect request is received from a remote client.
        // Add your custom logic here, such as validating the connect request or performing custom authentication.
        // Example:
        Debug.Log("Received connect request from client with token length: " + token.Length);
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        // This method is called when a custom authentication response is received from the server.
        // Add your custom logic here, such as handling the authentication result or updating player data based on the response.
        // Example:
        Debug.Log("Received custom authentication response. Data count: " + data.Count);
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        // This method is called when the local player is disconnected from the server.
        // Add your custom logic here, such as cleaning up player data or showing a disconnection message.
        // Example:
        Debug.Log("Disconnected from the server");
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        // This method is called when a host migration occurs during gameplay.
        // Add your custom logic here, such as handling the migration process or updating relevant game data.
        // Example:
        Debug.Log("Host migration occurred");
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        // This method is called when network input is received from a player.
        // Add your custom logic here, such as processing the input data or updating player actions.
        // Example:
        //Debug.Log("Received network input from player: ");
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        // This method is called when network input is missing from a player.
        // Add your custom logic here, such as handling missing input or implementing fallback behaviors.
        // Example:
        Debug.Log("Missing network input from player: " + player.PlayerId);
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        // This method is called when a new player joins the session.
        // Add your custom logic here, such as instantiating the player character or updating player counts.
        // Example:
        Debug.Log("A new player joined the session. Player ID: " + player.PlayerId);

        // Check if the joinned player is local player???
        if(player == runner.LocalPlayer)
        {
            Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(1,5), 0.5f, UnityEngine.Random.Range(1, 5)); // Random Position

            runner.Spawn(_playerPrefab, spawnPosition, Quaternion.identity, player); // Spawn the Player in Random Place
        }

    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        // This method is called when a player leaves the session.
        // Add your custom logic here, such as removing the player character or updating player counts.
        // Example:
        Debug.Log("A player left the session. Player ID: " + player.PlayerId);
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

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        // This method is called when the session list is updated.
        // Add your custom logic here, such as refreshing the available sessions or updating UI elements.
        // Example:
        Debug.Log("Updated session list. Count: " + sessionList.Count);
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        // This method is called when the NetworkRunner is shut down.
        // Add your custom logic here, such as handling clean-up tasks or saving game data.
        // Example:
        Debug.Log("Runner Shutdown. Reason: " + shutdownReason.ToString());
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        // This method is called when a user simulation message is received.
        // Add your custom logic here, such as processing the message or triggering game-specific actions.
        // Example:
        Debug.Log("Received user simulation message. Message type: ");
    }

}
