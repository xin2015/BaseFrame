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
            FTPHelper helper = new FTPHelper("ftp://202.104.69.206:21", "weatheruser", "weatheruser!2017");
            string a = helper.GetString("", "NLST");
            string b = helper.GetString("", "LIST");
            string c = helper.GetString("FA", "NLST");
            string d = helper.GetString("FA", "LIST");
        }
    }
}
