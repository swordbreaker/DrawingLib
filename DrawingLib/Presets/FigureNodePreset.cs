using DrawingLib.Figures;

namespace DrawingLib.Presets
{
    public record FigureNodePreset
    {
        public virtual Color StrokeColor { get; init; } = Colors.Black;
        public virtual Color FillColor { get; init; } = Colors.Transparent;
        public virtual float StrokeSize { get; init; } = 1f;
        public virtual SizeF TextMargin { get; init; } = new SizeF(5f);
        public virtual TextFlow TextFlow { get; init; } = TextFlow.ClipBounds;
        public virtual TextPosition TextPosition { get; init; } = TextPosition.Center;
        public virtual string Text { get; init; } = "";
    }

    public record DefaultFigureNodePreset : FigureNodePreset;
}
