using DrawingLib.Util;
using System.Numerics;

namespace DrawingLib.Figures
{
    public record Box : Figure
    {
        public Box(PointF pos, SizeF size) : base(pos)
        {
            Rect = new Rect(-size.Width / 2, -size.Height / 2, size.Width, size.Height);
        }

        public static Box Create(RectF rect) => 
            new Box(rect.Center, rect.Size);

        public RectF Rect { get; init; }
        public Color StrokeColor { get; init; } = Colors.Black;
        public Color FillColor { get; init; } = Colors.Transparent;
        public float CornerRadius { get; init; } = 5f;
        public float TextMargin { get; init; } = 5f;
        public TextFlow TextFlow { get; init; } = TextFlow.ClipBounds;
        public TextPosition TextPosition { get; init; } = TextPosition.Center;
        public string Text { get; init; } = "";

        public Box Inflate(SizeF size) => this with { Rect = Rect.Inflate(size) };

        public override IEnumerable<PointF> AnchorPoints => Rect
            .GetRectAnchorPoints();

        public override RectF BoundingBox => 
            Rect.Union(GetTextRect());

        protected override void Render(ICanvas canvas)
        {
            canvas.StrokeColor = StrokeColor ?? Colors.Black;
            canvas.FillColor = FillColor ?? Colors.Transparent;

            canvas.FillRoundedRectangle(Rect, CornerRadius);
            canvas.DrawRoundedRectangle(Rect, CornerRadius);

            PlaceText(canvas);
        }

        private RectF GetTextRect()
        {
            var rectWithMargin = Rect.Inflate(new Size(-TextMargin));

            return TextPosition switch
            {
                TextPosition.Top => rectWithMargin,
                TextPosition.Bottom => rectWithMargin,
                TextPosition.Left => rectWithMargin,
                TextPosition.Right => rectWithMargin,
                TextPosition.Center => rectWithMargin,
                TextPosition.OuterTop => Rect.Offset(0, -(Rect.Height + TextMargin)),
                TextPosition.OuterLeft => Rect.Offset(-(Rect.Height + TextMargin), 0),
                TextPosition.OuterRight => Rect.Offset(Rect.Height + TextMargin, 0),
                TextPosition.OuterBottom => Rect.Offset(0, (Rect.Height + TextMargin)),
                _ => RectF.Zero,
            };
        }

        private void PlaceText(ICanvas canvas)
        {
            var rect = GetTextRect();

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
        }
    }
}
