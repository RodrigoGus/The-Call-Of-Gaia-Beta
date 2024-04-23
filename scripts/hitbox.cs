using Godot;
using System;

public partial class Hitbox : Area2D
{
	private Player player;
	public const float JumpVelocity = -400.0f;
	
	public void OnBodyEntered(Node2D body)
	{
		if ("Player" == body.Name)
		{
			this.player = (Player)body;
			Vector2 velocity = this.player.Velocity;
			velocity.Y = JumpVelocity / 2;
			this.player.Velocity = velocity;

			Owner.QueueFree();
		}
	}
}
