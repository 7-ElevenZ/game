using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElevenZ.Core
{
    public partial class AudioManager : Node
    {
        public enum AudioBus
        {
            Master,
            Music,
            Sound    
        }

        public static AudioManager Instance { get; private set; }

        public override void _Ready()
        {
            foreach (var kv in BusNames)
            {
                var name = kv.Value;
                var index = AudioServer.GetBusIndex(name);

                GD.Print($"Bus name: {name}, index: {index}");

                if (index == -1)
                {
                    GD.PushWarning($"Audio bus \"{name}\" not found, some audio settings may not work.");
                    continue;
                }

                var audioBus = kv.Key;
                BusIndexes[audioBus] = index;
            }

            Instance = this;
        }

        [ExportCategory("Nodes")]

        [Export]
        public AudioStreamPlayer MusicPlayer { get; set; }


        [Export]
        public AudioStreamPlayer SoundPlayer { get; set; }

        [ExportCategory("Audio Buses")]

        [Export]
        public Godot.Collections.Dictionary<AudioBus, string> BusNames { get; set; }

        public Dictionary<AudioBus, int> BusIndexes { get; set; } = [];

        public void PlayMusic(AudioStream stream, float volume = 1f)
        {
            if (MusicPlayer == null)
            {
                GD.PushWarning("Music player was not attached in the Inspector.");
                return;
            }

            MusicPlayer.Stream = stream;
            MusicPlayer.VolumeLinear = volume;

            if (MusicPlayer.Playing)
                MusicPlayer.Stop();

            MusicPlayer.Play();
        }

        public async Task PlaySoundAndWait(AudioStream stream, float pitch = 1f, float volume = 1f)
        {
            if (SoundPlayer == null)
            {
                GD.PushWarning("Sound player was not attached in the Inspector.");
                return;
            }

            PlaySound(stream, pitch, volume);
            await ToSignal(SoundPlayer, AudioStreamPlayer.SignalName.Finished);
        }

        public void PlaySound(AudioStream stream, float pitch = 1f, float volume = 1f)
        {
            if (SoundPlayer == null)
            {
                GD.PushWarning("Sound player was not attached in the Inspector.");
                return;
            }

            SoundPlayer.Stream = stream;
            SoundPlayer.PitchScale = pitch;
            SoundPlayer.VolumeLinear = volume;

            if (SoundPlayer.Playing)
                SoundPlayer.Stop();

            SoundPlayer.Play();
        }

        public void SetBusVolume(AudioBus bus, float volume)
        {
            if (!BusIndexes.TryGetValue(bus, out var index))
            {
                GD.PushWarning($"Failed to get {bus} bus");
                return;
            }

            var clamped = Mathf.Clamp(volume, 0, 1);
            AudioServer.SetBusVolumeLinear(index, clamped);
        }
    }
}