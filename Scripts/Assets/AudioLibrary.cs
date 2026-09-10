using Godot;
using System;

namespace ElevenZ.Assets
{
    public static class AudioLibrary
    {
        private static AudioStream _buttonDown;
        private static AudioStream _buttonUp;
        private static AudioStream _menuTheme;

        public static AudioStream ButtonDown => _buttonDown
            ??= GD.Load<AudioStream>("res://Assets/Audio/SoundEffects/UI/ButtonDown.wav");
            
        public static AudioStream ButtonUp => _buttonUp 
            ??= GD.Load<AudioStream>("res://Assets/Audio/SoundEffects/UI/ButtonUp.wav");

        public static AudioStream MenuTheme => _menuTheme 
            ??= GD.Load<AudioStream>("res://Assets/Audio/Music/MainMenu.ogg");
    }
}