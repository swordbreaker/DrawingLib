using DrawingLib.Figures.AnchorPoints;
using DrawingLib.Presets;
using DrawingLib.Util;
using static DrawingLib.Util.MathMetrics;

namespace DrawingLib.Figures.Nodes
{
    public record Circle(PointF Center, CirclePreset CirclePreset) : FigureNode(Center, CirclePreset)
    {
        public static CirclePreset DefaultPreset = new DefaultCirclePreset();

        public Circle() : this(PointF.Zero, DefaultPreset) { }
        public Circle(PointF center) : this(center, DefaultPreset) { }

        public float Radius { get; init; } = CirclePreset.Radius;

        public override Anchor Anchor => new(GetAnchorPoint(0.75f), GetAnchorPoint(0f), GetAnchorPoint(0.25f), GetAnchorPoint(0.5f), GetAnchorPoint);

        /// <summary>
        /// </summary>
        /// <param name="p">
        /// 0 is at the top.
        /// 0.25 at the right
        /// 0.5 is at the bottom
        /// 0.75 is at the right
        /// 1 is at the top again.
        /// </param>
        /// <returns></returns>
        public override AnchorPoint GetAnchorPoint(float p) =>
            new(GetPointOnCircle(p), Transform);

        public AnchorPoint GetAnchorPoint(Pi pi) =>
            new(GetPointOnCircle(pi / 2 * PI), Transform);

        private Vector2 GetPointOnCircle(float p) =>
            new Vector2(MathF.Cos(RatioToPi(p)), MathF.Sin(RatioToPi(p))) * Radius;

        public static float RatioToPi(float p) => p * 2 * MathF.PI - MathF.PI/2;

        protected override RectF BoudingBoxWithoutText => new(-Radius, -Radius, Radius * 2, Radius * 2);

        protected override void DrawFigure(ICanvas canvas)
        {
            canvas.FillCircle(PointF.Zero, Radius);
            canvas.DrawCircle(PointF.Zero, Radius);
        }
    }
}
