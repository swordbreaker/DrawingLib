using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingLib.Presets
{
    public record CirclePreset : FigureNodePreset
    {
        public float Radius { get; init; } = 5f;
    }

    public record DefaultCirclePreset : CirclePreset;
}
