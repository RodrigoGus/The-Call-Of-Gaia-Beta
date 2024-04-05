using Godot;
using System;
using GLobalsNS;

public partial class hud_manager : Control
{
	public Label _coins_counter;
	public Label _score_counter;
	public Label _life_counter;
	public Label _timer_counter;


	public override void _Ready()
	{
		_coins_counter = GetNode<Label>("coins_counter");
		_score_counter = GetNode<Label>("score_counter");
		_life_counter = GetNode<Label>("life_counter");
		_timer_counter = GetNode<Label>("timer_counter");

		_coins_counter.Text = Globals.coins.ToString();
		_score_counter.Text = Globals.score.ToString();
	}

	public override void _Process(double delta)
	{
	}
}
