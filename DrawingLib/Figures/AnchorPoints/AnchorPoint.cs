using DrawingLib.Figures.Connections;
using DrawingLib.Figures.Nodes;

namespace DrawingLib.Figures.AnchorPoints
{
    public record AnchorPoint
    {
        public AnchorPoint(Vector2 position, Transform? parent)
        {
            Transform = new Transform
            {
                Position = position,
                Parent = parent,
            };
        }

        public static AnchorPoint Zero => new(Vector2.Zero, null);

        public Transform Transform { get; }

        public Vector2 GetPositionRelativeTo(Transform? transform) => 
            Transform.GetPositionRelativeTo(transform);

        public Vector2 AbsolutePosition => Transform.AbsolutePosition;

        public static AnchorConnection operator >>(AnchorPoint a, FigureNode b) => new(a, FigureConnection.FindNearestAnchor(new[] { a }, b.Anchor.All).end) { LineFactory = AnchorConnection.NoneToArrow };
        
        public static AnchorConnection operator >>(AnchorPoint a, AnchorPoint b) => new (a, b) { LineFactory = AnchorConnection.NoneToArrow };
    }
}
