using Godot;
using System;
//using System.Numerics;

public partial class moving_platform : Node2D
{
	private const int WAIT_DURATION = 1;

	private AnimatableBody2D platform;
	[Export]
	public const float MOVE_SPEED = 3f;
	[Export]
	public int distance = 192;
	[Export]
	public bool moveHorizontal = true;

	private Vector2 follow = Vector2.Zero;
	private int platformCenter = 16;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.platform = GetNode<AnimatableBody2D>("platform");
		MovePlatform();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double _delta)
	{
		this.platform.Position = this.platform.Position.Lerp(this.follow, 0.5f);
	}

	public void MovePlatform()
	{
		Vector2 moveDirection = this.moveHorizontal ? Vector2.Right * this.distance : Vector2.Up * this.distance;
		double duration = (double)moveDirection.Length() / MOVE_SPEED * this.platformCenter;

		Tween platformTween = CreateTween().SetLoops().SetTrans(Tween.TransitionType.Linear).SetEase(Tween.EaseType.InOut);

		platformTween.TweenProperty(platformTween, "follow", moveDirection, duration).SetDelay(WAIT_DURATION);
		platformTween.TweenProperty(platformTween, "follow", Vector2.Zero, duration).SetDelay(WAIT_DURATION);
	}
}
