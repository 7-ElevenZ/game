using ElevenZ.Assets;
using ElevenZ.Core;
using Godot;

namespace ElevenZ.UI.Effects
{
    public partial class ButtonEffects : Button
    {
        [ExportCategory("Sounds")]

        [Export]
        public AudioStream ButtonDownSound { get; set; } = AudioLibrary.ButtonDown;

        [Export]
        public AudioStream ButtonUpSound { get; set; } = AudioLibrary.ButtonUp;

        [ExportCategory("Tweening")]

        [Export]
        public float MouseOverScale = 0.025f;

        [Export]
        public float ButtonDownScale = 0.025f;

        private Tween _tween;
        private Vector2 _originalScale;

        public override async void _Ready()
        {
            _originalScale = Scale;

            ButtonDown += OnButtonDown;
            ButtonUp += OnButtonUp;

            MouseEntered += OnMouseOver;
            MouseExited += ResetScale;

            await ToSignal(GetTree(), "process_frame");

            PivotOffset = Size / 2;
        }

        private void ResetScale()
        {
            _tween = TweenManager.Instance.CreateTweenForNode(this, Tween.EaseType.Out, Tween.TransitionType.Cubic);
            _tween.TweenProperty(this, "scale", _originalScale, 0.35);
        }

        private void OnMouseOver()
        {
            _tween = TweenManager.Instance.CreateTweenForNode(this, Tween.EaseType.Out, Tween.TransitionType.Cubic);

            var newScale = new Vector2(_originalScale.X + MouseOverScale, _originalScale.Y + MouseOverScale);
            _tween.TweenProperty(this, "scale", newScale, 0.35);
        }

        private void OnButtonUp()
        {
            AudioManager.Instance.PlaySound(ButtonUpSound);
            ResetScale();
        }

        private void OnButtonDown()
        {
            AudioManager.Instance.PlaySound(ButtonDownSound);

            _tween = TweenManager.Instance.CreateTweenForNode(this, Tween.EaseType.Out, Tween.TransitionType.Cubic);

            var newScale = new Vector2(_originalScale.X - ButtonDownScale, _originalScale.Y - ButtonDownScale);
            _tween.TweenProperty(this, "scale", newScale, 0.35);
        }
    }
}