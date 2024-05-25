using Godot;
using System;
using System.Threading.Tasks;

public partial class ObsidianSphere : CharacterBody2D
{
	private const float Speed = 1000.0f;
	private const float JumpVelocity = -400.0f;
	public int Direction = 1;
	private float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	public AnimatedSprite2D Animation;
	public RayCast2D WallDetector;
	private bool IsDying = false;
	public bool PlayerDetected = false;

	public override void _Ready()
	{
		Animation = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		WallDetector = GetNode<RayCast2D>("WallDetector");
	}

	public override void _PhysicsProcess(double delta)
	{
		ProcessMovement(delta);
	}

	private void ProcessMovement(double delta)
	{
		Vector2 velocity = Velocity;
		if (!IsOnFloor())
		{
			velocity.Y += _gravity * (float)delta;
		}

		CheckWallCollision();
		velocity.X = Direction * Speed * (float)delta;
		Velocity = velocity;
		MoveAndSlide();
	}

	private void CheckWallCollision()
	{
		if (WallDetector.IsColliding())
		{
			Direction *= -1;
			WallDetector.Scale *= new Vector2(-1, 1);
			Animation.FlipH = !Animation.FlipH;
		}
	}

	private void OnAnimatedSprite2dAnimationFinished()
	{
		if (IsDying)
		{
			IsDying = false;
			QueueFree();
		}
	}

	private void OnHitboxBodyEntered(Node2D body)
	{
		if (!IsDying)
		{
			Animation.Play("hurt");
			IsDying = true;
		}
	}

	private void OnDetectionAreaBodyEntered(Node2D body)
	{
		if (body.Name == "Player") PlayerDetected = true;
	}
	private void OnDetectionAreaBodyExited(Node2D body)
	{
		if (body.Name == "Player") PlayerDetected = false;
	}
}
