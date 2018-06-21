using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFrame.Core.Helpers
{
    public class ColorHelper
    {
        public static Color[] DefaultColors { get; }

        static ColorHelper()
        {
            DefaultColors = new Color[16];
            DefaultColors[0] = Color.FromArgb(0, 31, 63);
            DefaultColors[1] = Color.FromArgb(0, 116, 217);
            DefaultColors[2] = Color.FromArgb(127, 219, 255);
            DefaultColors[3] = Color.FromArgb(57, 204, 204);
            DefaultColors[4] = Color.FromArgb(61, 153, 112);
            DefaultColors[5] = Color.FromArgb(46, 204, 64);
            DefaultColors[6] = Color.FromArgb(1, 255, 112);
            DefaultColors[7] = Color.FromArgb(255, 220, 0);
            DefaultColors[8] = Color.FromArgb(255, 133, 27);
            DefaultColors[9] = Color.FromArgb(255, 65, 54);
            DefaultColors[10] = Color.FromArgb(133, 20, 75);
            DefaultColors[11] = Color.FromArgb(240, 18, 190);
            DefaultColors[12] = Color.FromArgb(177, 13, 201);
            DefaultColors[13] = Color.FromArgb(17, 17, 17);
            DefaultColors[14] = Color.FromArgb(170, 170, 170);
            DefaultColors[15] = Color.FromArgb(221, 221, 221);
        }
    }
}
