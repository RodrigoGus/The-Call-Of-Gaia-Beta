
using Godot;
using Godot.Collections;


public partial class Globals : Node
{
    public static int coins = 0;
    public static int score = 0;
    public static int player_life = 10;
    public static Vector2 position = new Vector2(775, 12);

    public override void _Ready()
    {
        save_game();
    }
    public Dictionary save(){
        Dictionary save_dictionary = new Dictionary{
            {"username", "Anderson"},
            {"position", position},
            {"coins" , 5},
            {"score", 5},
            {"player_life", 10},
            {"play_time", hud_manager.hours + ":" + hud_manager.minutes + ":" + hud_manager.seconds},
        };
        return save_dictionary;
        
    }

    public void save_game(){
        var save_game = FileAccess.Open("user://savegame.txt", FileAccess.ModeFlags.Write);
        var json_string = Json.Stringify(save());
        save_game.StoreLine(json_string);
    }

}

