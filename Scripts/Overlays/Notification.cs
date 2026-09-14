using ElevenZ.Assets;
using ElevenZ.Core;
using Godot;
using System;
using System.Drawing;

namespace ElevenZ.Overlays
{
	/*
		internal methods, contributors should use NotifcationManager.ShowNotification()
	*/
	public partial class Notification : CanvasLayer
	{
		[Export]
		public Label TitleLabel { get; set; }

		[Export]
		public RichTextLabel ContentLabel { get; set; }

		[Export]
		public Panel MainPanel { get; set; }

		private Tween _tween;

        public override async void _Ready()
        {
			GetViewport().SetInputAsHandled();

            await GameManager.Instance.Wait(1);
			Dismiss();
        }

		private void Close()
		{
			_tween.Finished -= Close;
			QueueFree();
		}

		private void Dismiss()
		{
			if (!IsInstanceValid(this)) return;

			_tween = TweenManager.Instance.CreateTweenForNode(
				MainPanel,
				Tween.EaseType.InOut,
				Tween.TransitionType.Sine
			);

			_tween.TweenProperty(MainPanel, "modulate", new Godot.Color(1, 1, 1, 0), 2);
			_tween.Finished += Close;
		}

		public void SetContent(string content)
			=> ContentLabel.Text = content;

		public void SetTitle(string title)
			=> TitleLabel.Text = title;
	}
}