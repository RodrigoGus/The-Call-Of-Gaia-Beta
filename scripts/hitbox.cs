using Godot;

public partial class hitbox : Area2D
{
    private player _player;
    public const float JumpVelocity = -400.0f;

    public override void _Ready()
    {
    }

    public void OnBodyEntered(Node2D body)
    {
        if ("player" == body.Name)
        {
            this._player = (player)body;
            Vector2 velocity = this._player.Velocity;
            velocity.Y = JumpVelocity / 2;
            this._player.Velocity = velocity;
        }
    }
}
