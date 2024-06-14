
using Godot;

public partial class Globals : Node
{
    public static int defaultCoins = 0;
    public static int defaultScore = 0;
    public static int defaultPlayer_life = 10;
    public static Vector2 defaultPosition = new Vector2(775, 12);
    public static int coins = 0;
    public static int score = 0;
    public static int playerLife = 10;
    public static Vector2 playerPosition;
    public static int hours;
    public static int minutes;
    public static int seconds;

    public override void _Ready()
    {
    }
    
}

