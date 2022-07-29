using DrawingLib.Figures.AnchorPoints;
using System.Numerics;

namespace DrawingLib.Figures.Nodes
{
    public interface IFigureNode : IFigure
    {
        AnchorPoint GetAnchorPoint(float p);
        Anchor Anchor { get; }
    }
}
