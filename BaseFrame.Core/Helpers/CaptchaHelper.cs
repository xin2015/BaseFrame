using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFrame.Core.Helpers
{
    public class CaptchaHelper
    {
        static char[] _numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static char[] _letters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        static Random _rand = new Random();

        private static void CreateImage(string path, char[] chars)
        {
            int size = 32;
            Bitmap bmap = new Bitmap(chars.Length * size, size);
            Graphics g = Graphics.FromImage(bmap);
            try
            {
                //清空背景
                g.Clear(Color.White);

                //背景噪音线
                for (int i = 0; i < 3; i++)
                {
                    g.DrawLine(new Pen(Color.Silver), _rand.Next(bmap.Width / 2), _rand.Next(bmap.Height), _rand.Next(bmap.Width / 2, bmap.Width), _rand.Next(bmap.Height));
                }

                Font[] fonts = FontHelper.DefaultFontFamilies.Select(o => new Font(o, _rand.Next(size / 2, size - 6))).ToArray();

                //验证码
                for (int i = 0; i < chars.Length; i++)
                {
                    Font font = fonts[_rand.Next(fonts.Length)];
                    g.DrawString(chars[i].ToString(), font, new SolidBrush(ColorHelper.DefaultColors[_rand.Next(16)]), size * i + _rand.Next(size - (int)font.Size - 6), _rand.Next(size - (int)font.Size - 6));
                }

                //前景噪音点
                for (int i = 0; i < 50; i++)
                {
                    bmap.SetPixel(_rand.Next(bmap.Width), _rand.Next(bmap.Height), ColorHelper.DefaultColors[_rand.Next(16)]);
                }

                //边框线
                g.DrawRectangle(new Pen(Color.LightGray), 0, 0, bmap.Width - 1, bmap.Height - 1);

                bmap.Save(path);
            }
            catch (Exception e)
            {
                string m = e.Message;
            }
        }

        private static string Captcha(string path, int length, char[] source)
        {
            char[] chars = new char[length];
            Random _rand = new Random();
            for (int i = 0; i < length; i++)
            {
                chars[i] = source[_rand.Next(source.Length)];
            }
            CreateImage(path, chars);
            return new string(chars);
        }

        public static string Captcha(string path, int length = 4, int type = 3)
        {
            char[] source;
            switch (type)
            {
                case 1: { source = _numbers; break; }
                case 2: { source = _letters; break; }
                default: { source = _numbers.Concat(_letters).ToArray(); break; }
            }
            return Captcha(path, length, source);
        }
    }
}
