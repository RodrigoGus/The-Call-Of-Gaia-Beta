using Godot;

public partial class texture_health_bar : TextureProgressBar
{

	public override void _Ready()
	{
		Value = Globals.player_life;
	}

	public override void _Process(double delta)
	{
		Value = Globals.player_life;
	}
}
