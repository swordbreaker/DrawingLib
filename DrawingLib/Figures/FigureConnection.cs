using DrawingLib.Figures.Nodes;

namespace DrawingLib.Figures
{
    public record FigureConnection(IFigureNode FigurA, IFigureNode FigurB) : Figure(PointF.Zero)
    {
        public Func<PointF, PointF, Line> LineFactory { get; init; } = (start, end) => new Line(start, end) { EndFigure = new Triangle() };

        public Func<IEnumerable<PointF>, IEnumerable<PointF>, PointF> AnchorSelectionStartStragegy { get; init; } = FindNearestAnchor;

        public Func<IEnumerable<PointF>, IEnumerable<PointF>, PointF> AnchorSelectionEndStragegy { get; init; } = FindNearestAnchor;

        private static PointF FindNearestAnchor(IEnumerable<PointF> anchorsA, IEnumerable<PointF> anchorsB) =>
            anchorsA
                .OrderBy(p => anchorsB.First())
                .First();

        public Line Line
        {
            get
            {
                var line = LineFactory(Start, End);
                line.Transform.Parent = Transform;
                return line;
            }
        }

        public PointF Start => AnchorSelectionStartStragegy(GetAnchors(FigurA), GetAnchors(FigurB));

        public PointF End => AnchorSelectionEndStragegy(GetAnchors(FigurB), GetAnchors(FigurA));

        public IEnumerable<PointF> GetAnchors(IFigureNode figure)
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
