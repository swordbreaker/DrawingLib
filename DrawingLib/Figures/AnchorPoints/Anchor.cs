using DrawingLib.Figures.Nodes;

namespace DrawingLib.Figures.AnchorPoints
{
    public record class Anchor
    {
        public AnchorPoint Left { get; }
        public AnchorPoint Top { get; }
        public AnchorPoint Right { get; }
        public AnchorPoint Bottom { get; }

        private readonly Func<float, AnchorPoint> getAnchorPointFunc;

        public Anchor(Vector2 left, Vector2 top, Vector2 right, Vector2 bottom, Func<float, AnchorPoint> getAnchorPointFunc, Transform transform)
        {
            Left = new AnchorPoint(left, transform);
            Top = new AnchorPoint(top, transform);
            Right = new AnchorPoint(right, transform);
            Bottom = new AnchorPoint(bottom, transform);
            this.getAnchorPointFunc = getAnchorPointFunc;
        }

        public Anchor(AnchorPoint left, AnchorPoint top, AnchorPoint right, AnchorPoint bottom, Func<float, AnchorPoint> getAnchorPointFunc)
        {
            Left =left;
            Top = top;
            Right =right;
            Bottom = bottom;
            this.getAnchorPointFunc = getAnchorPointFunc;
        }

        public Anchor(IFigureNode figure) : 
            this(
                figure.GetAnchorPoint(7/8f),
                figure.GetAnchorPoint(1/8f), 
                figure.GetAnchorPoint(3/8f),
                figure.GetAnchorPoint(5/8f),
                figure.GetAnchorPoint)
        {
        }

        public IEnumerable<AnchorPoint> All
        {
            get
            {
                yield return Left;
                yield return Top;
                yield return Right;
                yield return Bottom;
            }
        }
    }
}
