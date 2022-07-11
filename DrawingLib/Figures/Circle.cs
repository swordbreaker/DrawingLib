namespace DrawingLib.Figures
{
    public record Circle(PointF Center, float Radius, string Text = "", float TextMargin = 10) : Figure(Center)
    {
        public override IEnumerable<PointF> AnchorPoints => 
            new[] {
                new PointF(-Radius, 0),
                new PointF(0, -Radius),
                new PointF(Radius, 0),
                new PointF(0, Radius),
            };

        public override RectF BoundingBox => new(-Radius, -Radius, Radius*2, Radius*2);

        public Color FillColor { get; init; } = Colors.Transparent;

        public Color StrokeColor { get; init; } = Colors.Transparent;

        protected override void Render(ICanvas canvas)
        {
            canvas.FillColor = FillColor;
            canvas.StrokeColor = StrokeColor;
            canvas.FillCircle(PointF.Zero, Radius);
            canvas.DrawCircle(PointF.Zero, Radius);
            canvas.DrawString(Text, 0, -(Radius + TextMargin), HorizontalAlignment.Center);
        }
    }
}
