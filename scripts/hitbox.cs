using Godot;
using System;

public partial class hitbox : Area2D
{
	private player player;
	public const float JumpVelocity = -400.0f;
	
	public void OnBodyEntered(Node2D body)
	{
		if ("Player" == body.Name)
		{
			this.player = (player)body;
			Vector2 velocity = this.player.Velocity;
			velocity.Y = JumpVelocity / 2;
			this.player.Velocity = velocity;

			Owner.QueueFree();
		}
	}
}
