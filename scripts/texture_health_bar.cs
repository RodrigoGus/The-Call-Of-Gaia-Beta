using Godot;

public partial class texture_health_bar : TextureProgressBar
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Value = Globals.player_life;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Value = Globals.player_life;
	}
}
