using Godot;
using System;
using System.Threading.Tasks;

namespace ElevenZ.Core
{
    public partial class AudioManager : Node
    {
        public static AudioManager Instance { get; private set; }

        public override void _Ready()
        {
            Instance = this;
        }

        [ExportCategory("Nodes")]

        [Export]
        public AudioStreamPlayer MusicPlayer { get; set; }


        [Export]
        public AudioStreamPlayer SoundPlayer { get; set; }

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
            await ToSignal(SoundPlayer, "finished");
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
    }
}