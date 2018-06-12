using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseFrame.Web.Models
{
    public class LoginInfo
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime Time { get; set; }
    }
}