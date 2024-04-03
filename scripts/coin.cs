using Godot;
using System;

public partial class coin : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void OnBodyEntered(Node2D body)
	{
		GetNode<AnimatedSprite2D>("anim").Play("collected");
	}

	private void OnAnimAnimationFinished()
	{
		QueueFree();
	}
}


