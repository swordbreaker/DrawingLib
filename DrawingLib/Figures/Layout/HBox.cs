using System.Numerics;

namespace DrawingLib.Figures.Layout
{
    public record HBox(float Marign, float ElementMargin) : Figure(Vector2.Zero), ILayout
    {
        public HBox(IEnumerable<IFigure> figures, float margin = 20f, float elementMargin = 20f) : this(margin, elementMargin)
        {
            Add(figures.ToArray());
            BoundingBox = Childrens.Aggregate(Rect.Zero, (old, @new) => old.Union(@new.AbsoluteBoundingBox));
            AnchorPoints = new[]
            {
                new PointF(BoundingBox.Left, BoundingBox.Center.Y),
                new PointF(BoundingBox.Right, BoundingBox.Center.Y),
                new PointF(BoundingBox.Center.X, BoundingBox.Top),
                new PointF(BoundingBox.Center.X, BoundingBox.Bottom),
            };
        }

        public static HBox Create(float margin = 10f, float elementMargin = 20f) => 
            new(Enumerable.Empty<IFigure>(), margin, elementMargin);

        public void Layout()
        {
            var figs = Childrens.ToArray();
            if(figs.Length > 0)
            {
                var currentXPos = Marign;
                for(int i = 0; i < figs.Length; i++)
                {
                    figs[i].Transform.Position = new Vector2(currentXPos, 0) + Transform.Position;

                    if (i < figs.Length - 1)
                    {
                        currentXPos += figs[i].BoundingBox.Width / 2 + figs[i+1].BoundingBox.Width/2 + ElementMargin;
                    }
                }
            }
        }

        public override IEnumerable<PointF> AnchorPoints { get; } = Enumerable.Empty<PointF>();

        public override RectF BoundingBox { get; }

        protected override void Render(ICanvas canvas)
        {
            foreach (var figure in Childrens)
            {
                figure.Draw(canvas);
            }
        }
    }
}
