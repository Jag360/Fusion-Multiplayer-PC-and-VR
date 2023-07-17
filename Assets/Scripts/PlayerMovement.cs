using Fusion;

public class PlayerMovement : NetworkBehaviour
{
    public float MoveSpeed;

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        // Check if there is valid input data available for the player
        if (GetInput<PlayerInputData>(out var inputData))
        {
            // Move the player based on the input direction and movement speed
            transform.Translate(inputData.Direction * Runner.DeltaTime * MoveSpeed);
        }
    }
}
