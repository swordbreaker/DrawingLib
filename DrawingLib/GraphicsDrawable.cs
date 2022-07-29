
using DrawingLib.Drawings;
using DrawingLib.Graphics;
using static Microsoft.Maui.Graphics.Colors;

namespace DrawingLib;
public class GraphicsDrawable : IDrawable
{
    public float Scale { get; set; } = 1f;

    //private readonly Drawing Drawing = new MelganGenerator();
    //private readonly Drawing Drawing = new Autoencoder();
    //private readonly Drawing Drawing = new AutoEncoderRecostructionLoss();
    private readonly Drawing Drawing = new VariationalAutoencoder();

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        Drawing.Draw(canvas, Scale, dirtyRect);
    }
}

