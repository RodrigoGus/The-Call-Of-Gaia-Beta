using Godot;
	using System;
	using System.Threading.Tasks;
	using System.Xml.Serialization;

	public partial class ObsidianVulture : CharacterBody2D{
        private const float Speed = 1500.0f;
        private const float SpeedEnemy = 5.0f; 
        private const float DetectionRange = 400.0f; 
        private const float AttackRange = 50.0f; 
        private int Direction = 1; 
        private Node2D Player; 
        private bool PlayerDetected = false;
        private AnimatedSprite2D Animation;
        private AudioStreamPlayer FlyingSound;
        private AudioStreamPlayer AttackSound;
        private float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

        public override void _Ready()
        {
            Animation = GetNode<AnimatedSprite2D>("AnimatedSprite");
            FlyingSound = GetNode<AudioStreamPlayer>("flying_sound");
            AttackSound = GetNode<AudioStreamPlayer>("attack_sound");
            Player = GetTree().Root.GetNode<Node2D>("World1/Player");
        }

        public override void _PhysicsProcess(double delta)
        {
            ProcessMovement(delta);
            ManageFlyingSound();
        }

        private void ProcessMovement(double delta)
        {
            if (!PlayerDetected)
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
            else
            {
                Vector2 directionToPlayer = (Player.GlobalPosition - GlobalPosition).Normalized();
                Vector2 velocity = Velocity;
                velocity.X = directionToPlayer.X * SpeedEnemy * (float)delta;
                Animation.FlipH = directionToPlayer.X < 0;

                if (GlobalPosition.DistanceTo(Player.GlobalPosition) <= AttackRange)
                {
                    AttackPlayer();
                }

                Velocity = velocity;
                MoveAndSlide();
            }
        }

        private void ManageFlyingSound()
        {
            if (PlayerDetected)
            {
                if (!FlyingSound.Playing)
                {
                    FlyingSound.Play();
                }
            }
            else
            {
                FlyingSound.Stop();
            }
        }

        private void CheckWallCollision()
        {
           
        }

        private void AttackPlayer()
        {
            if (!AttackSound.Playing)
            {
                AttackSound.Play();
            }
            
        }

        private void OnHitboxBodyEntered(Node body)
        {
            if (body is Player && !PlayerDetected)
            {
                PlayerDetected = true;
                GD.Print("Player detected");
            }
        }

        private void OnHitboxBodyExited(Node body)
        {
            if (body is Player && PlayerDetected)
            {
                PlayerDetected = false;
                GD.Print("Player exited");
            }
        }
    }
	