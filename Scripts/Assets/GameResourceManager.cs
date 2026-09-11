using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElevenZ.Assets
{
    public partial class GameResourceManager : Node
    {
        public static GameResourceManager Instance { get; private set; }

        public override void _Ready()
        {
            Instance = this;
        }

        public Dictionary<string, Resource> Resources { get; private set; }
            = [];

        public void LoadResources(Action<float> onProgress, params (string name, string path)[] resources)
        {
            var total = resources.Length;

            if (total == 0)
            {
                onProgress?.Invoke(100f);
                return;
            }

            for (int i = 0; i < total; i++)
            {
                var resource = resources[i];
                LoadResource<Resource>(resource.name, resource.path);

                var percentage = (i + 1) / (float)total * 100f;

                onProgress?.Invoke(percentage);
            }
        }

        public void LoadResource<T>(string name, string path) where T : Resource
        {
            if (Resources.ContainsKey(name))
            {
                GD.PushWarning($"Resource key \"{name}\" already taken");
                return;
            }

            var resource = GD.Load<T>(path);
            Resources[name] = resource;
        }

        public T GetResource<T>(string name) where T : Resource
        {
            if (!Resources.TryGetValue(name, out var res))
            {
                GD.PushWarning($"Resource \"{name}\" not found");
                return null;
            }

            return (T)res;
        }
    }
}