using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BaseFrame.Core.Helpers
{
    /// <summary>
    /// FTP工具类
    /// </summary>
    public class FTPHelper
    {
        private NetworkCredential _networkCredential;
        private string _baseUriString;

        public FTPHelper(string baseUriString, string userName, string password)
        {
            this._baseUriString = baseUriString;
            _networkCredential = new NetworkCredential(userName, password);
        }

        private string GetPath(string path)
        {
            return string.Format("{0}/{1}", _baseUriString, path);
        }

        private FtpWebRequest GetRequest(string path, string method)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(GetPath(path));
            request.Credentials = _networkCredential;
            request.Method = method;
            return request;
        }

        private FtpWebResponse GetResponse(string path, string method)
        {
            return (FtpWebResponse)GetRequest(path, method).GetResponse();
        }

        /// <summary>
        /// 获取字符串信息
        /// </summary>
        /// <param name="path">ftp相对路径</param>
        /// <param name="method">方法</param>
        /// <returns></returns>
        public string GetString(string path, string method)
        {
            FtpWebResponse response = GetResponse(path, method);
            StreamReader sr = new StreamReader(response.GetResponseStream());
            string result = sr.ReadToEnd();
            sr.Close();
            response.Close();
            return result;
        }

        /// <summary>
        /// 获取字节数组
        /// </summary>
        /// <param name="path">ftp相对路径</param>
        /// <param name="method">方法</param>
        /// <returns></returns>
        public byte[] GetData(string path, string method)
        {
            FtpWebResponse response = GetResponse(path, method);
            byte[] result = new byte[response.ContentLength];
            response.GetResponseStream().Read(result, 0, (int)response.ContentLength);
            response.Close();
            return result;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">ftp相对路径</param>
        /// <returns></returns>
        public string DeleteFile(string path)
        {
            FtpWebResponse response = GetResponse(path, WebRequestMethods.Ftp.DeleteFile);
            string result = response.StatusDescription;
            response.Close();
            return result;
        }

        /// <summary>
        /// 获取路径文件列表
        /// </summary>
        /// <param name="path">ftp相对路径</param>
        /// <returns></returns>
        public string[] ListDirectory(string path)
        {
            string result = GetString(path, WebRequestMethods.Ftp.ListDirectory);
            return result.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="path">ftp相对路径</param>
        /// <param name="fileName">文件本地保存路径</param>
        public void DownloadFile(string path, string fileName)
        {
            using (WebClient wc = new WebClient())
            {
                wc.Credentials = _networkCredential;
                wc.DownloadFile(GetPath(path), fileName);
            }
        }

        /// <summary>
        /// 下载文件（断点续传）
        /// </summary>
        /// <param name="path">ftp相对路径</param>
        /// <param name="fileName">文件本地保存路径</param>
        public void DownloadFileBreakpointContinuingly(string path, string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Append);
            FtpWebRequest request = GetRequest(path, WebRequestMethods.Ftp.DownloadFile);
            request.ContentOffset = fs.Length;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            response.GetResponseStream().CopyTo(fs);
            response.Close();
            fs.Close();
        }

        /// <summary>
        /// 下载字节数组
        /// </summary>
        /// <param name="path">ftp相对路径</param>
        /// <returns></returns>
        public byte[] DownloadData(string path)
        {
            using (WebClient wc = new WebClient())
            {
                wc.Credentials = _networkCredential;
                return wc.DownloadData(GetPath(path));
            }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="path">ftp相对路径</param>
        /// <param name="fileName">文件本地保存路径</param>
        /// <returns></returns>
        public byte[] UploadFile(string path, string fileName)
        {
            using (WebClient wc = new WebClient())
            {
                wc.Credentials = _networkCredential;
                return wc.UploadFile(GetPath(path), fileName);
            }
        }

        /// <summary>
        /// 上传字节数组
        /// </summary>
        /// <param name="path">ftp相对路径</param>
        /// <param name="data">字节数组</param>
        /// <returns></returns>
        public byte[] UploadData(string path, byte[] data)
        {
            using (WebClient wc = new WebClient())
            {
                wc.Credentials = _networkCredential;
                return wc.UploadData(GetPath(path), data);
            }
        }
    }
}
