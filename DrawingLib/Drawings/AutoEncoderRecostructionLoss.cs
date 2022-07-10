using DrawingLib.Figures;
using DrawingLib.Figures.Layout;
using DrawingLib.Graphics;
using System.Numerics;

namespace DrawingLib.Drawings
{
    public class AutoEncoderRecostructionLoss
    {
        public static void Draw(ICanvas canvas, float scale)
        {
            const float cicleRadius = 5;
            var rectSize = new SizeF(60, 60);

            var container = new Container(new PointF(50, 50));
            container.Transform.Scale = new Vector2(scale, scale);

            // Input
            var inputCirlce = new Circle(Vector2.Zero, cicleRadius, "Input");

            // Encoder
            var encoder = new Box(Vector2.Zero, rectSize) with { Text = "Encoder" };

            // Bottleneck
            var bottleNeckSize = new SizeF(rectSize.Width*0.75f, rectSize.Height);
            var bottleNeck = Box.Create(new RectF(Vector2.Zero, bottleNeckSize)) with { Text = "Bottleneck" };

            // Decoder
            var decoder = Box.Create(new RectF(Vector2.Zero, rectSize)) with { Text = "Decoder" };

            // Output
            var outputCirlce = new Circle(Vector2.Zero, cicleRadius) { Text = "Output" };

            // draw figures
            var hbox = HBox.Create(margin: 10);
            hbox.Add(
                inputCirlce,
                encoder,
                bottleNeck,
                decoder,
                outputCirlce);

            container.Add(hbox);

            Renderer.Render(canvas, container);

            // draw connections
            (inputCirlce >> encoder).Draw(canvas);
            (encoder >> bottleNeck).Draw(canvas);
            (bottleNeck >> decoder).Draw(canvas);
            (decoder >> outputCirlce).Draw(canvas);

            canvas.ResetState();
            canvas.ConcatenateTransform(Matrix3x2.Identity);
            canvas.StrokeColor = Colors.Black;
            canvas.StrokeSize = 4;
            canvas.FillColor = Colors.Black;

            var start = outputCirlce.Transform.AbsolutePosition;
            var end = inputCirlce.Transform.AbsolutePosition;
            var p0 = start + (end - start) * 1f + new Vector2(0, 100);
            var p1 = start + (end - start) * 2 / 3f + new Vector2(0, 100);
            var p2 = start + (end - start) * 1 / 3f + new Vector2(0, 100);
            var p4 = start + new Vector2(0, 100);

            var reconstructionLossPath = new PathF();
            reconstructionLossPath.MoveTo(end.X, end.Y);
            reconstructionLossPath.QuadTo(p0, p1);
            reconstructionLossPath.LineTo(p2);
            reconstructionLossPath.QuadTo(p4, start);
            canvas.DrawPath(reconstructionLossPath);
        }
    }
}
