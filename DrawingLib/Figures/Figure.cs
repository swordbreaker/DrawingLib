using DrawingLib.Util;
using System.Numerics;

namespace DrawingLib.Figures
{
    public abstract record Figure : IFigure
    {
        public Figure(PointF position)
        {
            Transform.Position = new Vector2(position.X, position.Y);
        }

        public Transform Transform { get; init; } = new Transform();

        public IFigure? Parent
        {
            get => _parent;
            set
            {
                if(_parent != value)
                {
                    _parent?.Remove(this);
                    _parent = value;
                    Transform.Parent = _parent?.Transform;
                }
            }
        }

        public IReadOnlyCollection<IFigure> Childrens { get; } = new HashSet<IFigure>();

        private HashSet<IFigure> InternalChildrens => (HashSet<IFigure>)Childrens;

        public IEnumerable<PointF> AbsoluteAnchorPoints => AnchorPoints
            .Select(p => p.TransformBy(Transform.AbsoluteTransformationMatrix));

        public abstract IEnumerable<PointF> AnchorPoints { get; }

        public RectF AbsoluteBoundingBox => BoundingBox
            .GetCorners()
            .Select(p => p.TransformBy(Transform.AbsoluteTransformationMatrix))
            .CalculateBoundingBox();

        public abstract RectF BoundingBox { get; }

        private IFigure? _parent = null;

        public void Draw(ICanvas canvas)
        {
            canvas.SaveState();
            canvas.ResetState();
            canvas.ConcatenateTransform(Transform.AbsoluteTransformationMatrix);

            Render(canvas);

            canvas.ConcatenateTransform(Matrix3x2.Identity);
            canvas.RestoreState();
        }

        protected abstract void Render(ICanvas canvas);

        public IFigure Add(params IFigure[] figures)
        {
            foreach (var fig in figures)
            {
                fig.Parent = this;
                InternalChildrens.Add(fig);
            }
            return this;
        }

        public IFigure Remove(params IFigure[] figures)
        {
            foreach (var fig in figures)
            {
                fig.Parent = null;
                InternalChildrens.Remove(fig);
            }
            return this;
        }

        public static FigureConnection operator >>(Figure a, Figure b) => new(a, b);

        public static FigureConnection operator <<(Figure a, Figure b) => new(b, a);
    }
}
