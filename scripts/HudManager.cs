using Godot;

public partial class HudManager : Control
{
	public Label _coins_counter;
	public Timer _clock_timer;

	[Export(PropertyHint.Range, "0,59,")]
	private int default_minutes = 0;

	[Export(PropertyHint.Range, "0,59,")]
	private int default_seconds = 0;
	public Label _timer_counter;


	public override void _Ready()
	{
		_coins_counter = GetNode<Label>("/root/World1/Hud/control/coins_counter");
		_clock_timer = GetNode<Timer>("/root/World1/Hud/control/clock_timer");
		_timer_counter = GetNode<Label>("/root/World1/Hud/control/timer_container/timer_counter");
		_coins_counter.Text = Globals.coins.ToString();
		_timer_counter.Text = Globals.minutes + ":" + Globals.seconds;


	}

	public override void _Process(double delta)
	{
		_coins_counter.Text = Globals.coins.ToString();
	}

	private void _on_clock_timer_timeout()
	{

		if (Globals.seconds == 60)
		{
			Globals.minutes += 1;
			Globals.seconds = 0;

		}
		if (Globals.minutes == 60)
		{
			Globals.hours += 1;
			Globals.minutes = 0;
			Globals.seconds = 0;
		}
		Globals.seconds += 1;
		_timer_counter.Text = Globals.minutes.ToString("D2") + ":" + Globals.seconds.ToString("D2");
	}
}



