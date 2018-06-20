using BaseFrame.Core.CryptoTransverters;
using BaseFrame.Core.Extensions;
using BaseFrame.Core.Helpers;
using BaseFrame.DAL;
using DotNet.Utilities;
using System;
using System.Collections.Generic;
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
                System.Console.WriteLine(CaptchaHelper.Captcha("", 4, 1));
                System.Console.WriteLine(CaptchaHelper.Captcha("", 4, 2));
                System.Console.WriteLine(CaptchaHelper.Captcha(""));
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
