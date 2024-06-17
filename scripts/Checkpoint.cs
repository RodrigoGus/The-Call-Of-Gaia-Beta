using Godot;
using Godot.Collections;
using MySqlConnector;
using System;

public partial class Checkpoint : Area2D
{
	private const string PlayerGroupName = "players";
	private const string SaveFilePath = "user://savegame.txt";
	private MySqlConnection conn;
	public string connectionString = "server=tcog_db.mysql.dbaas.com.br;user=tcog_db;database=tcog_db;password=tcogdb@T1";

	public override void _Ready()
	{
		conn = new MySqlConnection(connectionString);
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
			SaveGameToDatabase(saveData);
		}
	}

	private Dictionary CreateSaveData()
	{
		return new Dictionary
		{
			{"level", GetTree().CurrentScene.Name},
			{"position_X", Globals.playerPosition.X},
			{"position_Y", Globals.playerPosition.Y},
			{"coins", Globals.coins},
			{"score", Globals.score},
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

	private void SaveGameToDatabase(Dictionary saveData)
	{
		try
		{
			conn.Open();
			string query = @"UPDATE UserStats 
							SET level = @level, 
								position_X = @position_X, 
								position_Y = @position_Y, 
								coins = @coins, 
								score = @score, 
								game_time_hours = @game_time_hours, 
								game_time_minutes = @game_time_minutes, 
								game_time_seconds = @game_time_seconds 
							WHERE user_email = @userEmail";

			using (var cmd = new MySqlCommand(query, conn))
			{
				cmd.Parameters.AddWithValue("@level", saveData["level"].ToString());
				cmd.Parameters.AddWithValue("@position_X", (int)saveData["position_X"]);
				cmd.Parameters.AddWithValue("@position_Y", (int)saveData["position_Y"]);
				cmd.Parameters.AddWithValue("@coins", (int)saveData["coins"]);
				cmd.Parameters.AddWithValue("@score", (int)saveData["score"]);
				cmd.Parameters.AddWithValue("@game_time_hours", (int)saveData["game_time_hours"]);
				cmd.Parameters.AddWithValue("@game_time_minutes", (int)saveData["game_time_minutes"]);
				cmd.Parameters.AddWithValue("@game_time_seconds", (int)saveData["game_time_seconds"]);

				var userSession = (UserSession)GetNode("/root/UserSession");
				var userEmail = userSession.userSessionEmail;
				cmd.Parameters.AddWithValue("@userEmail", userEmail);

				cmd.ExecuteNonQuery();
			}
			GD.Print("Checkpoint salvo com sucesso no banco de dados!");
		}
		catch (Exception e)
		{
			GD.PrintErr("Erro ao salvar checkpoint no banco de dados: ", e.Message);
			GD.PrintErr("Stack Trace: ", e.StackTrace);
		}
		finally
		{
			conn.Close();
		}
	}

}
