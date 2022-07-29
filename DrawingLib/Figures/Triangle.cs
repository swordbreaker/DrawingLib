using DrawingLib.Util;

namespace DrawingLib.Figures
{
    /// <summary>
    /// 
    /// <code>
    ///  __
    ///  \/
    ///   ^ (0, 0)
    /// </code>
    /// </summary>
    public record Triangle() : Figure(PointF.Zero)
    {
        protected override void Render(ICanvas canvas)
        {
            canvas.FillColor = Colors.Black;
            var arrow = new PathF();
            arrow.MoveTo(-5, -5);
            arrow.LineTo(0, 0);
            arrow.LineTo(5, -5);
            arrow.Close();
            canvas.FillPath(arrow);
        }

        protected IEnumerable<Vector2> Vertices => 
            new []
            {
                new Vector2(-5, -5),
                new Vector2(0, 0),
                new Vector2(5, -5),
            };

        public override RectF BoundingBox => MathUtil.CalculateBoundingBox(Vertices.ToArray());
    }
}
