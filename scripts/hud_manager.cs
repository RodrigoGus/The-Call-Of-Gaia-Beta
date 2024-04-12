using Godot;
using System;

public partial class hud_manager : Control
{
	public Label _coins_counter;
	// public Label _score_counter;
	// public Label _life_counter;
	// public Label _timer_counter;


	public override void _Ready()
	{
		_coins_counter = GetNode<Label>("/root/world1/HUD/control/container/coins_container/coins_counter");
		// _score_counter = GetNode<Label>("score_counter");
		// _life_counter = GetNode<Label>("/root/world1/HUD/control/container/life_container/life_counter");
		// _timer_counter = GetNode<Label>("timer_counter");

		_coins_counter.Text = globals.coins.ToString();
		// _score_counter.Text = globals.score.ToString();
		// _life_counter.Text = globals.player_life.ToString();
	}

	public override void _Process(double delta)
	{
		_coins_counter.Text = globals.coins.ToString();
		// _life_counter.Text = globals.player_life.ToString();
	}
}
