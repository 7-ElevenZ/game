using ElevenZ.Assets;
using ElevenZ.Core;
using Godot;
using System;

namespace ElevenZ.Scenes
{
    public partial class MainMenu : CanvasLayer
    {
		[ExportCategory("Audio")]

        [Export]
        public AudioStream MenuSong { get; set; } = AudioLibrary.MenuTheme;


		[ExportCategory("Buttons")]

		[Export]
		public Button QuitButton { get; set; }

		private void ImplementButtons()
		{
			if (QuitButton == null)
			{
				GD.PushWarning("Quit button was not attached in the Inspector.");
				return;
			}

			if (OS.HasFeature("ios"))
				QuitButton.Hide();
			else
				QuitButton.Pressed += GameManager.Instance.QuitGame;
		}

        public override void _Ready()
        {
            if (MenuSong == null)
            {
                GD.PushWarning("Menu song was not attached in the Inspector.");
                return;
            }

            AudioManager.Instance.PlayMusic(MenuSong);

			ImplementButtons();
        }
    }
}