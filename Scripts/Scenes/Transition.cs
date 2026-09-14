using Godot;
using System;
using System.Threading.Tasks;

namespace ElevenZ.Scenes
{
	public partial class Transition : CanvasLayer
	{
		public static Transition Instance { get; private set; }

        public override void _Ready()
        {
            Instance = this;
        }


		[Export]
		public AnimationPlayer AnimPlayer { get; set; }

		private async Task SceneTransition(Action sceneChanger)
		{
			if (AnimPlayer == null)
			{
				GD.PushWarning("Animation player was not attached in the Inspector.");
				return;
			}

			AnimPlayer.Play("TransitionStart");
			await ToSignal(AnimPlayer, AnimationMixer.SignalName.AnimationFinished);

			sceneChanger.Invoke();

			AnimPlayer.Play("TransitionEnd");
		}

		public async Task ChangeScene(string path)
			=> await SceneTransition(() => GetTree().ChangeSceneToFile(path));

		public async Task ChangeScene(Node node)
			=> await SceneTransition(() => GetTree().ChangeSceneToNode(node));

		public async Task ChangeScene(PackedScene packed)
			=> await SceneTransition(() => GetTree().ChangeSceneToPacked(packed));
	}
}
