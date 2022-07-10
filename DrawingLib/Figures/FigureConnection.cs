namespace DrawingLib.Figures
{
    public record FigureConnection(IFigure FigurA, IFigure FigurB) : Figure(PointF.Zero)
    {
        public override IEnumerable<PointF> AnchorPoints =>
            new[] { Start, End};

        public Line Line
        {
            get
            {
                var line = new Line(Start, End) { EndFigure = new Triangle() };
                line.Transform.Parent = Transform;
                return line;
            }
        }

        public PointF Start => FigurA.AbsoluteAnchorPoints
                .OrderBy(p => p.Distance(FigurB.AbsoluteAnchorPoints.First()))
                .First();

        public PointF End => FigurB.AbsoluteAnchorPoints
                .OrderBy(p => p.Distance(FigurA.AbsoluteAnchorPoints.First()))
                .First();

        public override RectF BoundingBox => Line.BoundingBox;

        protected override void Render(ICanvas canvas)
        {
            Line.Draw(canvas);
        }
    }
}
