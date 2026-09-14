using ElevenZ.Assets;
using ElevenZ.UI;
using Godot;
using System;
using System.Threading.Tasks;

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

        public void ShowOptions()
        {
            var optionsScene = GameResourceManager.Instance.GetResource<PackedScene>("options");
            if (optionsScene == null)
            {
                GD.PushWarning("Cannot show options: Options scene has not been loaded.");
                return;
            }

            OverlayManager.Instance.AddOverlay(optionsScene, 2);
        }

        public void Clear()
            => OverlayManager.Instance.ClearOverlays();

        public async Task Wait(double timeout)
        {
            var timer = GetTree().CreateTimer(timeout);
            await ToSignal(timer, Timer.SignalName.Timeout);
        }
    }
}