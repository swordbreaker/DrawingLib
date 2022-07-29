using DrawingLib.Figures;
using DrawingLib.Figures.Layout;
using DrawingLib.Figures.Nodes;
using DrawingLib.Graphics;
using DrawingLib.Presets;

namespace DrawingLib.Drawings
{
    internal class MelganGenerator : Drawing
    {
        protected override PointF Margin => new PointF(100, 50);

        protected override IEnumerable<IFigure> DrawFigures()
        {
            Box.DefaultBoxPreset = Box.DefaultBoxPreset with { Size = new SizeF(100, 40) };

            var textTemplate = new BoxPreset
            {
                FillColor = Colors.Transparent,
                StrokeColor = Colors.Transparent,
                TextPosition = TextPosition.Center,
                Size = new SizeF(100, 25),
                TextMargin = SizeF.Zero
            };


            var melInput = new Box(textTemplate) { Text = "Mel Spectogram" };
            var convLayer = new Box() { Text = "Conv Layer" };

            var up1 = new Box() { Text = "Upsamling [8x] Layer" };
            var res1 = new Box() { Text = "Residual Stack" };

            var g1 = new VBox() { FillColor = Colors.Green, StrokeColor = Colors.Black};
            g1.Add(up1, res1);

            var up2 = new Box() { Text = "Upsamling [8x] Layer" };
            var res2 = new Box() { Text = "Residual Stack" };
            var g2 = new VBox() { FillColor = Colors.Green, StrokeColor = Colors.Black };
            g2.Add(up2, res2);

            var convLayer2 = new Box() { Text = "Conv Layer" };
            var rawWav = new Box(textTemplate) { Text = "Raw Waveform" };

            var vbox = new VBox() { ElementMargin = 25 };
            vbox.Add(
                melInput,
                convLayer,
                g1,
                g2,
                convLayer2,
                rawWav);

            yield return vbox;
            yield return melInput >> convLayer;
            yield return convLayer >> up1;
            yield return up1 >> res1;
            yield return res1 >> up2;
            yield return up2 >> res2;
            yield return res2 >> convLayer2;
            yield return convLayer2 >> rawWav;
        }
    }
}
