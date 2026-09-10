using ElevenZ.Core;
using Godot;
using System;
using System.Threading.Tasks;

namespace ElevenZ.UI.Effects
{
    public partial class Logo : TextureRect
    {
        [ExportCategory("Rotation")]

        [Export]
        public float RotationSpeed { get; set; } = 1.5f;


        [Export]
        public float RotationAmplitude { get; set; } = 3f;

        private float _time;

        public override void _Process(double delta)
        {
            _time += (float)delta;
            RotationDegrees = Mathf.Sin(RotationSpeed * _time) * RotationAmplitude;
        }
    }
}