using Godot;
using Godot.Collections;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class title_screen : Control
{
	[Export] public NodePath debugSaveNotFoundPath;
	private const string SaveFilePath = "user://savegame.txt";
	private const string FirstLevelPath = "res://levels/World1.tscn";

	public override void _Ready()
	{
		// Setup code if needed
		Globals.playerLife = 1;
		Globals.coins = 0;
		Globals.score = 0;
		Player.isDeath = false;
		AishaCat.isDeath = false;
	}

	public override void _Process(double delta)
	{
		// Frame-by-frame code if needed
	}

	// Método chamado quando o botão de novo jogo é pressionado
	private void OnNewGameBtnPressed()
	{
		CreateSaveGame();
		GetTree().ChangeSceneToFile(FirstLevelPath);
	}

	// Método chamado quando o botão de carregar jogo é pressionado
	private void OnLoadGameBtnPressed()
	{
		if (FileAccess.FileExists(SaveFilePath))
		{
			LoadGame();
			GetTree().ChangeSceneToFile(FirstLevelPath);
		}
		else
		{
			GetNode<Label>(debugSaveNotFoundPath).Visible = true;
		} 
	}

	// Método chamado quando o botão de sair é pressionado
	private void OnQuitBtnPressed()
	{
		GetTree().Quit();
	}
	
	// Cria um novo arquivo de save
	public void CreateSaveGame()
	{
		var firstSaveDic = new Dictionary
		{
			{"username", "Anderson"},
			{"level", "World1.tscn"},
			{"position_X", Globals.playerPosition.X},
			{"position_Y", Globals.playerPosition.Y},
			{"coins", Globals.coins},
			{"score", Globals.score},
			{"player_life", Globals.playerLife},
			{"game_time_hours", Globals.hours},
			{"game_time_minutes", Globals.minutes},
			{"game_time_seconds", Globals.seconds}
		};
		var save_game = FileAccess.Open(SaveFilePath, FileAccess.ModeFlags.Write);
		var json_string = Json.Stringify(firstSaveDic);
		save_game.StoreLine(json_string);
		save_game.Close();
	}

	public void LoadGame()
	{
			var saveFile = FileAccess.Open(SaveFilePath, FileAccess.ModeFlags.Read);
			var jsonString = saveFile.GetLine();
			var json = new Json();
			json.Parse(jsonString);
			var nodeData = new Dictionary<string, float>((Dictionary)json.Data);

			Globals.playerPosition = new Vector2(nodeData["position_X"], nodeData["position_Y"]);
			Globals.coins = (int)nodeData["coins"];
			Globals.score = (int)nodeData["score"];
			Globals.playerLife = (int)nodeData["player_life"];
			Globals.hours = (int)nodeData["game_time_hours"];
			Globals.minutes = (int)nodeData["game_time_minutes"];
			Globals.seconds = (int)nodeData["game_time_seconds"];
	}
}

