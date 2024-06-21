using Godot;

public partial class Player : CharacterBody2D
{
	private bool isTransforming = false;
	public PackedScene catScene = (PackedScene)ResourceLoader.Load("res://actors/AishaCat.tscn");
	public World1 worldScene;
	private NodePath worldScenePath = "/root/World1";
	public static bool isTransformedToCat;
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
		this.worldScene = GetNode<World1>(worldScenePath);
		this.rayRight = GetNode<RayCast2D>(rayRightPath);
		this.rayLeft = GetNode<RayCast2D>(rayLeftPath);
		Position = Globals.playerPosition;
		isTransformedToCat = false;
	}
	public override void _PhysicsProcess(double delta)
	{
		if (isTransforming)
		{
			return;
		}
			
		
		Vector2 velocity = Velocity;

		if(Input.IsActionJustPressed("T") && !isTransformedToCat){
			StartTransformation();
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
			if (Input.IsActionPressed("right")) this.inputDirection = 0.203f;
			if (Input.IsActionPressed("left")) this.inputDirection = -0.203f;
			
			this.animation.Scale = new Vector2(this.inputDirection, this.animation.Scale.Y);
		}
		else velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);

		if (this.knockbackVector != Vector2.Zero) velocity = this.knockbackVector;
		
		UpdateAnimation();

		Velocity = velocity;
		MoveAndSlide();

	}

	private void OnHurtboxBodyEntered(Node2D body)
	{
		if (Globals.playerLife <= 0) 
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
		Globals.playerLife -= 1;
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

	private void UpdateAnimation()
	{
		string state = "idle";

		if (direction != Vector2.Zero)
			state = "run";
		if (!IsOnFloor())
			state = isJumping ? "jump" : "fall";
		if (isHited)
			state = "hurt";
		if (isTransforming)
			state = "transform_to_cat";

		if (animation.Name != state)
			animation.Play(state);
	}
	private void StartTransformation()
	{
		isTransforming = true;
		UpdateAnimation();
	}

	public void AishaToCat()
	{
		if (!isTransformedToCat)
		{
			CharacterBody2D cat = catScene.Instantiate<CharacterBody2D>();
			GetParent().AddChild(cat);
			cat.Position = Position;
			QueueFree();
			isTransformedToCat = true;
		}
	}
	private void OnAnimAnimationFinished()
	{
		if (animation.Animation == "transform_to_cat" && isTransforming)
		{
			isTransforming = false;
			AishaToCat();
		}
	}

}
