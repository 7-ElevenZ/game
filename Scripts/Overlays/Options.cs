using ElevenZ.Core;
using Godot;
using System;

namespace ElevenZ.Overlays
{
	public partial class Options : CanvasLayer
	{
		[ExportCategory("Buttons")]

		[Export]
		public Button ApplyButton { get; set; }

		[Export]
		public Button DiscardButton { get; set; }

		[Export]
		public Button CloseButton { get; set; }

		private bool _settingsChanged;

        public override void _Ready()
        {
            DisableButtons();

			ConfigManager.Instance.SettingChanged += OnSettingChanged;

			ApplyButton.Pressed += ApplySettings;
			DiscardButton.Pressed += DiscardSettings;
			CloseButton.Pressed += Close;
        }

        public override void _ExitTree()
        {
            ConfigManager.Instance.SettingChanged -= OnSettingChanged;
        }

		private void OnSettingChanged()
		{
			_settingsChanged = true;
			EnableButtons();
		}

		private void Close()
		{
			if (_settingsChanged)
			{
				NotificationManager.Instance.ShowNotification(
					"Please apply or discard first!",
					"You [i]must[/i] apply or discard your settings to continue."
				);

				return;
			}

			QueueFree();
		}

		private void ApplySettings()
		{
			ConfigManager.Instance.ApplySettings();
			GD.Print("Applying settings");

			_settingsChanged = false;
			DisableButtons();
		}

		private void DiscardSettings()
		{
			ConfigManager.Instance.DiscardSettings();
			GD.Print("Discarding settings");

			_settingsChanged = false;
			DisableButtons();
		}

		private void EnableButtons()
		{
			ApplyButton.Disabled = false;
			DiscardButton.Disabled = false;
		}

		private void DisableButtons()
		{
			ApplyButton.Disabled = true;
			DiscardButton.Disabled = true;
		}
	}
}