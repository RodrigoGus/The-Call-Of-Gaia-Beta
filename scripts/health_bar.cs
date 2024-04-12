using Godot;
using System;

public partial class health_bar : ProgressBar
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Value = globals.player_life;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Value = globals.player_life;
	}
}
