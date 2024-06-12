using Godot;

public partial class Coin : Area2D
{

	public int coins = 1;
	public CollisionShape2D _collision;
	public override void _Ready()
	{
		_collision = GetNode<CollisionShape2D>("Collision");


	}

	public override void _Process(double delta)
	{
	}
	private void OnBodyEntered(Node2D body)
	{
		_collision.SetDeferred("disabled", true);
		GetNode<AnimatedSprite2D>("Anim").Play("collected");

		Globals.coins += coins;
	}
	private void OnAnimAnimationFinished()
	{
		QueueFree();
	}
}
