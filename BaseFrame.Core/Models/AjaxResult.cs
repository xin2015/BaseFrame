using BaseFrame.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFrame.Core.Models
{
    public class AjaxResult
    {
        public bool Status { get; set; }
        public string Message { get; set; }

        public AjaxResult(bool status, string message)
        {
            Status = status;
            Message = message;
        }

        public static AjaxResult GetAjaxResult(bool status, string manage)
        {
            return new AjaxResult(status, string.Format("{0}{1}！", manage, status ? LanguageHelper.L("Success") : LanguageHelper.L("Fail")));
        }

        public static AjaxResult GetAddAjaxResult(bool status)
        {
            return GetAjaxResult(status, LanguageHelper.L("Add"));
        }

        public static AjaxResult GetEditAjaxResult(bool status)
        {
            return GetAjaxResult(status, LanguageHelper.L("Edit"));
        }

        public static AjaxResult GetDeleteAjaxResult(bool status)
        {
            return GetAjaxResult(status, LanguageHelper.L("Delete"));
        }

        public static AjaxResult GetLoginAjaxResult(bool status)
        {
            return GetAjaxResult(status, LanguageHelper.L("Login"));
        }
    }
}
