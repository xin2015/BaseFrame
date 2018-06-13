using BaseFrame.Core.CryptoTransverters;
using BaseFrame.Core.Extensions;
using BaseFrame.Core.Helpers;
using BaseFrame.DAL;
using System;
using System.Collections.Generic;
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
            using (FluentModel db = new FluentModel())
            {
                db.UpdateSchema();
                db.InitData();
            }
        }
    }
}
