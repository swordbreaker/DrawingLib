namespace DrawingLib.Presets
{
    public record BoxPreset : FigureNodePreset
    {
        public virtual SizeF Size { get; init; } = new(60, 40);
        public virtual float CornerRadius { get; init; } = 5f;
    }

    public record DefaultBoxPreset : BoxPreset;
}
