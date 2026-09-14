using ElevenZ.Assets;
using ElevenZ.Core;
using Godot;

namespace ElevenZ.Scenes
{
    public partial class MainMenu : CanvasLayer
    {
        public AudioStream MenuSong { get; set; }


		[ExportCategory("Buttons")]

		[Export]
		public Button OptionsButton { get; set; }

		[Export]
		public Button QuitButton { get; set; }

		private void ImplementButtons()
		{
			if (QuitButton == null)
			{
				GD.PushWarning("Quit button was not attached in the Inspector.");
				return;
			}

			if (OptionsButton == null)
			{
				GD.PushWarning("Options button was not attached in the Inspector.");
				return;
			}

			if (OS.HasFeature("ios"))
				QuitButton.Hide();
			else
				QuitButton.Pressed += GameManager.Instance.QuitGame;

			OptionsButton.Pressed += GameManager.Instance.ShowOptions;
		}

        public override async void _Ready()
        {
            MenuSong ??= GameResourceManager.Instance.GetResource<AudioStream>("menu");

            AudioManager.Instance.PlayMusic(MenuSong);

			ImplementButtons();
        }
    }
}