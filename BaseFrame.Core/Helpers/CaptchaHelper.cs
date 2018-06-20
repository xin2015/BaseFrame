using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFrame.Core.Helpers
{
    public class CaptchaHelper
    {
        static char[] _numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static char[] _letters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        private static string Captcha(string path, int length, char[] source)
        {
            char[] chars = new char[length];
            Random rand = new Random();
            for (int i = 0; i < length; i++)
            {
                chars[i] = source[rand.Next(source.Length)];
            }
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
