using DrawingLib.Figures.AnchorPoints;
using DrawingLib.Figures.Connections;
using DrawingLib.Presets;
using DrawingLib.Util;

namespace DrawingLib.Figures.Nodes
{
    public abstract record FigureNode(PointF Position, FigureNodePreset Preset) : Figure(Position), IFigureNode
    {
        protected FigureNode(PointF Postion) : this(Postion, new DefaultFigureNodePreset())
        {
        }

        private FigureNodePreset Preset { get; init; } = Preset;
        public Color StrokeColor { get; init; } = Preset.StrokeColor;
        public Color FillColor { get; init; } = Preset.FillColor;
        public float StrokeSize { get; init; } = Preset.StrokeSize;
        public SizeF TextMargin { get; init; } = Preset.TextMargin;
        public TextFlow TextFlow { get; init; } = Preset.TextFlow;
        public TextPosition TextPosition { get; init; } = Preset.TextPosition;
        public string Text { get; init; } = Preset.Text;

        public override RectF BoundingBox => BoudingBoxWithoutText.Union(GetTextRect());

        protected abstract RectF BoudingBoxWithoutText { get; }

        public virtual Anchor Anchor => new(this);

        public static FigureConnection operator >>(FigureNode a, FigureNode b) => new(a, b) { LineFactory = AnchorConnection.NoneToArrow };
        public static AnchorConnection operator >>(FigureNode a, AnchorPoint b) => new(FigureConnection.FindNearestAnchor(a.Anchor.All, new[] {b}).start, b) { LineFactory = AnchorConnection.NoneToArrow };
        public static FigureConnection operator -(FigureNode a, FigureNode b) => new(a, b) { LineFactory = AnchorConnection.NoneTonone };

        protected override void Render(ICanvas canvas)
        {
            canvas.StrokeColor = StrokeColor ?? Colors.Black;
            canvas.FillColor = FillColor ?? Colors.Transparent;
            canvas.StrokeSize = StrokeSize;

            DrawFigure(canvas);
            PlaceText(canvas);

            //foreach (var a in Anchor.All)
            //{
            //    canvas.StrokeColor = Colors.Violet;
            //    canvas.DrawCircle(a, 5f);
            //}
        }

        protected abstract void DrawFigure(ICanvas canvas);

        private RectF GetTextRect()
        {
            if (string.IsNullOrEmpty(Text))
            {
                return BoudingBoxWithoutText;
            }

            var rectWithMargin = BoudingBoxWithoutText.Inflate(TextMargin * -1);
            var outerRect = BoudingBoxWithoutText.Union(new Rect(-30f, 0, 60f, 10f));

            return TextPosition switch
            {
                TextPosition.Top => rectWithMargin,
                TextPosition.Bottom => rectWithMargin,
                TextPosition.Left => rectWithMargin,
                TextPosition.Right => rectWithMargin,
                TextPosition.Center => rectWithMargin,
                TextPosition.OuterTop => outerRect.Offset(0, -(outerRect.Height + TextMargin.Height)),
                TextPosition.OuterLeft => outerRect.Offset(-(BoudingBoxWithoutText.Height + TextMargin.Width), 0),
                TextPosition.OuterRight => outerRect.Offset(BoudingBoxWithoutText.Height + TextMargin.Width, 0),
                TextPosition.OuterBottom => outerRect.Offset(0, BoudingBoxWithoutText.Height + TextMargin.Height),
                _ => RectF.Zero,
            };
        }

        private void PlaceText(ICanvas canvas)
        {
            var rect = GetTextRect();

            canvas.StrokeColor = Preset.TextColor;

            switch (TextPosition)
            {
                case TextPosition.Top:
                    canvas.DrawString(Text, rect, HorizontalAlignment.Center, VerticalAlignment.Top, TextFlow);
                    break;
                case TextPosition.Bottom:
                    canvas.DrawString(Text, rect, HorizontalAlignment.Center, VerticalAlignment.Bottom, TextFlow);
                    break;
                case TextPosition.Left:
                    canvas.DrawString(Text, rect, HorizontalAlignment.Center, VerticalAlignment.Bottom, TextFlow);
                    break;
                case TextPosition.Right:
                    canvas.DrawString(Text, rect, HorizontalAlignment.Right, VerticalAlignment.Center, TextFlow);
                    break;
                case TextPosition.Center:
                    canvas.DrawString(Text, rect, HorizontalAlignment.Center, VerticalAlignment.Center, TextFlow);
                    break;
                case TextPosition.OuterTop:
                    canvas.DrawString(Text, rect, HorizontalAlignment.Center, VerticalAlignment.Bottom, TextFlow);
                    break;
                case TextPosition.OuterLeft:
                    canvas.DrawString(Text, rect, HorizontalAlignment.Right, VerticalAlignment.Center, TextFlow);
                    break;
                case TextPosition.OuterRight:
                    canvas.DrawString(Text, rect, HorizontalAlignment.Left, VerticalAlignment.Center, TextFlow);
                    break;
                case TextPosition.OuterBottom:
                    canvas.DrawString(Text, rect, HorizontalAlignment.Center, VerticalAlignment.Top, TextFlow);
                    break;
            }
            canvas.RestoreState();
        }

        public virtual AnchorPoint GetAnchorPoint(float p)
        {
            // Normalize p to [0, -1]
            if (p < 0)
            {
                if(p < -1)
                {
                    p += (p + 1);
                }
                p = 1 - p;
            }
            else if (p > 1)
            {
                p -= (p - 1);
            }

            var corners = BoundingBox.GetCorners();
            var point = p switch
            {
                < 1 / 4f => Vector2.Lerp(corners[0], corners[1], p * 4),
                < 2 / 4f => Vector2.Lerp(corners[1], corners[2], (p - 1 / 4f) * 4),
                < 3 / 4f => Vector2.Lerp(corners[2], corners[3], (p - 2 / 4f) * 4),
                _ => Vector2.Lerp(corners[3], corners[0], (p - 3 / 4f) * 4),
            };

            return new AnchorPoint(point, Transform);
        }
    }
}
