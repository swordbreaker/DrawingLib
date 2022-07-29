using DrawingLib.Presets;
using DrawingLib.Util;

namespace DrawingLib.Figures.Nodes
{
    public record Box(PointF Position, BoxPreset BoxPreset) : FigureNode(Position, BoxPreset)
    {
        public static BoxPreset DefaultBoxPreset = new DefaultBoxPreset();

        public Box() : this(PointF.Zero, DefaultBoxPreset) { }
        public Box(BoxPreset boxPreset) : this(PointF.Zero, boxPreset) { }


        public Box(PointF position, SizeF size) : this(position, new DefaultBoxPreset())
        {
            Rect = new RectF((size * -1 / 2f).ToPoint(), size);
        }

        public Box(SizeF size) : this(PointF.Zero, size) { }

        public RectF Rect { get; init; } = new RectF((BoxPreset.Size * -1/2f).ToPoint(), BoxPreset.Size);
        public float CornerRadius { get; init; } = BoxPreset.CornerRadius;

        public Box Inflate(SizeF size) => this with { Rect = Rect.Inflate(size) };

        protected override RectF BoudingBoxWithoutText => Rect;

        protected override void DrawFigure(ICanvas canvas)
        {
            canvas.FillRoundedRectangle(Rect, CornerRadius);
            canvas.DrawRoundedRectangle(Rect, CornerRadius);
        }
    }
}
