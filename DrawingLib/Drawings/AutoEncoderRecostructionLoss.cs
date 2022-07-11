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
            var inputCirlce = Circle(cicleRadius) with { Text = "Input", FillColor = Colors.Red };

            // Encoder
            var encoder = Box(rectSize) with { Text = "Encoder", FillColor = Colors.Yellow };

            // Bottleneck
            var bottleNeck = Box(bottleNeckSize) with { Text = "Bottleneck", FillColor = Colors.Aquamarine };

            // Decoder
            var decoder = Box(rectSize) with { Text = "Decoder", FillColor = Colors.Yellow };

            // Output
            var outputCirlce = Circle(cicleRadius) with { Text = "Output", FillColor = Colors.Red };

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

                var start = (Vector2)outputCirlce.AnchorPoints.Last() + outputCirlce.Transform.Position;
                var end = (Vector2)inputCirlce.AnchorPoints.Last() + inputCirlce.Transform.Position;
                var directionVec = (end - start);
                var offsetY = new Vector2(0, 100);
                var p0 = start + directionVec * 1f + offsetY;
                var p1 = start + directionVec * 2 / 3f + offsetY;
                var p2 = start + directionVec * 1 / 3f + offsetY;
                var p4 = start + new Vector2(0, 100);

                var textPos = start + directionVec * 1 / 2 + offsetY + new Vector2(0, 15);

                var reconstructionLossPath = new PathF();
                reconstructionLossPath.MoveTo(end.X, end.Y);
                reconstructionLossPath.QuadTo(p0, p1);
                reconstructionLossPath.LineTo(p2);
                reconstructionLossPath.QuadTo(p4, start);
                c.DrawPath(reconstructionLossPath);
                c.DrawString("Reconstruction Loss", textPos.X, textPos.Y, HorizontalAlignment.Center);
            });
        }
    }
}
