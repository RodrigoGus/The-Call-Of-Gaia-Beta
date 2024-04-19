using Godot;

public partial class hud_manager : Control
{
    public Label _coins_counter;
    public Timer _clock_timer;
    public static int hours = 0;
    public static int minutes = 0;
    public static int seconds = 0;

    [Export(PropertyHint.Range, "0,59,")]
    private int default_minutes = 0;

    [Export(PropertyHint.Range, "0,59,")]
    private int default_seconds = 0;
    public Label _timer_counter;


    public override void _Ready()
    {
        _coins_counter = GetNode<Label>("/root/world1/HUD/control/container/coins_container/coins_counter");
        _clock_timer = GetNode<Timer>("/root/world1/HUD/control/clock_timer");
        _timer_counter = GetNode<Label>("/root/world1/HUD/control/container/timer_container/timer_counter");
        _coins_counter.Text = Globals.coins.ToString();
        _timer_counter.Text = default_minutes + ":" + default_seconds;

        reset_clock_timer();
    }

    public override void _Process(double delta)
    {
        _coins_counter.Text = Globals.coins.ToString();
    }

    private void _on_clock_timer_timeout()
    {

        if (seconds == 60)
        {
            minutes += 1;
            seconds = 0;

        }
        if (minutes == 60)
        {
            hours += 1;
            minutes = 0;
            seconds = 0;
        }
        seconds += 1;
        _timer_counter.Text = minutes.ToString("D2") + ":" + seconds.ToString("D2");
    }

    private void reset_clock_timer()
    {
        minutes = default_minutes;
        seconds = default_seconds;

    }
}



