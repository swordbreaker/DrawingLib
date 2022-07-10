using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingLib.Figures
{
    public interface IFigure
    {
        void Draw(ICanvas canvas);

        IEnumerable<PointF> AbsoluteAnchorPoints { get; }
        IEnumerable<PointF> AnchorPoints { get; }

        RectF AbsoluteBoundingBox { get; }
        RectF BoundingBox { get; }

        public Transform Transform { get; }

        public IFigure? Parent { get; set; }

        public IReadOnlyCollection<IFigure> Childrens { get; }

        public IFigure Add(params IFigure[] figures);

        public IFigure Remove(params IFigure[] figures);
    }
}
