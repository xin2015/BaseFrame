using BaseFrame.Core.CryptoTransverters;
using BaseFrame.Core.Extensions;
using BaseFrame.Core.Helpers;
using BaseFrame.DAL;
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

            FTPHelper helper = new FTPHelper("ftp://202.104.69.206/Data/Upload", "admin", "suncereltd@2017");
            helper.DownloadFile2("20180611.zip", "D:\\20180611.zip");
        }
    }
}
