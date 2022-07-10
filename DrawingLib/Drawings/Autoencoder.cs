using DrawingLib.Figures;
using static Microsoft.Maui.Graphics.Colors;

namespace DrawingLib.Drawings
{
    public class Autoencoder
    {
        public static void Draw(ICanvas canvas)
        {
            var margin = 20f;
            var radius = 10f;

            var pos = new SizeF(30, 30);
            var stepDown = new SizeF(0, (margin + radius) / 2);
            var stepUp = stepDown * -1;
            var stepLeft = new SizeF(30, 0);
            var ns = new[] { 5, 4, 3, 2, 3, 4, 5, 0 };
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

            for (int j = 0; j < 7; j++)
            {
                pos = positions[j];

                for (int i = 0; i < ns[j]; i++)
                {
                    var p = new PointF(0, i * (radius + margin)) + pos;

                    // draw lines
                    for (int k = 0; k < ns[j + 1]; k++)
                    {
                        var nextPos = positions[j + 1];
                        canvas.DrawLine(p, new PointF(0, k * (radius + margin)) + nextPos);
                    }
                }
            }

            for (int j = 0; j < 7; j++)
            {
                pos = positions[j];

                for (int i = 0; i < ns[j]; i++)
                {
                    var p = new PointF(0, i * (radius + margin)) + pos;

                    // draw cicle
                    canvas.DrawCircle(p, radius);
                    canvas.FillCircle(p, radius);
                }
            }

            canvas.FontSize = 8;
            canvas.FontColor = Black;

            var encoderRect = Rect.FromLTRB(positions[0].Width - radius, positions[0].Height - radius, positions[2].Width + radius, ns[0] * (radius + margin) + radius)
                .Inflate(new Size(4));
            var encoder = Box.Create(encoderRect) with
            {
                Text = "Encoder",
                TextPosition = TextPosition.OuterBottom,
                StrokeColor = Red.WithSaturation(0.5f)
            };
            encoder.Draw(canvas);

            var decoderRect = Rect.FromLTRB(positions[4].Width - radius, positions[^1].Height - radius, positions[^1].Width + radius, ns[^2] * (radius + margin) + radius)
                .Inflate(new Size(4));
            var decoderSquare = Box.Create(decoderRect) with
            {
                Text = "Decoder",
                TextPosition = TextPosition.OuterBottom,
                StrokeColor = Blue.WithSaturation(0.5f)
            };
            decoderSquare.Draw(canvas);

            var bottleneckRect = new RectF(new PointF(-radius, -radius) + positions[3], new SizeF(radius * 2, 2 * margin + radius))
                .Inflate(new SizeF(4));
            var bottleneckQuare = Box.Create(bottleneckRect) with
            {
                StrokeColor = Green.WithSaturation(0.5f)
            };

            bottleneckQuare.Draw(canvas);
            canvas.DrawString(
                "Bottleneck",
                bottleneckRect
                    .Offset(0, bottleneckRect.Height)
                    .Inflate(new Size(20, -bottleneckQuare.TextMargin)),
                HorizontalAlignment.Center,
                VerticalAlignment.Top);
        }
    }
}
