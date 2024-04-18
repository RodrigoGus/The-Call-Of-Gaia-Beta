using Godot;

public partial class hud_manager : Control
{
    public Label _coins_counter;
    public Timer _clock_timer;
    public int minutes = 0;
    public int seconds = 0;




    // public int hours = 0;
    [Export(PropertyHint.Range, "0,59,")]
    private int default_minutes = 0;

    [Export(PropertyHint.Range, "0,59,")]
    private int default_seconds = 0;


    // public Label _score_counter;
    // public Label _life_counter;
    public Label _timer_counter;


    public override void _Ready()
    {
        _coins_counter = GetNode<Label>("/root/world1/HUD/control/container/coins_container/coins_counter");
        _clock_timer = GetNode<Timer>("/root/world1/HUD/control/clock_timer");
        // _score_counter = GetNode<Label>("score_counter");
        // _life_counter = GetNode<Label>("/root/world1/HUD/control/container/life_container/life_counter");
        _timer_counter = GetNode<Label>("/root/world1/HUD/control/container/timer_container/timer_counter");

        _coins_counter.Text = Globals.coins.ToString();
        _timer_counter.Text = default_minutes + ":" + default_seconds;
        // _score_counter.Text = globals.score.ToString();
        // _life_counter.Text = globals.player_life.ToString();
        reset_clock_timer();
    }

    public override void _Process(double delta)
    {
        _coins_counter.Text = Globals.coins.ToString();
        // _life_counter.Text = globals.player_life.ToString();
    }

    private void _on_clock_timer_timeout()
    {
        // Replace with function body.
        if (seconds == 60)
        {
            // if (minutes > 0){
            // 	minutes -= 1;
            // 	seconds = 60;
            // }
            minutes += 1;
            seconds = 0;

        }
        seconds += 1;
        _timer_counter.Text = minutes.ToString() + ":" + seconds.ToString();
    }

    private void reset_clock_timer()
    {
        minutes = default_minutes;
        seconds = default_seconds;

    }
}



