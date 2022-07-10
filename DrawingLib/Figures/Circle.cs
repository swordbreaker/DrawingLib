namespace DrawingLib.Figures
{
    public record Circle(PointF Center, float Radius, string Text = "", float TextMargin = 10) : Figure(Center)
    {
        public override IEnumerable<PointF> AnchorPoints => 
            new[] { 
                new PointF(Radius, 0),
                new PointF(-Radius, 0),
                new PointF(0, Radius),
                new PointF(0, -Radius),
            };

        public override RectF BoundingBox => new(-Radius, -Radius, Radius*2, Radius*2);

        protected override void Render(ICanvas canvas)
        {
            canvas.DrawCircle(PointF.Zero, Radius);
            canvas.DrawString(Text, 0, -(Radius + TextMargin), HorizontalAlignment.Center);
        }
    }
}
