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

        public PointF Start => GetAnchors(FigurA)
                .OrderBy(p => p.Distance(GetAnchors(FigurB).First()))
                .First();

        public PointF End => GetAnchors(FigurB)
                .OrderBy(p => p.Distance(GetAnchors(FigurA).First()))
                .First();

        public IEnumerable<PointF> GetAnchors(IFigure figure)
        {
            var tranfomationMatrix = figure.Transform.GetTransformationMatrixRelativeTo(Transform.Parent);
            return figure.AnchorPoints.Select(p => p.TransformBy(tranfomationMatrix));
        }

        public override RectF BoundingBox => Line.BoundingBox;

        protected override void Render(ICanvas canvas)
        {
            Line.Draw(canvas);
        }
    }
}
