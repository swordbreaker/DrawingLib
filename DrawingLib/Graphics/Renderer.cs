using DrawingLib.Figures;

namespace DrawingLib.Graphics
{
    public class Renderer
    {
        public static void Render(ICanvas canvas, IFigure figure)
        {
            figure.Draw(canvas);

            foreach (var fig in figure.Childrens)
            {
                Render(canvas, fig);
            }
        }
    }
}
