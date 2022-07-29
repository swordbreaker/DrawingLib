using DrawingLib.Figures;
using DrawingLib.Figures.Layout;
using DrawingLib.Figures.Nodes;
using DrawingLib.Graphics;
using System.Numerics;

namespace DrawingLib.Drawings
{
    internal class VariationalAutoencoder : Drawing
    {
        protected override PointF Margin => new PointF(50, 150);

        protected override IEnumerable<IFigure> DrawFigures()
        {
            var hbox = new HBox();
            const float boxHeigth = 90f;
            Circle.DefaultPreset = Circle.DefaultPreset with { TextPosition = TextPosition.OuterBottom, FillColor = Colors.Red.WithSaturation(0.5f) };

            var inCirlce = new Circle() { Text = "Input" };
            var encodere = new Box(new SizeF(boxHeigth)) { Text = "Encoder" };
            var mean = new Box(new SizeF(60, boxHeigth)) { Text = "Mean Vector" };
            var std = new Box(new SizeF(60, boxHeigth))  { Text = "Std Vector" };
            var sampled = new Box(new SizeF(60, boxHeigth)) { Text = "Sampled latent vector" };
            var decoder = new Box(new SizeF(boxHeigth)) { Text = "Decoder" };
            var outCirlce = new Circle() { Text = "Output" };

            var vbox = new VBox();
            vbox.Transform.Position = new Vector2(0, -70);
            vbox.Add(std, mean);

            hbox.Add(inCirlce, encodere, vbox, sampled, decoder, outCirlce);
            yield return hbox;
            yield return inCirlce >> encodere;
            yield return encodere.Anchor.Right >> mean.Anchor.Left;
            yield return encodere.Anchor.Right >> std.Anchor.Left;
            yield return mean.Anchor.Right >> sampled.Anchor.Left;
            yield return std.Anchor.Right >> sampled.Anchor.Left;
            yield return sampled >> decoder;
            yield return decoder >> outCirlce;
        }
    }
}
