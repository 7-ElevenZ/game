using Godot;
using System;

public partial class AudioLibrary : Node
{
    private AudioStream _buttonDown;
    private AudioStream _buttonUp;

    public AudioStream ButtonDown => _buttonDown ??= GD.Load<AudioStream>("res://Assets/Audio/UI/button-down.wav");
    public AudioStream ButtonUp => _buttonUp ??= GD.Load<AudioStream>("res://Assets/Audio/UI/button-up.wav");
}
