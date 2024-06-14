using Godot;
using Godot.Collections;

public partial class Checkpoint : Area2D
{
	private const string PlayerGroupName = "players";
	private const string SaveFilePath = "user://savegame.txt";

	public override void _Ready()
	{

	}

	public override void _Process(double delta)
	{
	}

	private void _on_body_entered(Node2D body)
	{
		if (body.IsInGroup(PlayerGroupName))
		{
			Globals.playerPosition = Position;
			var saveData = CreateSaveData();
			SaveGameToFile(saveData);
		}
	}

	private Dictionary CreateSaveData()
	{
		return new Dictionary
		{
			{"username", "Anderson"},
			{"level", GetTree().CurrentScene.Name},
			{"position_X", Globals.playerPosition.X},
			{"position_Y", Globals.playerPosition.Y},
			{"coins", Globals.coins},
			{"score", Globals.score},
			{"player_life", Globals.playerLife},
			{"game_time_hours", Globals.hours},
			{"game_time_minutes", Globals.minutes},
			{"game_time_seconds", Globals.seconds}
		};
	}

	private static void SaveGameToFile(Dictionary saveData)
	{
		var saveFile = FileAccess.Open(SaveFilePath, FileAccess.ModeFlags.Write);
		var jsonString = Json.Stringify(saveData);
		saveFile.StoreLine(jsonString);
		saveFile.Close();
	}
}
