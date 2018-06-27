using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFrame.Core.Helpers
{
    /// <summary>
    /// 验证码工具类
    /// </summary>
    public class CaptchaHelper
    {
        static Random _rand = new Random();

        /// <summary>
        /// 获取验证码源
        /// </summary>
        /// <param name="type">验证码源类型，1数字、2大写字母、3数字和大写字母、4字符（数字和大小写字母和标点符号）</param>
        /// <returns></returns>
        private static char[] GetCaptchaSource(int type)
        {
            char[] source;
            switch (type)
            {
                case 1:
                    {
                        source = new char[10];
                        for (int i = 0; i < 10; i++)
                        {
                            source[i] = (char)(i + 48);
                        }
                        break;
                    }
                case 2:
                    {
                        source = new char[26];
                        for (int i = 0; i < 26; i++)
                        {
                            source[i] = (char)(i + 65);
                        }
                        break;
                    }
                case 3:
                    {
                        source = new char[36];
                        for (int i = 0; i < 10; i++)
                        {
                            source[i] = (char)(i + 48);
                        }
                        for (int i = 0; i < 26; i++)
                        {
                            source[i + 10] = (char)(i + 65);
                        }
                        break;
                    }
                case 4:
                    {
                        source = new char[95];
                        for (int i = 0; i < 95; i++)
                        {
                            source[i] = (char)(i + 32);
                        }
                        break;
                    }
                default: { source = new char[0]; break; }
            }
            return source;
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="length">验证码长度</param>
        /// <param name="source">验证码源</param>
        /// <returns></returns>
        private static char[] GetCaptcha(int length, char[] source)
        {
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = source[_rand.Next(source.Length)];
            }
            return chars;
        }

        /// <summary>
        /// 生成验证码图片
        /// </summary>
        /// <param name="path">验证码图片路径</param>
        /// <param name="chars">验证码</param>
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

        /// <summary>
        /// 获取验证码并生成验证码图片
        /// </summary>
        /// <param name="path">验证码图片路径</param>
        /// <param name="length">验证码长度</param>
        /// <param name="type">验证码源类型，1数字、2大写字母、3数字和大写字母、4字符（数字和大小写字母和标点符号）</param>
        /// <returns></returns>
        public static string Captcha(string path, int length = 4, int type = 3)
        {
            char[] chars = GetCaptcha(length, GetCaptchaSource(type));
            CreateImage(path, chars);
            return new string(chars);
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="length">验证码长度</param>
        /// <param name="type">验证码源类型，1数字、2大写字母、3数字和大写字母、4字符（数字和大小写字母和标点符号）</param>
        /// <returns></returns>
        public static string Captcha(int length = 4, int type = 3)
        {
            return new string(GetCaptcha(length, GetCaptchaSource(type)));
        }
    }
}
