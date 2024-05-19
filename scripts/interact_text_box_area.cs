using Godot;
using System;

public partial class interact_text_box_area : Area2D
{
	[Export]
	public string text;
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
