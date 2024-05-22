using Godot;
using Godot.Collections;

public partial class DialogPlayer : CanvasLayer
{
    [Export(PropertyHint.File, "*.json")]
    public string SceneTextFile;
	public Dictionary<string, string> sceneText = new Dictionary<string, string>();
	public Array selectedText = new Array();
	public bool inProgress = false;
	public TextureRect background;
	public Label textLabel;


	public override void _Ready()
	{
		background = GetNode<TextureRect>("Background");
		textLabel = GetNode<Label>("Text");
		background.Visible = false;
		sceneText = LoadSceneText();
        SignalBus.Connect("DisplayDialog", this, nameof(OnDisplayDialog));
	}

	public override void _Process(double delta)
	{
	}

	public Dictionary<string, string> LoadSceneText()
	{
		Dictionary<string, string> nodeData = new Dictionary<string, string>();
		if (FileAccess.FileExists("user://dialog texts/teste.json")){
			FileAccess file = FileAccess.Open("user://dialog texts/teste.json", FileAccess.ModeFlags.Read);
			var jsonString = file.GetAsText();
			var json = new Json();
			var parseResult = json.Parse(jsonString);
			nodeData = new Dictionary<string, string>((Dictionary)json.Data);
			
		};
		return nodeData;
		
	}

	public void OnDisplayDialog(string textKey){
		if(inProgress){
			NextLine();
		} else {
			GetTree().Paused = true;
			background.Visible = true;
			inProgress = true;
			selectedText = sceneText[textKey].;
		}
	}
}
