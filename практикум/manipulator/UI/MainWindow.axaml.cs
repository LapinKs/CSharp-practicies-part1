using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace Manipulation.UI;

public partial class MainWindow : Window
{
	private readonly Frame frame;

	public void InitializeComponent()
	{
		AvaloniaXamlLoader.Load(this);
	}

	public MainWindow()
	{
		InitializeComponent();
		frame = this.FindNameScope()?.Find<Frame>("Frame") ??
		        throw new InvalidOperationException("Frame wasn't created!");
		frame.GetSize = () => ClientSize;

		Background = new SolidColorBrush(new Color(255, 255, 230, 230));

		KeyDown += (_, ev) => VisualizerTask.KeyDown(frame, ev);
		PointerMoved += (_, ev) => VisualizerTask.MouseMove(frame, ev);
		PointerWheelChanged += (_, ev) => VisualizerTask.MouseWheel(frame, ev);
	}
}