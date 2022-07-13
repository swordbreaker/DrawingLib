using DrawingLib.Presets;

namespace DrawingLib.Figures.Nodes
{
    public record Circle(PointF Center, CirclePreset CirclePreset) : FigureNode(Center)
    {
        public static CirclePreset DefaultPreset = new DefaultCirclePreset();

        public Circle() : this(PointF.Zero, DefaultPreset) { }
        public Circle(PointF center) : this(center, DefaultPreset) { }

        public float Radius { get; init; } = CirclePreset.Radius;

        public override IEnumerable<PointF> AnchorPoints =>
            new[] {
                new PointF(-Radius, 0),
                new PointF(0, -Radius),
                new PointF(Radius, 0),
                new PointF(0, Radius),
            };

        protected override RectF BoudingBoxWithoutText => new(-Radius, -Radius, Radius * 2, Radius * 2);

        protected override void DrawFigure(ICanvas canvas)
        {
            canvas.FillCircle(PointF.Zero, Radius);
            canvas.DrawCircle(PointF.Zero, Radius);
        }
    }
}
