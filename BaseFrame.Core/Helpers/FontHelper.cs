using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFrame.Core.Helpers
{
    public class FontHelper
    {
        public static FontFamily[] DefaultFontFamilies { get; }

        static FontHelper()
        {
            List<FontFamily> list = new List<FontFamily>();
            list.Add(new FontFamily("Arial"));
            list.Add(new FontFamily("Arvo"));
            list.Add(new FontFamily("Axure Handwriting"));
            list.Add(new FontFamily("Bahnschrift Light"));
            list.Add(new FontFamily("Bodoni MT"));
            list.Add(new FontFamily("Book Antiqua"));
            list.Add(new FontFamily("Calisto MT"));
            list.Add(new FontFamily("Cambria Math"));
            list.Add(new FontFamily("Castellar"));
            list.Add(new FontFamily("Centaur"));
            list.Add(new FontFamily("Century"));
            list.Add(new FontFamily("Chiller"));
            list.Add(new FontFamily("Consolas"));
            list.Add(new FontFamily("Droid Serif"));
            list.Add(new FontFamily("Felix Titling"));
            list.Add(new FontFamily("Footlight MT Light"));
            list.Add(new FontFamily("Franklin Gothic Book"));
            list.Add(new FontFamily("Garamond"));
            list.Add(new FontFamily("Ink Free"));
            list.Add(new FontFamily("Lucida Handwriting"));
            list.Add(new FontFamily("Lucida Sans Typewriter"));
            list.Add(new FontFamily("Microsoft New Tai Lue"));
            list.Add(new FontFamily("Microsoft Sans Serif"));
            list.Add(new FontFamily("MS UI Gothic"));
            list.Add(new FontFamily("Perpetua Titling MT"));
            list.Add(new FontFamily("Poiret One"));
            list.Add(new FontFamily("Poor Richard"));
            list.Add(new FontFamily("Raleway"));
            list.Add(new FontFamily("Roboto"));
            list.Add(new FontFamily("SimSun-ExtB"));
            list.Add(new FontFamily("Tahoma"));
            list.Add(new FontFamily("Tempus Sans ITC"));
            list.Add(new FontFamily("Times New Roman"));
            list.Add(new FontFamily("Verdana"));
            list.Add(new FontFamily("Yu Gothic Medium"));
            list.Add(new FontFamily("等线 Light"));
            list.Add(new FontFamily("方正姚体"));
            list.Add(new FontFamily("黑体"));
            list.Add(new FontFamily("华文楷体"));
            list.Add(new FontFamily("华文宋体"));
            list.Add(new FontFamily("华文新魏"));
            list.Add(new FontFamily("华文细黑"));
            list.Add(new FontFamily("华文中宋"));
            list.Add(new FontFamily("隶书"));
            list.Add(new FontFamily("宋体"));
            list.Add(new FontFamily("細明體-ExtB"));
            list.Add(new FontFamily("新宋体"));
            list.Add(new FontFamily("新細明體-ExtB"));
            DefaultFontFamilies = list.ToArray();
        }
    }
}
