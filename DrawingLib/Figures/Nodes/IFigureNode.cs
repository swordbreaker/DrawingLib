using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingLib.Figures.Nodes
{
    public interface IFigureNode : IFigure
    {
        IEnumerable<PointF> AbsoluteAnchorPoints { get; }
        IEnumerable<PointF> AnchorPoints { get; }
    }
}
