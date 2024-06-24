using Godot;
	using System;
	using System.Threading.Tasks;
	using System.Xml.Serialization;
using ZstdSharp.Unsafe;

public partial class ObsidianSphere : CharacterBody2D
	{
		private const float Speed = 1000.0f;
		private const float JumpVelocity = -400.0f;
		private const float SpeedEnemy = 4.0f;
		public int Direction = 1;
		private float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
		public AnimatedSprite2D Animation;
		public RayCast2D WallDetector;
		private bool IsDying = false;
		public bool PlayerDetected = false;
		private AudioStreamPlayer somRoboAndando;
		private AudioStreamPlayer somRoboMorrendo;
		private Area2D detectionArea;
		private Node2D Player;
		private Node2D AishaCat;
		private Node2D Target;


		public override void _Ready()
		{
			Animation = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
			WallDetector = GetNode<RayCast2D>("WallDetector");
			somRoboAndando = GetNode<AudioStreamPlayer>("roboAndando_sfx"); 
			somRoboMorrendo = GetNode<AudioStreamPlayer>("roboMorrendo_sfx");
			detectionArea = GetNode<Area2D>("DetectionArea");
			Player = GetTree().Root.GetNode<Node2D>("World1/Player");
			AishaCat= GetTree().Root.GetNode<Node2D>("World1/AishaCat");
			Target = Player;
		}

    	public override void _PhysicsProcess(double delta)
		{
			ManageFootstepsSound();
			ProcessMovement(delta);
		}

		private void ProcessMovement(double delta)
		{
			if(PlayerDetected){
				Vector2 directionToTarget = (Target.GlobalPosition - GlobalPosition).Normalized();
				Vector2 velocity = Velocity;
				velocity.X = directionToTarget.X * Speed * SpeedEnemy *(float)delta;
				Animation.FlipH = directionToTarget.X < 0;
				
				
				Velocity = velocity;
				MoveAndSlide();
			}
			else 
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
			
			
		}

		private void ManageFootstepsSound()
		{
			if (IsOnFloor() && PlayerDetected)
			{
				if (!somRoboAndando.Playing)
				{
					somRoboAndando.Play();
					
				}
			}
			else
			{
				somRoboAndando.Stop();
			}
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
			if (!IsDying && body.Name == "Player")
			{
				somRoboMorrendo.Play(0.35f);
				Animation.Play("hurt");
				IsDying = true;
				
			}
		}

		private void OnDetectionAreaBodyEntered(Node2D body)
		{
			if (body is Player || body is AishaCat)
			{
				PlayerDetected = true;
				Target = body as Node2D;
				GD.Print("Player detected");
			}
		}
		private void OnDetectionAreaBodyExited(Node2D body)
		{
			if (body is Player || body is AishaCat)
			{
				PlayerDetected = false;
				GD.Print("Player exited");
			}
		}
	}