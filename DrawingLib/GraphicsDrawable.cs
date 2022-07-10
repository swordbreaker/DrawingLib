
using DrawingLib.Drawings;
using static Microsoft.Maui.Graphics.Colors;

namespace DrawingLib;
public class GraphicsDrawable : IDrawable
{
    public float Scale { get; set; } = 1f;

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.ResetState();
        //canvas.EnableDefaultShadow();
        canvas.FillColor = White;
        canvas.FillRectangle(dirtyRect);
        canvas.FillColor = Yellow;
        canvas.StrokeColor = Black;

        AutoEncoderRecostructionLoss.Draw(canvas, Scale);
    }
}

