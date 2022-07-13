using DrawingLib.Presets;

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

        public IEnumerable<PointF> AbsoluteAnchorPoints => AnchorPoints
            .Select(p => p.TransformBy(Transform.AbsoluteTransformationMatrix));

        public abstract IEnumerable<PointF> AnchorPoints { get; }

        public static FigureConnection operator >>(FigureNode a, FigureNode b) => new(a, b);

        protected override void Render(ICanvas canvas)
        {
            canvas.StrokeColor = StrokeColor ?? Colors.Black;
            canvas.FillColor = FillColor ?? Colors.Transparent;
            canvas.StrokeSize = StrokeSize;

            DrawFigure(canvas);
            PlaceText(canvas);
        }

        protected abstract void DrawFigure(ICanvas canvas);

        private RectF GetTextRect()
        {
            var rectWithMargin = BoudingBoxWithoutText.Inflate(TextMargin * -1);
            var outerRect = BoudingBoxWithoutText.Union(new Rect(-25f, 0, 50f, 10f));

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
