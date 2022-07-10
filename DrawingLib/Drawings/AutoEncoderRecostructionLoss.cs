using DrawingLib.Figures;
using DrawingLib.Graphics;
using System.Numerics;

namespace DrawingLib.Drawings
{
    public class AutoEncoderRecostructionLoss : Drawing
    {
        protected override PointF Margin => new(50, 50);

        protected override IEnumerable<IFigure> DrawFigures()
        {
            const float cicleRadius = 5;
            var rectSize = new SizeF(60, 60);
            var bottleNeckSize = new SizeF(rectSize.Width * 0.75f, rectSize.Height);

            // Input
            var inputCirlce = Circle(cicleRadius) with { Text = "Input" };

            // Encoder
            var encoder = Box(rectSize) with { Text = "Encoder" };

            // Bottleneck
            var bottleNeck = Box(bottleNeckSize) with { Text = "Bottleneck" };

            // Decoder
            var decoder = Box(rectSize) with { Text = "Decoder" };

            // Output
            var outputCirlce = Circle(cicleRadius) with { Text = "Output" };

            var hbox = HBox() with { Marign = 0, ElementMargin = 25 };
            hbox.Add(
                inputCirlce,
                encoder,
                bottleNeck,
                decoder,
                outputCirlce);

            yield return hbox;
            yield return inputCirlce >> encoder;
            yield return encoder >> bottleNeck;
            yield return bottleNeck >> decoder;
            yield return decoder >> outputCirlce;

            yield return DelegateFigure.Create((c, getRelativePos) =>
            {
                c.StrokeColor = Colors.Black;
                c.StrokeSize = 4;
                c.FillColor = Colors.Black;

                var start = outputCirlce.Transform.Position;
                var end = inputCirlce.Transform.Position;
                var p0 = start + (end - start) * 1f + new Vector2(0, 100);
                var p1 = start + (end - start) * 2 / 3f + new Vector2(0, 100);
                var p2 = start + (end - start) * 1 / 3f + new Vector2(0, 100);
                var p4 = start + new Vector2(0, 100);

                var reconstructionLossPath = new PathF();
                reconstructionLossPath.MoveTo(end.X, end.Y);
                reconstructionLossPath.QuadTo(p0, p1);
                reconstructionLossPath.LineTo(p2);
                reconstructionLossPath.QuadTo(p4, start);
                c.DrawPath(reconstructionLossPath);
            });
        }
    }
}
