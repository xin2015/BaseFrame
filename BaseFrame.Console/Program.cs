using BaseFrame.Core.CryptoTransverters;
using BaseFrame.Core.Extensions;
using BaseFrame.Core.Helpers;
using BaseFrame.DAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BaseFrame.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (FluentModel db = new FluentModel())
            //{
            //    db.UpdateSchema();
            //    db.InitData();
            //}

            //FTPHelper helper = new FTPHelper("ftp://202.104.69.206/Data/Upload", "admin", "suncereltd@2017");

            for (int i = 0; i < 10; i++)
            {
                System.Console.WriteLine(CaptchaHelper.Captcha(string.Format("D:\\captcha{0}1.jpg", i), 4, 1));
                System.Console.WriteLine(CaptchaHelper.Captcha(string.Format("D:\\captcha{0}2.jpg", i), 4, 2));
                System.Console.WriteLine(CaptchaHelper.Captcha(string.Format("D:\\captcha{0}3.jpg", i)));
            }

            //Random _rand = new Random();
            //foreach (FontFamily ff in FontHelper.DefaultFontFamilies)
            //{
            //    Bitmap bmap = new Bitmap(32 * 36, 32);
            //    Graphics g = Graphics.FromImage(bmap);

            //    // Create font and brush.
            //    Font drawFont = new Font(ff, 16);
            //    char[] chars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

            //    for (int i = 0; i < 36; i++)
            //    {
            //        SolidBrush drawBrush = new SolidBrush(ColorHelper.DefaultColors[1]);
            //        g.DrawString(chars[i].ToString(), drawFont, drawBrush, 32 * i + 10, 10);
            //    }

            //    bmap.Save(string.Format("D:\\{0}.png", ff.Name));
            //}
        }
    }
}
