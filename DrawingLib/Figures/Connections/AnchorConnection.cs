using DrawingLib.Figures.AnchorPoints;

namespace DrawingLib.Figures.Connections
{
    public record AnchorConnection(AnchorPoint Start, AnchorPoint End) : Figure(PointF.Zero)
    {
        public delegate Line DrawLineFunction(Vector2 start, Vector2 end);

        public readonly static DrawLineFunction NoneTonone = (start, end) => new Line(start, end);
        public readonly static DrawLineFunction NoneToArrow = (start, end) => new Line(start, end) { EndFigure = new Triangle() };
        public readonly static DrawLineFunction ArrowToArrow = (start, end) => new Line(start, end) { EndFigure = new Triangle(), StartFigure = new Triangle() };

        public DrawLineFunction LineFactory { get; init; } = (start, end) => new Line(start, end);

        public Line Line => LineFactory(Start.AbsolutePosition, End.AbsolutePosition);

        public override RectF BoundingBox => Line.BoundingBox;

        protected override void Render(ICanvas canvas)
        {
            Line.Draw(canvas);
        }
    }
}
