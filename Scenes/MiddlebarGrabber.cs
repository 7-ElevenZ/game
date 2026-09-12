using Godot;
using System;

public partial class MiddlebarGrabber : Control
{
	[Export]
	public Control Middlebar { get; set; }

	[Export]
	public float Smoothness { get; set; } = 10f;

	[Export]
	public int Margin { get; set; } = 5;

	private bool _dragging;

    public override void _GuiInput(InputEvent ev)
    {
        if (ev is InputEventMouseButton mouseBtnEvent)
			_dragging = mouseBtnEvent.Pressed && mouseBtnEvent.ButtonIndex == MouseButton.Left;
    }

	public override void _Process(double delta)
	{
		var mousePos = GetGlobalMousePosition();
		
		var mouseX = mousePos.X;
		var width = Middlebar.Size.X;
		var halfWidth = width / 2;
		var oldX = Middlebar.Position.X;

		var windowWidth = DisplayServer.WindowGetSize().X;

		if (_dragging)
		{
			var newX = mouseX - halfWidth;
			var maxPosX = windowWidth - width - Margin;

			newX = Mathf.Clamp(newX, Margin, maxPosX);

			Middlebar.Position = Middlebar.Position with
			{
				X = (float)Mathf.Lerp(oldX, newX, Smoothness * delta)
			};
		}
	}
}
