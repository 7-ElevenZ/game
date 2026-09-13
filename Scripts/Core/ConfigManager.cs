using Godot;
using System;

namespace ElevenZ.Core
{
    public partial class ConfigManager : Node
    {
        public const string ConfigFilePath = "user://preferences.cfg";

        public static ConfigManager Instance { get; set; }

        public override void _Ready()
        {
            CurrentConfigFile = new ConfigFile();
            LoadConfig();

            Instance = this;

            GetTree().AutoAcceptQuit = false;
        }

        public override void _Notification(int what)
        {
            if (what == NotificationWMCloseRequest)
            {
                Save();
                GetTree().Quit();
            }
        }

        public ConfigFile CurrentConfigFile { get; set; }

        private void LoadConfig()
        {
            var error = CurrentConfigFile.Load(ConfigFilePath);
            if (error != Error.Ok)
            {
                GD.PushWarning("Failed to load config file");
                return;
            }
        }

        public T GetSetting<[MustBeVariant] T>(string section, string key, Variant defaultValue = default)
        {
            var variant = CurrentConfigFile.GetValue(section, key, defaultValue);
            return variant.As<T>();
        }

        public void SetSetting(string section, string key, Variant value)
            => CurrentConfigFile.SetValue(section, key, value);

        public Error Save()
            => CurrentConfigFile.Save(ConfigFilePath);
    }
}
