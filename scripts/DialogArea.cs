using Godot;
using System;

public partial class DialogArea : Area2D
{
	[Export]
	public string dialogKey = "";
	public static bool areaActive = false;
	public SignalBus signalBus; 
	public override void _Ready()
	{
		signalBus = GetNode<SignalBus>("/root/SignalBus");
	}

	public override void _Process(double delta)
	{
		if (areaActive && Input.IsActionJustPressed("interact"))
		{
			signalBus.EmitSignal(nameof(SignalBus.DisplayDialog), dialogKey);
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
