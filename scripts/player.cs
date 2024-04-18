using Godot;

public partial class player : CharacterBody2D
{
    public const float Speed = 200.0f;
    public const float JumpVelocity = -400.0f;
    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    private NodePath animationNodePath = "anim";
    public AnimatedSprite2D animation;
    public bool isJumping = false;
    int inputDirection = 1;
    private NodePath remoteTransformPath = "remote";
    public RemoteTransform2D remoteTransform2D;
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
        else
        {
            velocity.Y += this.gravity * (float)delta;

            if (!this.isJumping) this.animation.Play("fall");
            else this.animation.Play("jump");
        }

        Vector2 direction = Input.GetVector("left", "right", "up", "down");
        if (direction != Vector2.Zero)
        {
            velocity.X = direction.X * Speed;
            if (Input.IsActionPressed("right")) this.inputDirection = 1;
            if (Input.IsActionPressed("left")) this.inputDirection = -1;

            this.animation.Scale = new Vector2(this.inputDirection, this.animation.Scale.Y);

            if (!this.isJumping) this.animation.Play("run");
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
            if (IsOnFloor()) this.animation.Play("idle");
        }

        if (knockbackVector != Vector2.Zero) velocity = knockbackVector;

        Velocity = velocity;
        MoveAndSlide();
    }

    private void OnHurtboxBodyEntered(Node2D body)
    {
        //if (body.IsInGroup("enemies")) QueueFree();
        if (Globals.player_life <= 0) QueueFree();
        else
        {
            if (rayRight.IsColliding()) TakeDamage(new Vector2(-200, -200));
            else if (rayLeft.IsColliding()) TakeDamage(new Vector2(200, -200));
        }
    }

    public void FollowCamera(Camera2D camera)
    {
        this.remoteTransform2D.RemotePath = camera.GetPath();
    }

    public void TakeDamage(Vector2 knockbackForce, double duration = 0.25)
    {
        Globals.player_life -= 1;
        if (knockbackForce != Vector2.Zero) knockbackVector = knockbackForce;

        Tween knockbackTween = GetTree().CreateTween();
        knockbackTween.Parallel().TweenProperty(this, "knockbackVector", Vector2.Zero, duration);
        animation.Modulate = new Color(1, 0, 0, 1);
        knockbackTween.Parallel().TweenProperty(animation, "modulate", new Color(1, 1, 1, 1), duration);
    }
}
