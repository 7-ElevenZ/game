using ElevenZ.Assets;
using ElevenZ.Overlays;
using ElevenZ.UI;
using Godot;
using System;

namespace ElevenZ.Core
{
    public partial class NotificationManager : Node
    {
        public static NotificationManager Instance { get; private set; }

        public override void _Ready()
        {
            Instance = this;
        }

        private PackedScene _notification;
        private bool _initialized;

        private void Initialize()
        {
            if (_initialized)
                return;

            _notification = GameResourceManager.Instance.GetResource<PackedScene>("notification");
            if (_notification == null)
            {
                GD.PushWarning("Notification scene failed to load.");
                return;    
            }

            _initialized = true;
        }

        public void ShowNotification(string title, string text)
        {
            Initialize();

            var instance = _notification.Instantiate();
            
            if (instance is Notification notification)
            {
                notification.SetTitle(title);
                notification.SetContent(text);
            }

            AddChild(instance);
        }
    }
}
