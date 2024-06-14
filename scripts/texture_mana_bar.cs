using Godot;
using System;

public partial class texture_mana_bar : TextureProgressBar
{

	public override void _Ready()
	{
		Value = Globals.playerLife;
	}

	public override void _Process(double delta)
	{
		Value = Globals.playerLife;
	}
}
