using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class LocalVRMovement : MonoBehaviour
{
    public float MovementSpeed;
    public float RotateSpeed;

    [SerializeField] private Transform _headTransform;

    [SerializeField] private InputActionReference _moveInput;
    [SerializeField] private InputActionReference _rotateInput;

    private CharacterController _characterController;
    private float _gravity = 0f;

    private void OnEnable()
    {
        // Enable the move and rotate input actions
        _moveInput.action.Enable();
        _rotateInput.action.Enable();
    }

    private void OnDisable()
    {
        // Disable the move and rotate input actions
        _moveInput.action.Disable();
        _rotateInput.action.Disable();
    }

    private void Start()
    {
        // Get the CharacterController component
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
        Rotate();
        ApplyGravity();
    }

    private void Move()
    {
        // Read the movement input from the move input action
        Vector2 movement = _moveInput.action.ReadValue<Vector2>();

        // Convert the movement input to a direction in local space
        Vector3 direction = new Vector3(movement.x, 0, movement.y);
        direction = _headTransform.TransformDirection(direction);
        direction = Vector3.Scale(direction, new Vector3(1, 0, 1).normalized);

        // Move the character using the CharacterController component
        _characterController.Move(direction * MovementSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        // Read the rotation input from the rotate input action
        Vector2 rotation = _rotateInput.action.ReadValue<Vector2>();

        // Rotate the character around the Y-axis based on the rotation input
        transform.rotation *= Quaternion.Euler(0, rotation.x * RotateSpeed, 0);
    }

    private void ApplyGravity()
    {
        //Phsyics Check To Ground
        //isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Apply gravity to the character using the CharacterController component
        if (_characterController.isGrounded)
        {
            _gravity = 0;
        }
        else
        {
            _gravity -= 9.81f * Time.deltaTime;
            _characterController.Move(new Vector3(0, _gravity, 0));
        }
    }
}
