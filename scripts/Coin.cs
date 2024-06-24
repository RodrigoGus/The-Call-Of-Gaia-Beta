using Godot;

public partial class Coin : Area2D
{

	public int coins = 1;
	public CollisionShape2D _collision;
	private AudioStreamPlayer somMoeda;

	public override void _Ready()
	{
		_collision = GetNode<CollisionShape2D>("Collision");
		somMoeda = GetNode<AudioStreamPlayer>("somMoeda_sfx");


	}

	public override void _Process(double delta)
	{
	}
	private void OnBodyEntered(Node2D body)
	{
		_collision.SetDeferred("disabled", true);
		GetNode<AnimatedSprite2D>("Anim").Play("collected");
		somMoeda.Play();
		Globals.coins += coins;
	}
	private void OnAnimAnimationFinished()
	{
		QueueFree();
		
	}
}
