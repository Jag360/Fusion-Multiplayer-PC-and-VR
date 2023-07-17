using System;
using Fusion;
using UnityEngine;
using Fusion.Sockets;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerInput : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] InputActionReference _moveInput;

    private void OnEnable()
    {
        _moveInput.action.Enable();   // Enable the move input action
    }

    private void OnDisable()
    {
        _moveInput.action?.Disable(); // Disable the move input action
    }

    private void Start()
    {
        // Register this script as a callback receiver for network events
        NetworkManager.Instance.SessionRunner.AddCallbacks(this);
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        // This method is called when network input is received from a player.
        // Add your custom logic here, such as processing the input data or updating player actions.

        // Example:
        //Debug.Log("Received network input from player: ");

        // Read the movement input from the InputActionReference
        Vector2 direction = _moveInput.action.ReadValue<Vector2>();
        Vector3 dir = new Vector3(direction.x, 0, direction.y);

        // Create a custom struct to hold the player input data
        PlayerInputData inputData = new PlayerInputData();

        // Set the movement direction in the input data
        inputData.Direction = dir;

        // Set the input data in the NetworkInput object
        input.Set(inputData);
    }


    #region Unused RunnerCallBacks
    public void OnConnectedToServer(NetworkRunner runner)
    {
        // This method is called when the local player is successfully connected to the server.
        // Add your custom logic here, such as initializing player data or displaying connection success message.
        // Example:
        //Debug.Log("Connected to the server");
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        // This method is called when the connection to the server fails.
        // Add your custom logic here, such as displaying an error message or handling reconnect attempts.
        // Example:
        //Debug.Log("Connect failed. Reason: " + reason.ToString());
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        // This method is called when a connect request is received from a remote client.
        // Add your custom logic here, such as validating the connect request or performing custom authentication.
        // Example:
        //Debug.Log("Received connect request from client with token length: " + token.Length);
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        // This method is called when a custom authentication response is received from the server.
        // Add your custom logic here, such as handling the authentication result or updating player data based on the response.
        // Example:
        //Debug.Log("Received custom authentication response. Data count: " + data.Count);
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        // This method is called when the local player is disconnected from the server.
        // Add your custom logic here, such as cleaning up player data or showing a disconnection message.
        // Example:
        //Debug.Log("Disconnected from the server");
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        // This method is called when a host migration occurs during gameplay.
        // Add your custom logic here, such as handling the migration process or updating relevant game data.
        // Example:
        //Debug.Log("Host migration occurred");
    }

    

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        // This method is called when network input is missing from a player.
        // Add your custom logic here, such as handling missing input or implementing fallback behaviors.
        // Example:
        //Debug.Log("Missing network input from player: " + player.PlayerId);
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        // This method is called when a new player joins the session.
        // Add your custom logic here, such as instantiating the player character or updating player counts.
        // Example:
        //Debug.Log("A new player joined the session. Player ID: " + player.PlayerId);
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        // This method is called when a player leaves the session.
        // Add your custom logic here, such as removing the player character or updating player counts.
        // Example:
        //Debug.Log("A player left the session. Player ID: " + player.PlayerId);
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
        // This method is called when reliable data is received from a player.
        // Add your custom logic here, such as processing the data or triggering game events based on the received data.
        // Example:
        //Debug.Log("Received reliable data from player: " + player.PlayerId + ", Data length: " + data.Count);
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        // This method is called when the scene load is completed.
        // Add your custom logic here, such as initializing game objects or triggering post-load actions.
        // Example:
        //Debug.Log("Scene load completed");
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        // This method is called when the scene load starts.
        // Add your custom logic here, such as showing loading indicators or preparing for scene transition.
        // Example:
        //Debug.Log("Scene load started");
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        // This method is called when the session list is updated.
        // Add your custom logic here, such as refreshing the available sessions or updating UI elements.
        // Example:
        //Debug.Log("Updated session list. Count: " + sessionList.Count);
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        // This method is called when the NetworkRunner is shut down.
        // Add your custom logic here, such as handling clean-up tasks or saving game data.
        // Example:
        //Debug.Log("Runner Shutdown. Reason: " + shutdownReason.ToString());
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        // This method is called when a user simulation message is received.
        // Add your custom logic here, such as processing the message or triggering game-specific actions.
        // Example:
        //Debug.Log("Received user simulation message. Message type: ");
    }
    #endregion

}

public struct PlayerInputData: INetworkInput
{
    public Vector3 Direction;
}
