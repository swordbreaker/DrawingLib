using DrawingLib.Figures;
using DrawingLib.Figures.Layout;
using DrawingLib.Figures.Nodes;
using DrawingLib.Graphics;
using DrawingLib.Presets;
using System.Numerics;
using static System.Net.Mime.MediaTypeNames;

namespace DrawingLib.Drawings
{
    public class AutoEncoderRecostructionLoss : Drawing
    {
        protected override PointF Margin => new(50, 50);

        protected override IEnumerable<IFigure> DrawFigures()
        {
            Circle.DefaultPreset = Circle.DefaultPreset with { Radius = 5f };
            var encoderDecoderPreset = new BoxPreset()
            {
                FillColor = Colors.Yellow,
                Size = new SizeF(60, 60)
            };

            var bottleNeckPreset = new BoxPreset
            {
                FillColor = Colors.Aquamarine,
                Size = new SizeF(encoderDecoderPreset.Size.Width * 0.75f, encoderDecoderPreset.Size.Height)
             };

            // Input
            var inputCirlce = new Circle() with { Text = "Input", FillColor = Colors.Red, TextMargin = new SizeF(5, 5), TextPosition = TextPosition.OuterTop };

            // Encoder
            var encoder = new Box(encoderDecoderPreset) with { Text = "Encoder" };

            // Bottleneck
            var bottleNeck = new Box(bottleNeckPreset) with { Text = "Bottleneck", FillColor = Colors.Aquamarine };

            // Decoder
            var decoder = new Box(encoderDecoderPreset) with { Text = "Decoder", };

            // Output
            var outputCirlce = new Circle() with { Text = "Output", FillColor = Colors.Red, TextMargin = new SizeF(5, 5), TextPosition = TextPosition.OuterTop };

            var hbox = new HBox() with { Marign = 0, ElementMargin = 25 };
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
