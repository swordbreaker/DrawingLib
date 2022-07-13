using System.Numerics;

namespace DrawingLib.Figures.Layout
{
    public record VBox(float Marign = 10f, float ElementMargin = 20f) : Figure(Vector2.Zero), ILayout
    {
        public VBox(IEnumerable<IFigure> figures, float margin = 20f, float elementMargin = 20f) : this(margin, elementMargin)
        {
            Add(figures.ToArray());
        }

        public void Layout()
        {
            var figs = Childrens.ToArray();
            if (figs.Length > 0)
            {
                var currentYPos = Marign;
                for (int i = 0; i < figs.Length; i++)
                {
                    figs[i].Transform.Position = new Vector2(0, currentYPos) + Transform.Position;

                    if (i < figs.Length - 1)
                    {
                        currentYPos += figs[i].BoundingBox.Height / 2 + figs[i + 1].BoundingBox.Height / 2 + ElementMargin;
                    }
                }
            }
        }

        public override RectF BoundingBox => Childrens.Aggregate(Rect.Zero, (old, @new) => old.Union(@new.AbsoluteBoundingBox));

        protected override void Render(ICanvas canvas)
        {
            foreach (var figure in Childrens)
            {
                figure.Draw(canvas);
            }
        }
    }
}
