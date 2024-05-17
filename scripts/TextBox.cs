using Godot;
using System;

public partial class TextBox : MarginContainer
{
	public Label label;
	public Timer timer;
	public const int MAXWIDTH = 256;
	public string text = "";
	public int letterIndex = 0;
	public float letterTime = 0.03f;
	public float spaceTime = 0.06f;
	public float pontuationTime = 0.2f;

	public override void _Ready()
	{
		label = GetNode<Label>("MarginContainer/Label");
		timer = GetNode<Timer>("LetterDisplayTimer");
		DisplayText("oi tudo bemoi tudo bemoi tudo bemoi tudo bemoi tudo bemoi tudo bemoi tudo bemoi tudo bemoi tudo bemoi tudo bem");
	}
	public override void _Process(double delta)
	{

	}
	public void DisplayText(string TextToDisplay)
	{
		this.text = TextToDisplay;
		this.label.Text = "";
		this.letterIndex = 0;

		if (text.Length > 0)
		{
			ScheduleNextLetter();
		}
	}

	private void ScheduleNextLetter()
	{
		if (letterIndex < text.Length)
		{
			char currentChar = text[letterIndex];
			float delay = letterTime;

			if (char.IsWhiteSpace(currentChar))
			{
				delay = spaceTime;
			}
			else if (char.IsPunctuation(currentChar))
			{
				delay = pontuationTime;
			}

			timer.Start(delay);
		}
	}

	private void OnLetterDisplayTimerTimeout()
	{
		if (letterIndex < text.Length)
		{
			label.Text += text[letterIndex];
			letterIndex++;
			ScheduleNextLetter();
		}
	}
}
