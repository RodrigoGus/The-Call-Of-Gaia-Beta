using Godot;
using System;
using System.Threading.Tasks;

public partial class ObsidianSphere : CharacterBody2D
{
	private const float Speed = 400.0f;
	private const float JumpVelocity = -400.0f;
	public int _direction = 1;
	private float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	public AnimatedSprite2D Animation;
	public RayCast2D WallDetector;
	private bool _isDying = false;
	public bool PlayerDetected = false;

	public override void _Ready()
	{
		Animation = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		WallDetector = GetNode<RayCast2D>("WallDetector");
	}

	public override void _PhysicsProcess(double delta)
	{
		if (_isDying) return;
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
		if (PlayerDetected)
		{
			Position += (player.position - Position)/Speed;
		} else{
			velocity.X = _direction * Speed * (float)delta;
		}


		Velocity = velocity;
		MoveAndSlide();
	}

	private void CheckWallCollision()
	{
		if (WallDetector.IsColliding())
		{
			_direction *= -1;
			WallDetector.Scale *= new Vector2(-1, 1);
			Animation.FlipH = !Animation.FlipH;
		}
	}

	private void OnAnimatedSprite2dAnimationFinished()
	{
		if (_isDying)
		{
			_isDying = false;
			QueueFree();  // Remove the enemy from the scene
		}
	}

	private void OnHitboxBodyEntered(Node2D body)
	{
		if (!_isDying)  // Check to ensure we do not restart the animation if already dying
		{
			Animation.Play("hurt");
			_isDying = true;
		}
	}

	private void OnDetectionAreaBodyEntered(Node2D body)
	{
		if (body.Name == "player") PlayerDetected = true;
	}
}









