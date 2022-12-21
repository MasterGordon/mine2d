namespace Mine2d.game.state;

public class PlayerMovementState
{
    public Vector2 Speed { get; set; } = new Vector2
    {
        X = 50f,
        Y = 20f,
    };
    public float Drag { get; set; } = 0.1f;
    public bool IsGrounded { get; set; } = false;
    public Vector2 CurrentVelocity { get; set; } = Vector2.Zero;
    public Vector2 CurrentMovement { get; set; } = Vector2.Zero;
}