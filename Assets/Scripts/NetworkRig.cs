using UnityEngine;
using Fusion;
public class NetworkRig : NetworkBehaviour
{
    // Indicates whether this NetworkRig is controlled by the local player
    public bool IsLocalNetworkRig => Object.HasStateAuthority;

    [Header("RigVisuals")]
    [SerializeField] private GameObject _headVisuals;

    [Header("RigComponents")]
    [SerializeField] private NetworkTransform _characterTransform;
    [SerializeField] private NetworkTransform _headTransform;
    [SerializeField] private NetworkTransform _bodyTransform;
    [SerializeField] private NetworkTransform _leftHandTransform;
    [SerializeField] private NetworkTransform _rightHandTransform;

    private HardwareRig _hardwareRig;

    public override void Spawned()
    {
        base.Spawned();

        // Perform initialization when the NetworkRig is spawned
        if (IsLocalNetworkRig)
        {
            // Find the HardwareRig component in the scene
            _hardwareRig = FindObjectOfType<HardwareRig>();

            if (_hardwareRig == null)
            {
                Debug.LogError("Missing Hardware Rig in the Scene");
            }

            // Disale head Mesh if this is the local Rig
            _headVisuals.SetActive(false);
        }
        else
        {
            // This is a client object, not controlled by the local player
            Debug.Log("This is a client object");
        }
    }

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        // Apply input data received from the local player
        if (GetInput<XRRigInputData>(out var inputData))
        {
            // Update the positions and rotations of the NetworkTransforms based on the input data
            _characterTransform.transform.SetPositionAndRotation(inputData.CharacterPosition, inputData.CharacterRotation);
            _headTransform.transform.SetPositionAndRotation(inputData.HeadsetPosition, inputData.HeadsetRotation);
            _bodyTransform.transform.SetPositionAndRotation(inputData.BodyPosition, inputData.BodyRotation);
            _leftHandTransform.transform.SetPositionAndRotation(inputData.LeftHandPosition, inputData.LeftHandRotation);
            _rightHandTransform.transform.SetPositionAndRotation(inputData.RightHandPosition, inputData.RightHandRotation);
        }
    }

    public override void Render()
    {
        base.Render();

        // Update the interpolation targets of the NetworkTransforms based on the hardware rig's positions and rotations
        if (IsLocalNetworkRig && _hardwareRig != null)
        {
            _headTransform.InterpolationTarget.SetPositionAndRotation(_hardwareRig._headTransform.position, _hardwareRig._headTransform.rotation);
            _characterTransform.InterpolationTarget.SetPositionAndRotation(_hardwareRig._characterTransform.position, _hardwareRig._characterTransform.rotation);
            _bodyTransform.InterpolationTarget.SetPositionAndRotation(_hardwareRig._bodyTransform.position, _hardwareRig._bodyTransform.rotation);
            _leftHandTransform.InterpolationTarget.SetPositionAndRotation(_hardwareRig._leftHandTransform.position, _hardwareRig._leftHandTransform.rotation);
            _rightHandTransform.InterpolationTarget.SetPositionAndRotation(_hardwareRig._rightHandTransform.position, _hardwareRig._rightHandTransform.rotation);
        }
    }
}
