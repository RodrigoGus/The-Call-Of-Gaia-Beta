using Godot;

public partial class coin : Area2D
{

    public int coins = 1;
    public CollisionShape2D _collision;
    public override void _Ready()
    {
        _collision = GetNode<CollisionShape2D>("collision");


    }

    public override void _Process(double delta)
    {
    }
    private void OnBodyEntered(Node2D body)
    {
        _collision.SetDeferred("disabled", true);
        GetNode<AnimatedSprite2D>("anim").Play("collected");

        Globals.coins += coins;
    }
    private void OnAnimAnimationFinished()
    {
        QueueFree();
    }
}
