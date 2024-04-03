using Godot;
using System;

public partial class player : CharacterBody2D
{
	public const float Speed = 200.0f;
	public const float JumpVelocity = -400.0f;
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public AnimatedSprite2D animation;
	public bool isJumping = false;
	int inputDirection = 1;

	public override void _Ready()
	{
		animation = GetNode<AnimatedSprite2D>("anim");
	}

	public void _Process()
	{
		if (Input.IsActionPressed("esc"))
		{
			GetTree().Quit();
		}
	}
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		if (!IsOnFloor()) velocity.Y += gravity * (float)delta;
		if (Input.IsActionJustPressed("up") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
			isJumping = true;
		}
		else if (IsOnFloor()) isJumping = false;

		Vector2 direction = Input.GetVector("left", "right", "up", "down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;

			if (Input.IsActionPressed("right")) inputDirection = 1;
			else if (Input.IsActionPressed("left")) inputDirection = -1;

			animation.Scale = new Vector2(inputDirection, animation.Scale.Y);

			if (!isJumping) animation.Play("run");
			else if (isJumping) animation.Play("jump");
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			animation.Play("idle");
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
