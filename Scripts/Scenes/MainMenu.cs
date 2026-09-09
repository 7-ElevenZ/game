using ElevenZ.Core;
using Godot;
using System;

public partial class MainMenu : CanvasLayer
{
	[Export]
	public AudioStream MenuSong { get; set; }

	public override void _Ready()
	{
		AudioManager.Instance.PlayMusic(MenuSong);
	}
}
