﻿using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFrame.Core.Helpers
{
    /// <summary>
    /// 快捷方式工具类
    /// </summary>
    public static class ShortcutHelper
    {
        /// <summary>
        /// 创建InternetShortcut
        /// </summary>
        /// <param name="path">快捷方式路径</param>
        /// <param name="targetPath">目标URL</param>
        public static void CreateURLShortcut(string path, string targetPath)
        {
            WshShell shell = new WshShell();
            WshURLShortcut urlShortcut = shell.CreateShortcut(path) as WshURLShortcut;
            urlShortcut.TargetPath = targetPath;
            urlShortcut.Save();
        }

        /// <summary>
        /// 创建InternetShortcut
        /// </summary>
        /// <param name="path">快捷方式路径</param>
        /// <param name="targetPath">目标URL</param>
        /// <param name="iconPath">自定义图标路径</param>
        public static void CreateURLShortcut(string path, string targetPath, string iconPath)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine("[InternetShortcut]");
                sw.WriteLine("IDList=");
                sw.WriteLine("URL=" + targetPath);
                sw.WriteLine("IconIndex=0");
                sw.WriteLine("HotKey=0");
                sw.WriteLine("IconFile=" + iconPath);
                sw.Flush();
            }
        }

        /// <summary>
        /// 创建快捷方式
        /// </summary>
        /// <param name="path">快捷方式路径</param>
        /// <param name="targetPath">目标路径</param>
        public static void CreateShortcut(string path, string targetPath)
        {
            WshShell shell = new WshShell();
            WshShortcut shortcut = shell.CreateShortcut(path) as WshShortcut;
            shortcut.TargetPath = targetPath;
            shortcut.Save();
        }

        public static void Demo()
        {
            string path = string.Format("{0}\\jiandan.url", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
            string targetPath = "http://jiandan.net/";
            CreateURLShortcut(path, targetPath);
            path = "D:\\chrometest.lnk";
            targetPath = "C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe";
            CreateShortcut(path, targetPath);
        }
    }
}
