using System.Collections.Generic;
using System.Drawing;
using GeometryTasks;
using Avalonia.Media;
namespace GeometryPainting
{
    public static class SegmentExtensions
    {
        private static Dictionary<Segment, Avalonia.Media.Color> dict = new Dictionary<Segment, Avalonia.Media.Color>();

        public static void SetColor(this Segment segment, Avalonia.Media.Color color)
        {
            if (dict.ContainsKey(segment))
                dict[segment] = color;
            else dict.Add(segment, color);
        }

        public static Avalonia.Media.Color GetColor(this Segment segment)
        {
            if (!dict.ContainsKey(segment))
                return Avalonia.Media.Colors.Black;
            else
                return dict[segment];
        }
    }
}