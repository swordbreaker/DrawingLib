using DrawingLib.Figures.Nodes;
using DrawingLib.Util;
using System.Numerics;

namespace DrawingLib.Figures.Layout
{
    public record VBox(float Marign = 10f, float ElementMargin = 20f) : Figure(Vector2.Zero), ILayout
    {
        public Color StrokeColor { get; init; } = Colors.Transparent;
        public Color FillColor { get; init; } = Colors.Transparent;

        public VBox(IEnumerable<IFigure> figures, float margin = 20f, float elementMargin = 20f) : this(margin, elementMargin)
        {
            Add(figures.ToArray());
        }

        public void Layout()
        {
            var figs = Childrens.ToArray();

            foreach (var fig in figs.OfType<ILayout>())
            {
                fig.Layout();
            }

            if (figs.Length > 0)
            {
                var currentYPos = Marign;
                for (int i = 0; i < figs.Length; i++)
                {
                    figs[i].Transform.SetPositionY(currentYPos);

                    if (i < figs.Length - 1)
                    {
                        var bBoxA = figs[i].GetTranslatedBoundingBox(Transform);
                        var bBoxB = figs[i].GetTranslatedBoundingBox(Transform);

                        //var bBoxA = figs[i].BoundingBox;
                        //var bBoxB = figs[i].BoundingBox;
                        currentYPos += bBoxA.Height / 2 + bBoxB.Height / 2 + ElementMargin;
                    }
                }
            }
        }

        private RectF GetTransformedRect(IFigure figure)
        {
            var m = figure.Transform.GetTransformationMatrixRelativeTo(Transform);
            return figure.BoundingBox.Transform(m);
        }

        private RectF CalculateBoundingBox()
        {
            if (Childrens.Count > 0)
            {
                //var h = Childrens.Select(f => f.BoundingBox.Height).Sum() + Childrens.Count * ElementMargin;
                var h = Childrens.Select(f => f.GetTranslatedBoundingBox(Transform).Height).Sum() + Childrens.Count * ElementMargin;
                var w = Childrens.Select(f => f.GetTranslatedBoundingBox(Transform).Width).Max();
                return new RectF(new PointF(-w / 2f, -Childrens.First().BoundingBox.Height / 2), new SizeF(w, h));
            }

            return RectF.Zero;
        }

        //public override RectF BoundingBox => Childrens.Select(GetTransformedRect).Union();
        public override RectF BoundingBox => CalculateBoundingBox();

        protected override void Render(ICanvas canvas)
        {
            canvas.FillColor = FillColor;
            canvas.StrokeColor = StrokeColor;
            var rect = BoundingBox.Inflate(new SizeF(10));
            canvas.FillRoundedRectangle(rect, 5f);
            canvas.DrawRoundedRectangle(rect, 5f);

            foreach (var figure in Childrens)
            {
                figure.Draw(canvas);
            }
        }
    }
}
