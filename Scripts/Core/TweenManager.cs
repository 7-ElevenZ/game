using Godot;
using System;
using System.Collections.Generic;

namespace ElevenZ.Core
{
    public partial class TweenManager : Node
    {
        private static Dictionary<Node, Tween> _tweens = new();

        public static TweenManager Instance { get; private set; }

        public override void _Ready()
        {
            Instance = this;
        }

        public Tween CreateTweenForNode(Node node, Tween.EaseType ease = Tween.EaseType.InOut, Tween.TransitionType trans = Tween.TransitionType.Linear, bool parallel = false)
        {
            if (_tweens.TryGetValue(node, out var existingTween) && !existingTween.IsValid()) _tweens.Remove(node);

            var tween = node.CreateTween()
                .SetEase(ease)
                .SetTrans(trans)
                .SetParallel(parallel);

            if (!_tweens.ContainsKey(node))
                _tweens.Add(node, tween);
            else
            {
                _tweens[node].Kill();
                _tweens[node] = tween;
            }

            return tween;
        }
    }
}