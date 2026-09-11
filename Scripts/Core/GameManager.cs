using ElevenZ.Assets;
using Godot;
using System;

namespace ElevenZ.Core
{
    public partial class GameManager : Node
    {
        public static GameManager Instance { get; private set; }

        public override void _Ready()
        {
            Instance = this;
        }

        public void QuitGame() // so i dont have to call GetTree().Quit() every time i want to quit
        {
            GetTree().Quit();
        }
    }
}