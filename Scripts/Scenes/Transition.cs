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
		public AnimationPlayer AnimationPlayer { get; set; }

		private async Task SceneTransition(Action sceneChanger)
		{
			if (AnimationPlayer == null)
			{
				GD.PushWarning("Animation player was not attached in the Inspector.");
				return;
			}

			AnimationPlayer.Play("TransitionStart");
			await ToSignal(AnimationPlayer, "animation_finished");

			sceneChanger.Invoke();

			AnimationPlayer.Play("TransitionEnd");
		}

		public async Task ChangeScene(string path)
			=> await SceneTransition(() => GetTree().ChangeSceneToFile(path));

		public async Task ChangeScene(Node node)
			=> await SceneTransition(() => GetTree().ChangeSceneToNode(node));

		public async Task ChangeScene(PackedScene packed)
			=> await SceneTransition(() => GetTree().ChangeSceneToPacked(packed));
	}
}
