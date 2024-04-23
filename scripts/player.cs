using Godot;
using System;
using System.Collections;

public partial class Player : CharacterBody2D
{
	public const float Speed = 150.0f;
	public const float JumpVelocity = -325.0f;
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle() / 1.5f;

	private Vector2 direction;
	private NodePath animationNodePath = "Anim";
	public AnimatedSprite2D animation;
	public bool isJumping = false;
	public bool isHited = false;
	float inputDirection = 0.203f;
	private NodePath remoteTransformPath = "Remote";
	public RemoteTransform2D remoteTransform2D;
	public int playerLife = 10;
	private Vector2 knockbackVector;
	private NodePath rayRightPath = "RayRight";
	public RayCast2D rayRight;
	private NodePath rayLeftPath = "RayLeft";
	public RayCast2D rayLeft;

	public override void _Ready()
	{
		this.animation = GetNode<AnimatedSprite2D>(animationNodePath);
		this.remoteTransform2D = GetNode<RemoteTransform2D>(remoteTransformPath);
		this.rayRight = GetNode<RayCast2D>(rayRightPath);
		this.rayLeft = GetNode<RayCast2D>(rayLeftPath);
	}
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		if (IsOnFloor())
		{
			if (Input.IsActionJustPressed("up"))
			{
				velocity.Y = JumpVelocity;
				this.isJumping = true;
			}
			else this.isJumping = false;
		}
		else velocity.Y += this.gravity * (float)delta; 

		this.direction = Input.GetVector("left", "right", "up", "down");
		if (this.direction != Vector2.Zero)
		{
			velocity.X = this.direction.X * Speed;
			if (Input.IsActionPressed("right")) this.inputDirection = 0.203f;
			if (Input.IsActionPressed("left")) this.inputDirection = -0.203f;
			
			this.animation.Scale = new Vector2(this.inputDirection, this.animation.Scale.Y);
		}
		else velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);

		if (this.knockbackVector != Vector2.Zero) velocity = this.knockbackVector;

		SetState();
		Velocity = velocity;
		MoveAndSlide();
	}

	private void OnHurtboxBodyEntered(Node2D body)
	{
		//if (body.IsInGroup("enemies")) QueueFree();
		if (this.playerLife <= 0) QueueFree();
		else
		{
			if (this.rayRight.IsColliding()) TakeDamage(new Vector2(-200, -200));
			else if (this.rayLeft.IsColliding()) TakeDamage(new Vector2(200, -200));
		}
	}

	public void FollowCamera(Camera2D camera)
	{
		this.remoteTransform2D.RemotePath = camera.GetPath();
	}

	public async void TakeDamage(Vector2 knockbackForce)
	{
		this.playerLife -= 1;
		if(knockbackForce != Vector2.Zero)
		{
			this.knockbackVector = knockbackForce;
			Tween knockbackTween = GetTree().CreateTween();
			knockbackTween.Parallel().TweenProperty(this, "knockbackVector", Vector2.Zero, 0.25);
			this.animation.Modulate = new Color(1, 0, 0, 1);
			knockbackTween.Parallel().TweenProperty(this.animation, "modulate", new Color(1, 1, 1, 1), 0.3);
		}

		this.isHited = true;
		await ToSignal(GetTree().CreateTimer(0.3), "timeout");
		this.isHited = false;
	}

	public void SetState()
	{
		String state = "idle";

		if (this.direction != Vector2.Zero) state = "run";
		if (!IsOnFloor() && this.isJumping) state = "jump";
		if (!IsOnFloor() && !this.isJumping) state = "fall";
		if (this.isHited) state = "hurt";

		if(this.animation.Name != state) this.animation.Play(state);
	}
}
