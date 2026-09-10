using ElevenZ.Assets;
using ElevenZ.Core;
using Godot;
using System;

namespace ElevenZ.Scenes
{
    public partial class MainMenu : CanvasLayer
    {
        [Export]
        public AudioStream MenuSong { get; set; } = AudioLibrary.MenuTheme;

        public override void _Ready()
        {
            if (MenuSong == null)
            {
                GD.PushWarning("Menu song was not attached in the Inspector.");
                return;
            }

            AudioManager.Instance.PlayMusic(MenuSong);
        }
    }
}