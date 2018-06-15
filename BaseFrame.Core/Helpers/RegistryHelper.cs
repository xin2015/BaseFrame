using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFrame.Core.Helpers
{
    /// <summary>
    /// 注册表操作工具类
    /// </summary>
    public static class RegistryHelper
    {
        public static void Demo()
        {
            using (RegistryKey software = Registry.LocalMachine.OpenSubKey("Software", true))
            {
                RegistryKey company = software.OpenSubKey("Suncere", true);
                if (company == null)
                {
                    company = software.CreateSubKey("Suncere");
                }
                RegistryKey project = company.CreateSubKey("ContentTransfer");
                project.SetValue("Name", "产品交换系统");
                project.SetValue("Version", "1.0.0.0");
                project.Dispose();
                company.Dispose();
            }
        }
    }
}
