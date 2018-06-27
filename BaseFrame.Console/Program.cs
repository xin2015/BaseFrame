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
                System.Console.WriteLine(CaptchaHelper.Captcha(string.Format("D:\\captcha41{0}.jpg", i), 4, 1));
                System.Console.WriteLine(CaptchaHelper.Captcha(string.Format("D:\\captcha52{0}.jpg", i), 5, 2));
                System.Console.WriteLine(CaptchaHelper.Captcha(string.Format("D:\\captcha63{0}.jpg", i), 6, 3));
                System.Console.WriteLine(CaptchaHelper.Captcha(string.Format("D:\\captcha74{0}.jpg", i), 7, 4));
            }
            System.Console.ReadLine();
        }
    }
}
