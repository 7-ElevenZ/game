using ElevenZ.Core;
using Godot;
using System;

public partial class OptionSlider : Control
{
	private string _text = "Setting";

	[ExportCategory("Text")]

	[Export]
	public string Text 
	{
		get => _text;
		set
		{
			_text = value;
			UpdateText();
		}
	}

	[ExportCategory("Setting")]

	[Export]
	public string Section { get; set; }

	[Export]
	public string Setting { get; set; }

	[ExportCategory("Controls")]

	[Export]
	private Label _label { get; set; }

	[Export]
	private Slider _slider { get; set; }

    public override void _Ready()
    {
		UpdateText();

		_slider.DragEnded += SliderDragEnded;
		_slider.Value = ConfigManager.Instance.GetSetting(Section, Setting, 1.0);
    }

	private void SliderDragEnded(bool valueChanged)
	{
		if (valueChanged)
			ConfigManager.Instance.SetSetting(Section, Setting, _slider.Value);
	}

	private void UpdateText()
		=> _label.Text = _text;
}
