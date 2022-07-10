using DrawingLib.Figures;
using ILayout = DrawingLib.Figures.Layout.ILayout;

namespace DrawingLib.Graphics
{
    public class Renderer
    {
        public static void Layout(ICanvas canvas, IFigure figure)
        {
            if(figure is ILayout layout)
            {
                layout.Layout();
            }

            foreach (var fig in figure.Childrens)
            {
                Layout(canvas, fig);
            }
        }

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
