using Godot;
using Godot.Collections;
using MySqlConnector;
using System;

public partial class title_screen : Control
{
	[Export] public NodePath debugSaveNotFoundPath;
	private const string SaveFilePath = "user://savegame.txt";
	private const string FirstLevelPath = "res://levels/World1.tscn";

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		if(UserSession.isLogin)
		{
			GetNode<Button>("LoginButton").Visible = false;
			GetNode<Button>("LogoutButton").Visible = true;
		} else
		{
			GetNode<Button>("LoginButton").Visible = true;
			GetNode<Button>("LogoutButton").Visible = false;
		}
	}

	private void OnNewGameBtnPressed()
	{
		CreateSaveGame();
		GetTree().ChangeSceneToFile(FirstLevelPath);
	}

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

	private void OnQuitBtnPressed()
	{
		GetTree().Quit();
	}
	

	public void CreateSaveGame()
	{
		Dictionary firstSaveDic = new Dictionary
		{
			{"level", "World1.tscn"},
			{"position_X", Globals.playerPosition.X},
			{"position_Y", Globals.playerPosition.Y},
			{"coins", Globals.coins},
			{"score", Globals.score},
			{"game_time_hours", Globals.hours},
			{"game_time_minutes", Globals.minutes},
			{"game_time_seconds", Globals.seconds}
		};
		var save_game = FileAccess.Open(SaveFilePath, FileAccess.ModeFlags.Write);
		var json_string = Json.Stringify(firstSaveDic);
		save_game.StoreLine(json_string);
		save_game.Close();
		if (UserSession.isLogin)
		{
			try
			{
				string query = "INSERT INTO UserStats (user_email ,level, position_X, position_Y, coins, score, game_time_hours, game_time_minutes, game_time_seconds) VALUES(@userEmail, @level, @position_X, @position_Y, @coins, @score, @game_time_hours, @game_time_minutes, @game_time_seconds)";

				using (var cmd = new MySqlCommand(query, UserSession.conn))
				{
					var userSession = (UserSession)GetNode("/root/UserSession");
					string userEmail = userSession.userSessionEmail;
					cmd.Parameters.AddWithValue("@userEmail", userEmail.ToString());
					cmd.Parameters.AddWithValue("@level", firstSaveDic["level"].ToString());
					cmd.Parameters.AddWithValue("@position_X", (int)firstSaveDic["position_X"]);
					cmd.Parameters.AddWithValue("@position_Y", (int)firstSaveDic["position_Y"]);
					cmd.Parameters.AddWithValue("@coins", (int)firstSaveDic["coins"]);
					cmd.Parameters.AddWithValue("@score", (int)firstSaveDic["score"]);
					cmd.Parameters.AddWithValue("@game_time_hours", (int)firstSaveDic["game_time_hours"]);
					cmd.Parameters.AddWithValue("@game_time_minutes", (int)firstSaveDic["game_time_minutes"]);
					cmd.Parameters.AddWithValue("@game_time_seconds", (int)firstSaveDic["game_time_seconds"]);



					cmd.ExecuteNonQuery();
				}
				GD.Print("Checkpoint salvo com sucesso no banco de dados!");
			}
			catch (Exception e)
			{
			}
			finally
			{
			}

		}
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
			Globals.hours = (int)nodeData["game_time_hours"];
			Globals.minutes = (int)nodeData["game_time_minutes"];
			Globals.seconds = (int)nodeData["game_time_seconds"];
	}
	
	private void OnLoginButtonButtonDown()
	{
		GetTree().ChangeSceneToFile("res://levels/LoginMenu.tscn");
	}

	private void OnLogoutButtonButtonDown()
	{
		UserSession.conn.Close();
		UserSession.isLogin = false;
	}
}









