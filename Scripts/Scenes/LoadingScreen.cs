using ElevenZ.Assets;
using ElevenZ.Scenes;
using Godot;
using System.Linq;
using System.Threading.Tasks;

public partial class LoadingScreen : CanvasLayer
{
	[Export]
	public Godot.Collections.Dictionary<string, string> ResourcesToLoad { get; set; } = [];

	[ExportCategory("UI")]

	[Export]
	public ProgressBar ProgressBar { get; set; }

	[ExportCategory("Scenes")]

	[Export]
	public PackedScene TargetScene { get; set; }

    public override async void _Ready()
    {
		if (ProgressBar == null)
		{
			GD.PushWarning("Progress bar was not attached in the Inspector.");
			return;
		}

		if (TargetScene == null)
		{
			GD.PushWarning("Target scene was not attached in the Inspector.");
			return;
		}

		if (ResourcesToLoad.Count == 0)
		{
			ProgressBar.Value = 100;
			GD.PushWarning("No resources to load.");

			await Transition.Instance.ChangeScene(TargetScene);
			return;
		}

        LoadAllResources();
    }

	private void LoadAllResources()
	{
		var resources = ResourcesToLoad
			.Select(kv => (kv.Key, kv.Value))
			.ToArray();

		GameResourceManager.Instance.LoadResources(
			async (percentage) =>
			{
				ProgressBar.Value = percentage;

				if (percentage >= 100)
				{
					await Transition.Instance.ChangeScene(TargetScene);
					return;
				}
			},
			resources
		);
	}
}
