
using Godot;

public partial class Globals : Node
{
    public static int default_coins = 0;
    public static int default_score = 0;
    public static int default_player_life = 10;
    public static Vector2 default_position = new Vector2(775, 12);
    public static int coins = 0;
    public static int score = 0;
    public static int player_life = 10;
    public static Vector2 player_position;
    public static int hours;
    public static int minutes;
    public static int seconds;

    public override void _Ready()
    {
        checkpoint.load_game();
        player.position = player_position;
    }
    
}

