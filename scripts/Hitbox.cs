using Godot;

public partial class Hitbox : Area2D
{
	private player _player;
	private AishaCat _aishaCat;
	public const float JumpVelocity = -400.0f;

	public override void _Ready()
	{
	}

	public void OnBodyEntered(Node2D body)
	{
		if ("Player" == body.Name)
		{
			this._player = (player)body;
			Vector2 velocity = this._player.Velocity;
			velocity.Y = JumpVelocity / 2;
			this._player.Velocity = velocity;
		} else if ("AishaCat" == body.Name)
		{
			this._aishaCat = (AishaCat)body;
			Vector2 velocity = this._aishaCat.Velocity;
			velocity.Y = JumpVelocity / 2;
			this._aishaCat.Velocity = velocity;
		}
	}
}
