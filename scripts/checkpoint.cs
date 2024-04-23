using Godot;
using Godot.Collections;


public partial class checkpoint : Area2D
{

	public override void _Ready()
	{
	}


	public override void _Process(double delta)
	{
	}

	private void _on_body_entered(Node2D body)
	{
		if (body.IsInGroup("players"))
		{
			GD.Print("checkpoint");
			Globals.player_position = Position;
			save();
			save_game();
		}
	}
	public static Dictionary save(){
		Dictionary save_dictionary = new Dictionary{
			{"username", "Anderson"},
	 		{"position_X", Globals.player_position.X},
			{"position_Y", Globals.player_position.Y},
			{"coins" , Globals.coins},
			{"score", Globals.score},
			{"player_life", Globals.player_life},
			{"game_time_hours", Globals.hours},
			{"game_time_minutes", Globals.minutes},
			{"game_time_seconds", Globals.seconds},
		};
		return save_dictionary;
		
	}

	public static void save_game(){
		var save_game = FileAccess.Open("user://savegame.txt", FileAccess.ModeFlags.Write);
		var json_string = Json.Stringify(save());
		save_game.StoreLine(json_string);
		save_game.Close();
	}

	public static void load_game(){
		if(!FileAccess.FileExists("user://savegame.txt")){
			GD.Print("no savegame");
			save_game();
		}else{
			FileAccess save_game = FileAccess.Open("user://savegame.txt", FileAccess.ModeFlags.Read);
			var jsonString = save_game.GetLine();
			var json = new Json();
			var parseResult = json.Parse(jsonString);
			var nodeData = new Dictionary<string, float>((Dictionary)json.Data);
			Globals.player_position = new Vector2(nodeData["position_X"], nodeData["position_Y"]);
			Globals.coins = (int)nodeData["coins"];
			Globals.score = (int)nodeData["score"];
			Globals.player_life = (int)nodeData["player_life"];
			Globals.hours = (int)nodeData["game_time_hours"];
			Globals.minutes = (int)nodeData["game_time_minutes"];
			Globals.seconds = (int)nodeData["game_time_seconds"];
		}

	}
	
}


