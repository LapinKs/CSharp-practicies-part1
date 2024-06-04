using System;
using System.Drawing;
using Avalonia;
using Avalonia.Input;
using Avalonia.Media;
using Point = Avalonia.Point;
using Color = Avalonia.Media.Color;
using Brush = Avalonia.Media.Brush;
using Pen = Avalonia.Media.Pen;
using Brushes = Avalonia.Media.Brushes;
using Size = Avalonia.Size;



namespace Manipulation
{
    public static class VisualizerTask
    {
        public static double X = 220;
        public static double Y = -100;
        public static double Alpha = 0.05;
        public static double Wrist = 2 * Math.PI / 3;
        public static double Elbow = 3 * Math.PI / 4;
        public static double Shoulder = Math.PI / 2;

        public static Brush UnreachableAreaBrush = new SolidColorBrush(Color.FromArgb(255, 255, 230, 230));
        public static Brush ReachableAreaBrush = new SolidColorBrush(Color.FromArgb(255, 230, 255, 230));
        public static Pen ManipulatorPen = new Pen(Brushes.Black, 3);
        public static Brush JointBrush = new SolidColorBrush(Colors.Gray);

        public static void KeyDown(IDisplay display, KeyEventArgs key)
        {
            // TODO: Добавьте реакцию на QAWS и пересчитывать Wrist
            if (key.Key == Key.Q) Shoulder += Alpha;
            if (key.Key == Key.A) Shoulder -= Alpha;
            if (key.Key == Key.W) Elbow += Alpha;
            if (key.Key == Key.S) Elbow -= Alpha;
            Wrist = -Alpha - Shoulder - Elbow;
            display.InvalidateVisual(); // вызывает перерисовку канваса
        }

        public static void MouseMove(IDisplay display, PointerEventArgs e)
        {
            // TODO: Измените X и Y пересчитав координаты (e.X, e.Y) в логические.
            var point = ConvertWindowToMath(e.GetPosition(display), GetShoulderPos(display));
            X = point.X;
            Y = point.Y;
            UpdateManipulator();
            display.InvalidateVisual();
        }

        public static void MouseWheel(IDisplay display, PointerWheelEventArgs e)
        {
            // TODO: Измените Alpha, используя e.Delta.Y — размер прокрутки колеса мыши
            Alpha += e.Delta.Y;

            UpdateManipulator();
            display.InvalidateVisual();
        }

        public static void UpdateManipulator()
        {
            var angles = ManipulatorTask.MoveManipulatorTo(X, Y, Alpha);
            if (double.NaN != angles[0] && double.NaN != angles[1] && double.NaN != angles[2])
            {
                Shoulder = angles[0];
                Elbow = angles[1];
                Wrist = angles[2];
            }
            // Вызовите ManipulatorTask.MoveManipulatorTo и обновите значения полей Shoulder, Elbow и Wrist, 
            // если они не NaN. Это понадобится для последней задачи.
        }

        public static void DrawManipulator(DrawingContext context, Point shoulderPos)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(Shoulder, Elbow, Wrist);

            DrawReachableZone(context, ReachableAreaBrush, UnreachableAreaBrush, shoulderPos, joints);

            var formattedText = new FormattedText($"X={X:0}, Y={Y:0}, Alpha={Alpha:0.00}", Typeface.Default, 18,
                TextAlignment.Center, TextWrapping.Wrap, Size.Empty);
            context.DrawText(Brushes.DarkRed, new Point(10, 10), formattedText);
            foreach (var e in joints)
                ConvertMathToWindow(e, shoulderPos);
            for (int i = 0; i < 2; i++)
                context.DrawLine(ManipulatorPen, joints[i], joints[i + 1]);
            for (int i = 0; i < 3; i++)
                context.DrawEllipse(JointBrush, null, joints[i], 1, 1);
            context.DrawLine(ManipulatorPen, shoulderPos, joints[0]);

            // Нарисуйте сегменты манипулятора методом ccontext.DrawLine(ManipulatorPen, ...)
            // Нарисуйте суставы манипулятора окружностями методом context.DrawEllipse(JointBrush, null, ...)
            // Не забудьте сконвертировать координаты из логических в оконные
        }

        private static void DrawReachableZone(
            DrawingContext context,
            Brush reachableBrush,
            Brush unreachableBrush,
            Point shoulderPos,
            Point[] joints)
        {
            var rmin = Math.Abs(Manipulator.UpperArm - Manipulator.Forearm);
            var rmax = Manipulator.UpperArm + Manipulator.Forearm;
            var mathCenter = new Point(joints[2].X - joints[1].X, joints[2].Y - joints[1].Y);
            var windowCenter = ConvertMathToWindow(mathCenter, shoulderPos);
            context.DrawEllipse(reachableBrush,
                null,
                new Point(windowCenter.X, windowCenter.Y),
                rmax, rmax);
            context.DrawEllipse(unreachableBrush,
                null,
                new Point(windowCenter.X, windowCenter.Y),
                rmin, rmin);
        }

        public static Point GetShoulderPos(IDisplay display)
        {
            return new Point(display.Bounds.Width / 2, display.Bounds.Height / 2);
        }

        public static Point ConvertMathToWindow(Point mathPoint, Point shoulderPos)
        {
            return new Point(mathPoint.X + shoulderPos.X, shoulderPos.Y - mathPoint.Y);
        }

        public static Point ConvertWindowToMath(Point windowPoint, Point shoulderPos)
        {
            return new Point(windowPoint.X - shoulderPos.X, shoulderPos.Y - windowPoint.Y);
        }
    }
}
