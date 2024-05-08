using Godot;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Data.Common;

public partial class AishaCat : CharacterBody2D
{
	private bool isTransforming = false;
	public PackedScene playerScene = (PackedScene)ResourceLoader.Load("res://actors/player.tscn");
	public world1 worldScene;
	private NodePath worldScenePath = "/root/world1";
	public static bool isTransformedToCat;
	public const float Speed = 150.0f;
	public const float JumpVelocity = -325.0f;
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle() / 1.5f;
	private Vector2 direction;
	private NodePath animationNodePath = "CatAnim";
	public AnimatedSprite2D animation;
	public bool isJumping = false;
	public bool isHited = false;
	float inputDirection = 0.203f;
	private NodePath remoteTransformPath = "Remote";
	public static RemoteTransform2D remoteTransform2D;
	private Vector2 knockbackVector;
	private NodePath rayRightPath = "RayRight";
	public RayCast2D rayRight;
	private NodePath rayLeftPath = "RayLeft";
	public RayCast2D rayLeft;
	public static Vector2 position;
	public static bool isDeath = false;


	public override void _Ready()
	{
		this.animation = GetNode<AnimatedSprite2D>(animationNodePath);
		remoteTransform2D = GetNode<RemoteTransform2D>(remoteTransformPath);
		this.worldScene = GetNode<world1>(worldScenePath);
		this.rayRight = GetNode<RayCast2D>(rayRightPath);
		this.rayLeft = GetNode<RayCast2D>(rayLeftPath);
		isTransformedToCat = true;
	}
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		if(Input.IsActionJustPressed("T")){
			isTransforming = true;
		}


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
			if (Input.IsActionPressed("right")) {
				this.inputDirection = 0.203f;
				this.animation.FlipH = false;
			}
			if (Input.IsActionPressed("left")) {
				this.inputDirection = -0.203f;
				this.animation.FlipH = true;
			}
			// this.animation.Scale = new Vector2(this.inputDirection, this.animation.Scale.Y);
			// this.catAnimation.Scale = new Vector2(this.inputDirection, this.animation.Scale.Y);
		}
		else velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);

		if (this.knockbackVector != Vector2.Zero) velocity = this.knockbackVector;

		SetState();
		Velocity = velocity;
		MoveAndSlide();
	}

	private void OnHurtboxBodyEntered(Node2D body)
	{

		if (Globals.player_life <= 0) 
		{
			QueueFree(); 
			isDeath = true;
		}
		else
		{
			if (this.rayRight.IsColliding()) TakeDamage(new Vector2(-200, -200));
			else if (this.rayLeft.IsColliding()) TakeDamage(new Vector2(200, -200));
		}
	}

	public static void FollowCamera(Camera2D camera)
	{
		remoteTransform2D.RemotePath = camera.GetPath();
	}

	public async void TakeDamage(Vector2 knockbackForce)
	{
		Globals.player_life -= 1;
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
		StringName state = "idle";

		if (this.direction != Vector2.Zero) state = "run";
		if (!IsOnFloor() && this.isJumping) state = "jump";
		if (!IsOnFloor() && !this.isJumping) state = "fall";

		if (this.isTransforming) state = "transform_to_human";

		if(this.animation.Name != state) this.animation.Play(state);
		// if(this.catAnimation.Name != state) this.catAnimation.Play(state);
	}

	public void CatToAisha(){
		if(isTransformedToCat){
			CharacterBody2D player = playerScene.Instantiate<CharacterBody2D>();
			GetParent().AddChild(player);
			player.Position = this.Position;
			this.QueueFree();
			isTransformedToCat = false;
			GD.Print(isTransformedToCat);
		}
	}
	private void OnCatAnimAnimationFinished()
	{
		isTransforming = false;
		CatToAisha();
	}


}



