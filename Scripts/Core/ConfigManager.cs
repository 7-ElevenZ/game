using Godot;
using System;
using System.Collections.Generic;

namespace ElevenZ.Core
{
    public partial class ConfigManager : Node
    {
        public const string ConfigFilePath = "user://preferences.cfg";

        public static ConfigManager Instance { get; private set; }

        public override void _Ready()
        {
            CurrentConfigFile = new ConfigFile();

            LoadConfig();
            
            AddDefaultSettings();
            ApplySettings();

            GetTree().AutoAcceptQuit = false;

            Instance = this;
        }

        public override void _Notification(int what)
        {
            if (what == NotificationWMCloseRequest)
            {
                Save();
                GetTree().Quit();
            }
        }

        public ConfigFile CurrentConfigFile { get; private set; }
        public ConfigFile PendingConfigFile { get; private set; }
        public Action SettingChanged { get; set; }

        private void LoadConfig()
        {
            var error = CurrentConfigFile.Load(ConfigFilePath);
            if (error != Error.Ok)
            {
                GD.PushWarning("Failed to load config file");
                return;
            }

            PendingConfigFile = CurrentConfigFile;
        }

        private void AddDefaultSettings()
        {
            SetDefault("audio", "master_volume", 1f);
            SetDefault("audio", "music_volume", 1f);
            SetDefault("audio", "sound_volume", 1f);
        }

        private void SetDefault(string section, string key, Variant value)
        {
            if (HasSetting(section, key))
                return;

            SetSetting(section, key, value);
        }

        public bool HasSetting(string section, string key)
            => PendingConfigFile.HasSectionKey(section, key);

        public T GetSetting<[MustBeVariant] T>(string section, string key, T defaultValue = default)
        {
            var variant = PendingConfigFile.GetValue(section, key, Variant.From(defaultValue));
            return variant.As<T>();
        }

        public void SetSetting(string section, string key, Variant value)
        {
            PendingConfigFile.SetValue(section, key, value);
            SettingChanged?.Invoke();
        }

        public Error Save()
            => CurrentConfigFile.Save(ConfigFilePath);

        public void DiscardSettings()
        {
            PendingConfigFile = null;
        }

        public void ApplySettings()
        {
            CurrentConfigFile = PendingConfigFile;

            var masterVolume = GetSetting("audio", "master_volume", 1f);
            var musicVolume = GetSetting("audio", "music_volume", 1f);
            var soundVolume = GetSetting("audio", "sound_volume", 1f);

            AudioManager.Instance.SetBusVolume(AudioManager.AudioBus.Master, masterVolume);
            AudioManager.Instance.SetBusVolume(AudioManager.AudioBus.Music, musicVolume);
            AudioManager.Instance.SetBusVolume(AudioManager.AudioBus.Sound, soundVolume);
        }
    }
}
