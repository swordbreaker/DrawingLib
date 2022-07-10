using System.Numerics;
using static DrawingLib.Figures.DelegateFigure;

namespace DrawingLib.Figures
{
    public record DelegateFigure(PointF Position, DrawDelegate DrawFunction) : Figure(Position)
    {
        public delegate void DrawDelegate(ICanvas canva, Func<Transform, Vector2> getRelativePositionFunc);

        public static DelegateFigure Create(DrawDelegate drawFunction) => 
            new(PointF.Zero, drawFunction);

        public override IEnumerable<PointF> AnchorPoints { get; } = Enumerable.Empty<PointF>();

        public override RectF BoundingBox { get; } = Rect.Zero;

        private Vector2 GetRelativePos(Transform transform) => 
            transform.GetPositionRelativeTo(Transform.Parent);

        protected override void Render(ICanvas canvas)
        {
            DrawFunction(canvas, GetRelativePos);
        }
    }
}
