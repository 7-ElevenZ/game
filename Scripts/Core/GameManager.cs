using ElevenZ.Assets;
using Godot;
using System;

namespace ElevenZ.Core
{
    public partial class GameManager : Node
    {
        public static GameManager Instance { get; private set; }

        public override async void _Ready()
        {
            Instance = this;
        }

        public void QuitGame()
        {
            GetTree().Root.PropagateNotification((int)NotificationWMCloseRequest);
        }
    }
}