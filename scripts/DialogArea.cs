using Godot;
using System;

public partial class DialogArea : Area2D
{
	[Export]
	public string dialogKey = "";
	public bool areaActive = false;
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (areaActive && Input.IsActionJustPressed("interact")){
			EmitSignal("DisplayDialog", dialogKey);
		}
	}
	private void OnAreaEntered(Area2D area)
	{
		areaActive = true;
	}


	private void OnAreaExited(Area2D area)
	{
		areaActive = false;
	}
}



