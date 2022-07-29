namespace DrawingLib.Figures
{
    public interface IFigure
    {
        void Draw(ICanvas canvas);

        RectF AbsoluteBoundingBox { get; }
        RectF BoundingBox { get; }

        Transform Transform { get; }

        IFigure? Parent { get; set; }

        IReadOnlyCollection<IFigure> Childrens { get; }

        IFigure Add(params IFigure[] figures);

        IFigure Remove(params IFigure[] figures);

        RectF GetTranslatedBoundingBox(Transform? transform = null);
    }
}
