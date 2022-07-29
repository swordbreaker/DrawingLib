using DrawingLib.Figures.AnchorPoints;
using DrawingLib.Figures.Nodes;
using System.Collections.Generic;

namespace DrawingLib.Figures.Connections
{
    public record FigureConnection(IFigureNode FigurA, IFigureNode FigurB) : Figure(PointF.Zero)
    {
        public AnchorConnection AnchorConnection
        {
            get
            {
                var (start, end) = FindNearestAnchor(FigurA.Anchor, FigurB.Anchor);
                return new AnchorConnection(start, end) with { LineFactory = LineFactory };
            }
        }

        public AnchorConnection.DrawLineFunction LineFactory { get; init; } = AnchorConnection.NoneToArrow;

        public static (AnchorPoint start, AnchorPoint end) FindNearestAnchor(Anchor anchorA, Anchor anchorB) =>
            FindNearestAnchor(anchorA.All, anchorB.All);

        public static (AnchorPoint start, AnchorPoint end) FindNearestAnchor(IEnumerable<AnchorPoint> anchorsA, IEnumerable<AnchorPoint> anchorsB)
        {
            var minDist = float.MaxValue;
            var nearestPointStart = AnchorPoint.Zero;
            var nearestPointEnd = AnchorPoint.Zero;

            foreach (var a in anchorsA)
            {
                foreach (var b in anchorsB)
                {
                    var dist = Vector2.Distance(a.AbsolutePosition, b.AbsolutePosition);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        nearestPointStart = a;
                        nearestPointEnd = b;
                    }
                }
            }

            return (nearestPointStart, nearestPointEnd);
        }

        public override RectF BoundingBox => AnchorConnection.BoundingBox;

        protected override void Render(ICanvas canvas)
        {
            AnchorConnection.Draw(canvas);
        }
    }
}
