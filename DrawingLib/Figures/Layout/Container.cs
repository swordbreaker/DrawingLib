using DrawingLib.Util;

namespace DrawingLib.Figures.Layout
{
    public record class Container(PointF Position) : Figure(Position)
    {
        public override RectF BoundingBox => 
            this.Childrens.Aggregate(RectF.Zero, (old, @new) => old.Union(@new.AbsoluteBoundingBox));

        protected override void Render(ICanvas canvas)
        {
        }
    }
}
