using Godot;
using System;

public partial class enemy : CharacterBody2D
{
	public const float Speed = 3500.0f;
	public const float JumpVelocity = -400.0f;
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	private int direction = 1;
	[Export]
	private NodePath animationNodePath = "anim";
	private AnimationPlayer animation;
	private NodePath texturePath = "sprite";
	private Sprite2D texture;
	private NodePath wallDetectorPath = "wall detector";
	private RayCast2D wallDetector;

	public override void _Ready()
	{
		this.animation = GetNode<AnimationPlayer>(this.animationNodePath);
		this.wallDetector = GetNode<RayCast2D>(this.wallDetectorPath);
		this.texture = GetNode<Sprite2D>(this.texturePath);
	}
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		if (!IsOnFloor()) velocity.Y += this.gravity * (float)delta;

		if (this.wallDetector.IsColliding())
		{
			this.direction *= -1;
			this.wallDetector.Scale *= -1;
			this.texture.FlipH = !this.texture.FlipH;
		}

		velocity.X = this.direction * Speed * (float)delta;

		animation.Play("run");

		Velocity = velocity;
		MoveAndSlide();
	}
}
