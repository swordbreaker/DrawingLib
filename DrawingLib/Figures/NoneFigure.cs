namespace DrawingLib.Figures
{
    public record NoneFigure : IFigure
    {
        public IEnumerable<PointF> AnchorPoints => Enumerable.Empty<PointF>();
        public RectF BoundingBox => Rect.Zero;
        public IEnumerable<PointF> AbsoluteAnchorPoints => AnchorPoints;
        public RectF AbsoluteBoundingBox => BoundingBox;

        public Transform Transform { get; } = new Transform();
        public IFigure? Parent { get; set; } = null;

        public IReadOnlyCollection<IFigure> Childrens => throw new NotImplementedException();

        public void Draw(ICanvas canvas)
        {
        }

        public IFigure Remove(params IFigure[] figures) => this;

        public IFigure Add(params IFigure[] figures) => this;

        public IFigure Rotate(float degrees) => this;

        public IFigure RotateRad(float rad) => this;
     
        public IFigure Scale(float scale) => this;

        public IFigure Scale(float dx, float dy) => this;

        public IFigure Translate(float x, float y) => this;

        public RectF GetTranslatedBoundingBox(Transform? transform = null) => BoundingBox;
    }
}
