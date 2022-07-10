using DrawingLib.Util;
using System.Numerics;

namespace DrawingLib.Figures
{
    public record Line(PointF start, PointF end) : Figure(PointF.Zero)
    {
        public IFigure StartFigure { get; init; } = new NoneFigure();
        public IFigure EndFigure { get; init; } = new NoneFigure();

        public override IEnumerable<PointF> AnchorPoints => new[] {start, end};

        public override RectF BoundingBox => Rect.FromLTRB(
            Math.Min(start.X, end.X),
            Math.Min(start.Y, end.Y),
            Math.Max(start.X, end.X),
            Math.Max(start.Y, end.Y))
            .Union(StartFigure.AbsoluteBoundingBox)
            .Union(EndFigure.AbsoluteBoundingBox);

        protected override void Render(ICanvas canvas)
        {
            canvas.StrokeColor = Colors.Black;
            canvas.StrokeSize = 1;
            canvas.FillColor = Colors.Black;
            canvas.DrawLine(start, end);
            var downVec = new Vector2(0, -1);
            var lineVec = new Vector2(end.X, end.Y) - new Vector2(start.X, start.Y);
            var rotation = lineVec.Rotation(downVec);

            StartFigure.Transform.SetRadRotation(rotation);
            StartFigure.Transform.SetPosition(start);

            EndFigure.Transform.SetRadRotation(rotation + MathF.PI);
            EndFigure.Transform.SetPosition(end);

            StartFigure.Draw(canvas);
            EndFigure.Draw(canvas);
        }
    }
}
