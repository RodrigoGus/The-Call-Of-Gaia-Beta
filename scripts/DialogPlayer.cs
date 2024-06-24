using Godot;
using System;
using Godot.Collections;

public partial class DialogPlayer : CanvasLayer
{
	[Export(PropertyHint.File, "*.json")]
	public string SceneTextFile;
	public Dictionary<string, Array<string>> sceneText = new();
	public Array<string> selectedText = new();
	public bool inProgress = false;
	public Sprite2D actorImage;
	public TextureRect background;
	public Label textLabel;
	public SignalBus signalBus;
	public AnimationPlayer aP;
	public override void _Ready()
	{
		aP = GetNode<AnimationPlayer>("AnimationPlayer");
		actorImage = GetNode<Sprite2D>("Sprite2D");
		background = GetNode<TextureRect>("Background");
		textLabel = GetNode<Label>("TextLabel");
		actorImage.Visible = false;
		background.Visible = false;
		sceneText = LoadSceneText();
		signalBus = GetNode<SignalBus>("/root/SignalBus");
		signalBus.Connect("DisplayDialog", new Callable(this, nameof(OnDisplayDialog)));
		
	}

	public Dictionary<string, Array<string>> LoadSceneText(){
		FileAccess file = FileAccess.Open(SceneTextFile, FileAccess.ModeFlags.Read);
		var jsonString = file.GetAsText();
		var json = new Json();
		var parseResult = json.Parse(jsonString);
		var nodeData = new Dictionary<string, Array<string>>((Dictionary)json.Data);
		return nodeData;

	}

	private async void ShowText()
	{
		string text = selectedText[0];
		selectedText.RemoveAt(0);
		textLabel.Text = "";

		foreach (char letter in text)
		{
			textLabel.Text += letter;
			await ToSignal(GetTree().CreateTimer(0.05f), "timeout");
		}
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
		aP.Play("close");

	}

	public void OnDisplayDialog(string textKey)
	{
		if(inProgress){
			NextLine();
		} else {
			GetTree().Paused = true;
			aP.Play("open");
			actorImage.Visible = true;
			background.Visible = true;
			inProgress = true;
			selectedText = sceneText[textKey].Duplicate();
			ShowText();
		}
	}

	private void OnAnimationPlayerAnimationFinished(StringName anim_name)
	{
		if(anim_name == "close"){
			actorImage.Visible = false;
			background.Visible = false;
			inProgress = false;
			GetTree().Paused = false;
		}
	}
}
