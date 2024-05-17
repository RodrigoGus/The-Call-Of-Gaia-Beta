using Godot;
using System;

public partial class TextBox : MarginContainer
{
	public Label label;
	public Timer timer;
	public const int MAXWIDTH = 100;
	[Export]
	public string text;
	public int letterIndex = 0;
	public float letterTime = 0.03f;
	public float spaceTime = 0.06f;
	public float pontuationTime = 0.2f;

	public override void _Ready()
	{
		label = GetNode<Label>("MarginContainer/Label");
		timer = GetNode<Timer>("LetterDisplayTimer");
		DisplayText(text);
	}
	public override void _Process(double delta)
	{
	}
	public void DisplayText(string TextToDisplay)
	{
		this.letterIndex = 0;

		if (TextToDisplay.Length > 0)
		{
			// Verificar se o texto é maior que MAXWIDTH
			if (TextToDisplay.Length > MAXWIDTH)
			{
				int lastIndex = 0;
				for (int i = 0; i < TextToDisplay.Length; i++)
				{
					if (i > 0 && i % MAXWIDTH == 0)
					{
						label.Text += '\n'; // Adicionar quebra de linha
						lastIndex = i;
					}
					label.Text += TextToDisplay[i];
				}
				// Atualizar o índice da próxima letra
				this.letterIndex = lastIndex + 1;
			}
			else
			{
				// Texto cabe em uma linha, então não há necessidade de quebra
				label.Text = TextToDisplay;
				this.letterIndex = TextToDisplay.Length;
			}

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

	private void OnInteractTextBoxAreaBodyEntered(Node2D body)
	{
		if(Input.IsActionJustPressed("E"))
		{
			DisplayText(text);
		}
	}
}






