using DrawingLib.Figures;
using DrawingLib.Figures.Layout;
using System.Numerics;

namespace DrawingLib.Graphics
{
    public abstract class Drawing
    {
        protected Box Box(SizeF size) => new Box(PointF.Zero, size);
        protected Circle Circle(float radius) => new Circle(PointF.Zero, radius);
        protected HBox HBox() => Figures.Layout.HBox.Create();

        protected virtual PointF Margin { get; } = new PointF(20, 20);
        protected virtual Color BackgroundColor { get; } = Colors.White;

        protected abstract IEnumerable<IFigure> DrawFigures();

        public void Draw(ICanvas canvas, float scale, RectF dirtyRect)
        {
            canvas.FillColor = BackgroundColor;
            canvas.FillRectangle(dirtyRect);
            canvas.ResetState();

            var container = new Container(Margin);
            container.Add(DrawFigures().ToArray());
            container.Transform.Scale = new Vector2(scale);
            Renderer.Layout(canvas, container);
            Renderer.Render(canvas, container);
        }
    }
}
