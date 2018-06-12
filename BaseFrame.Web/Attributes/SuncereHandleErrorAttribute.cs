using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaseFrame.Web.Attributes
{
    public class SuncereHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            //TODO 添加自定义异常处理
            base.OnException(filterContext);
        }
    }
}