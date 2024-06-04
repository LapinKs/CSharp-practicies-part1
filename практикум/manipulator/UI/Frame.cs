using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace Manipulation.UI;

public class Frame : UserControl
{
	public Func<Size> GetSize;

	public override void Render(DrawingContext context)
	{
		var size = GetSize();
		var shoulderPos = new Point(size.Width / 2f, size.Height / 2f);
		VisualizerTask.DrawManipulator(context, shoulderPos);
		base.Render(context);
	}
}