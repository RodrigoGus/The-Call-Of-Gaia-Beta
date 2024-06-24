using Godot;
using MySqlConnector;

public partial class UserSession : Node
{
    public string userSessionEmail;
    public string passwordUserSession;
    public static bool isLogin = false;
	public static MySqlConnection conn;
	public string connectionString = "Server=tcog_db.mysql.dbaas.com.br;Database=tcog_db;Uid=tcog_db;Pwd=tcogdb@T1";

    public override void _Ready()
    {
        conn = new MySqlConnection(connectionString);
    }
}