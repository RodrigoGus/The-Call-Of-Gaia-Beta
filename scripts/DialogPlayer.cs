using Godot;
using System;
using Godot.Collections;

public partial class DialogPlayer : CanvasLayer
{
	[Export(PropertyHint.File, "*.json")]
	public string SceneTextFile;
	public Dictionary<string, string> sceneText = new();
	public Array<string> selectedText = new();
	public bool inProgress = false;
	public TextureRect background;
	public Label textLabel;
	public SignalBus signalBus;
	public override void _Ready()
	{
		background = GetNode<TextureRect>("Background");
		textLabel = GetNode<Label>("TextLabel");
		background.Visible = false;
		sceneText = LoadSceneText();
		signalBus = GetNode<SignalBus>("/root/SignalBus");
		signalBus.Connect("DisplayDialog", new Callable(this, nameof(OnDisplayDialog)));

	}

	public override void _Process(double delta)
	{
	}

	public Dictionary<string, string> LoadSceneText(){
		FileAccess file = FileAccess.Open(SceneTextFile, FileAccess.ModeFlags.Read);
		var jsonString = file.GetAsText();
		var json = new Json();
		var parseResult = json.Parse(jsonString);
		var nodeData = new Dictionary<string, string>((Dictionary)json.Data);
		return nodeData;
	}

	private void ShowText()
	{
		textLabel.Text = selectedText[0];
		selectedText.RemoveAt(0);
	}

	private void NextLine()
	{
		if (selectedText.Count > 0)
		{
			ShowText();
		}
		else
		{
			Finish();
		}
	}

	private void Finish()
	{
		textLabel.Text = "";
		background.Visible = false;
		inProgress = false;
		GetTree().Paused = false;
	}

	public void OnDisplayDialog(string textKey)
	{
		if(inProgress){
			NextLine();
		} else {
			GetTree().Paused = true;
			background.Visible = true;
			inProgress = true;
			selectedText.Add(sceneText[textKey]);
			ShowText();
		}
	}
}
