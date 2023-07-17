using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class HardwareRig : MonoBehaviour, INetworkRunnerCallbacks
{
    [Header("Rig Components")]
    public Transform _characterTransform;
    public Transform _headTransform;
    public Transform _bodyTransform;
    public Transform _leftHandTransform;
    public Transform _rightHandTransform;

    private void Start()
    {
        // Register this script as a callback receiver for network events
        NetworkManager.Instance.SessionRunner.AddCallbacks(this);
    }

    // This method is called when network input is received from a player
    void INetworkRunnerCallbacks.OnInput(NetworkRunner runner, NetworkInput input)
    {
        // Create an instance of the XRRigInputData struct to hold the input data
        XRRigInputData inputData = new XRRigInputData();

        // Populate the input data with the current positions and rotations of the rig components
        inputData.HeadsetPosition = _headTransform.position;
        inputData.HeadsetRotation = _headTransform.rotation;

        inputData.BodyPosition = _bodyTransform.position;
        inputData.BodyRotation = _bodyTransform.rotation;

        inputData.CharacterPosition = _characterTransform.position;
        inputData.CharacterRotation = _characterTransform.rotation;

        inputData.LeftHandPosition = _leftHandTransform.position;
        inputData.LeftHandRotation = _leftHandTransform.rotation;

        inputData.RightHandPosition = _rightHandTransform.position;
        inputData.RightHandRotation = _rightHandTransform.rotation;

        // Set the input data in the NetworkInput object
        input.Set(inputData);
    }

    #region Unused Callbacks
    // Implement the unused callbacks for the INetworkRunnerCallbacks interface
    void INetworkRunnerCallbacks.OnConnectedToServer(NetworkRunner runner)
    {
        // This method is called when the client successfully connects to the server
    }

    void INetworkRunnerCallbacks.OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        // This method is called when the client fails to connect to the server
    }

    void INetworkRunnerCallbacks.OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        // This method is called when a connect request is received from a client
    }

    void INetworkRunnerCallbacks.OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        // This method is called when a custom authentication response is received
    }

    void INetworkRunnerCallbacks.OnDisconnectedFromServer(NetworkRunner runner)
    {
        // This method is called when the client is disconnected from the server
    }

    void INetworkRunnerCallbacks.OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        // This method is called when a host migration occurs
    }

    void INetworkRunnerCallbacks.OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        // This method is called when input data is missing for a player
    }

    void INetworkRunnerCallbacks.OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        // This method is called when a new player joins the session
    }

    void INetworkRunnerCallbacks.OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        // This method is called when a player leaves the session
    }

    void INetworkRunnerCallbacks.OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
        // This method is called when reliable data is received from a player
    }

    void INetworkRunnerCallbacks.OnSceneLoadDone(NetworkRunner runner)
    {
        // This method is called when the scene loading is done
    }

    void INetworkRunnerCallbacks.OnSceneLoadStart(NetworkRunner runner)
    {
        // This method is called when the scene loading starts
    }

    void INetworkRunnerCallbacks.OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        // This method is called when the session list is updated
    }

    void INetworkRunnerCallbacks.OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        // This method is called when the runner is shut down
    }

    void INetworkRunnerCallbacks.OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        // This method is called when a user simulation message is received
    }
    #endregion
}

// Struct to hold the input data for the XR Rig
public struct XRRigInputData : INetworkInput
{
    public Vector3 HeadsetPosition;
    public Quaternion HeadsetRotation;

    public Vector3 BodyPosition;
    public Quaternion BodyRotation;

    public Vector3 CharacterPosition;
    public Quaternion CharacterRotation;

    public Vector3 LeftHandPosition;
    public Quaternion LeftHandRotation;

    public Vector3 RightHandPosition;
    public Quaternion RightHandRotation;
}
