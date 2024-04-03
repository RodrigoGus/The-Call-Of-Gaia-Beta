using Godot;
using System;

public partial class world1 : Node2D
{
	private NodePath _playerPath = "player";
	public player _player;
	private NodePath cameraPath = "camera";
	public Camera2D camera;

	public override void _Ready()
	{
		this._player = GetNode<player>(_playerPath);
		this.camera = GetNode<Camera2D>(cameraPath);

		_player.FollowCamera(camera);
	}
	public override void _Process(double delta)
	{
	}
}
