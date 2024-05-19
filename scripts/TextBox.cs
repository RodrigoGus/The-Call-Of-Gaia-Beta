using Godot;
using System;

public partial class TextBox : MarginContainer
{
	private bool interactTextBoxAreaBodyEntered = false;
	private interact_text_box_area interactTextBoxArea;
	private CanvasLayer ui;
	private Label label;
	private Timer timer;
	private const int MaxWidth = 100;

	private string text;
	private int letterIndex = 0;
	private float letterTime = 0.03f;
	private float spaceTime = 0.06f;
	private float punctuationTime = 0.2f;

	public override void _Ready()
	{
		interactTextBoxArea = GetParent().GetParent<interact_text_box_area>();
		ui = GetParent().GetParent().GetNode<CanvasLayer>("CanvasLayer");
		label = GetNode<Label>("MarginContainer/Label");
		timer = GetNode<Timer>("LetterDisplayTimer");
		timer.Timeout += OnLetterDisplayTimerTimeout;
	}

	public override void _Process(double delta)
	{
		if (interactTextBoxAreaBodyEntered)
		{
			if (Input.IsActionJustPressed("interact"))
			{
				if (!ui.Visible)
				{
					text = interactTextBoxArea.text;
					ui.Visible = true;
					DisplayText(text);
				}
				else
				{
					ui.Visible = false;
				}
			}
		} else{
			ui.Visible = false;
		}
	}

	private void DisplayText(string textToDisplay)
	{
		label.Text = "";
		letterIndex = 0;
		text = textToDisplay;
		ScheduleNextLetter();
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
				delay = punctuationTime;
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

	private void OnInteractTextBoxAreaBodyEntered(Node2D body)
	{
		interactTextBoxAreaBodyEntered = true;
	}

	private void OnInteractTextBoxAreaBodyExited(Node2D body)
	{
		interactTextBoxAreaBodyEntered = false;
	}


}

