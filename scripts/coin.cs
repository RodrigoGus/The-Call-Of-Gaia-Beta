using Godot;
using System;

public partial class coin : Area2D
{
	private void OnBodyEntered(Node2D body)
	{
		GetNode<AnimatedSprite2D>("Anim").Play("collected");
	}
	private void OnAnimAnimationFinished()
	{
		QueueFree();
	}
}
