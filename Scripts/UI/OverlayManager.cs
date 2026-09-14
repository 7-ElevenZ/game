using Godot;
using System;

namespace ElevenZ.UI
{
    public partial class OverlayManager : Node
    {
        public static OverlayManager Instance { get; private set; }

        public override void _Ready()
        {
            Instance = this;
        }

        public void AddOverlay(PackedScene overlay, int layer = 1)
        {
            var instance = overlay.Instantiate();
            GD.Print($"Adding overlay \"{instance.Name}\"");

            if (instance is CanvasLayer canvasLayer) canvasLayer.Layer = layer;

            AddChild(instance);
        }

        public void ClearOverlays()
        {
            foreach (var child in GetChildren())
            {
                if (child.IsQueuedForDeletion())
                {
                    GD.Print($"Node {child.Name} is queued for deletion.");
                    continue;    
                }

                child.QueueFree();
            }
        }
    }
}
