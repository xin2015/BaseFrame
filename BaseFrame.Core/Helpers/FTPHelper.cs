using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BaseFrame.Core.Helpers
{
    public class FTPHelper
    {
        private NetworkCredential networkCredential;
        private string baseUriString;

        public FTPHelper(string baseUriString, string userName, string password)
        {
            this.baseUriString = baseUriString;
            networkCredential = new NetworkCredential(userName, password);
        }

        private FtpWebResponse GetResponse(string path, string method)
        {
            string requestUriString = string.Format("{0}/{1}", baseUriString, path);
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(requestUriString);
            request.Credentials = networkCredential;
            request.Method = method;
            return (FtpWebResponse)request.GetResponse();
        }

        public string DeleteFile(string path)
        {
            FtpWebResponse response = GetResponse(path, WebRequestMethods.Ftp.DeleteFile);
            string result = response.StatusDescription;
            response.Close();
            return result;
        }

        public byte[] DownloadFile(string path)
        {
            return GetData(path, WebRequestMethods.Ftp.DownloadFile);
        }

        public string[] ListDirectory(string path)
        {
            string result = GetString(path, WebRequestMethods.Ftp.ListDirectory);
            return result.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        public string GetString(string path, string method, Encoding encoding)
        {
            FtpWebResponse response = GetResponse(path, method);
            StreamReader sr = new StreamReader(response.GetResponseStream(), encoding);
            string result = sr.ReadToEnd();
            sr.Close();
            response.Close();
            return result;
        }

        public string GetString(string path, string method)
        {
            FtpWebResponse response = GetResponse(path, method);
            StreamReader sr = new StreamReader(response.GetResponseStream());
            string result = sr.ReadToEnd();
            sr.Close();
            response.Close();
            return result;
        }

        public byte[] GetData(string path, string method)
        {
            FtpWebResponse response = GetResponse(path, method);
            byte[] result = new byte[response.ContentLength];
            response.GetResponseStream().Read(result, 0, (int)response.ContentLength);
            response.Close();
            return result;
        }


    }
}
