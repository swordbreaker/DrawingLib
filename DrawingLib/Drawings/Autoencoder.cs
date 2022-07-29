using DrawingLib.Figures;
using DrawingLib.Figures.Layout;
using DrawingLib.Figures.Nodes;
using DrawingLib.Graphics;
using DrawingLib.Util;
using System.Numerics;
using static Microsoft.Maui.Graphics.Colors;
using RectF = Microsoft.Maui.Graphics.RectF;

namespace DrawingLib.Drawings
{
    public class Autoencoder : Drawing
    {
        protected override PointF Margin => base.Margin;

        protected override IEnumerable<IFigure> DrawFigures()
        {
            var margin = 10f;
            var radius = 10f;
            var ns = new[] { 5, 4, 3, 2, 3, 4, 5, 0 };
            var pos = new Vector2(30, 30);
            var stepDown = new Vector2(0, (margin + radius) / 2);
            var stepUp = stepDown * -1;
            var stepLeft = new Vector2(30, 0);
            var positions = new[]
            {
                pos,
                pos += stepDown + stepLeft,
                pos += stepDown + stepLeft,
                pos += stepDown + stepLeft * 1.5f,
                pos += stepUp + stepLeft * 1.5f,
                pos += stepUp + stepLeft,
                pos += stepUp + stepLeft,
            };
            Circle.DefaultPreset = Circle.DefaultPreset with { Radius = radius, FillColor = Orange };

            var vboxes = new List<VBox>();

            for (int j = 0; j < 7; j++)
            {
                var vbox = new VBox() { Marign = 0f, ElementMargin = margin};
                vbox.Transform.Position = positions[j];

                for (int i = 0; i < ns[j]; i++)
                {
                    vbox.Add(new Circle());
                }
                yield return vbox;
                vboxes.Add(vbox);
                vbox.Layout();
            }

            for (int i = 0; i < vboxes.Count - 1; i++)
            {
                var current = vboxes[i];
                var next = vboxes[i + 1];

                foreach (var a in current.Childrens.OfType<Circle>())
                {
                    foreach (var b in next.Childrens.OfType<Circle>())
                    {
                        yield return a.Anchor.Right >> b.Anchor.Left;
                    }
                }
            }

            var encoderRect =
                MathUtil.Union(
                    vboxes[0].AbsoluteBoundingBox,
                    vboxes[1].AbsoluteBoundingBox,
                    vboxes[2].AbsoluteBoundingBox)
                .Inflate(new Size(4));

            var decoderRect =
                MathUtil.Union(
                    vboxes[4].AbsoluteBoundingBox,
                    vboxes[5].AbsoluteBoundingBox,
                    vboxes[6].AbsoluteBoundingBox)
                .Inflate(new Size(4));

            var bottleNeckRect = vboxes[3].AbsoluteBoundingBox.Inflate(new Size(4));

            yield return new Box(encoderRect.Center, encoderRect.Size) with
            {
                Text = "Encoder",
                TextPosition = TextPosition.OuterBottom,
                StrokeColor = Red.WithSaturation(0.5f)
            };

            yield return new Box(bottleNeckRect.Center, bottleNeckRect.Size) with
            {
                Text = "Bottleneck",
                TextPosition = TextPosition.OuterBottom,
                StrokeColor = Green.WithSaturation(0.5f),
                TextMargin = new SizeF(-5, 5)
            };

            yield return new Box(decoderRect.Center, decoderRect.Size) with
            {
                Text = "Decoder",
                TextPosition = TextPosition.OuterBottom,
                StrokeColor = Blue.WithSaturation(0.5f)
            };
        }
    }
}
