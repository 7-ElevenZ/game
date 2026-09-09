using Godot;
using System;

namespace ElevenZ.Assets
{
    public static class AudioLibrary
    {
        private static AudioStream _buttonDown;
        private static AudioStream _buttonUp;

        public static AudioStream ButtonDown => _buttonDown ??= GD.Load<AudioStream>("res://Assets/Audio/SoundEffects/UI/button-down.wav");
        public static AudioStream ButtonUp => _buttonUp ??= GD.Load<AudioStream>("res://Assets/Audio/SoundEffects/UI/button-up.wav");
    }
}