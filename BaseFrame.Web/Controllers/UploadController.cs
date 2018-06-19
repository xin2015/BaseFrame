using BaseFrame.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaseFrame.Web.Controllers
{
    public class UploadController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            AjaxResult result;
            try
            {
                string filePath = "/Upload/";
                string absolutePath = Server.MapPath(filePath);
                if (!Directory.Exists(absolutePath))//判断上传文件夹是否存在，若不存在，则创建
                {
                    Directory.CreateDirectory(absolutePath);//创建文件夹
                }
                file.SaveAs(absolutePath + file.FileName);
                result = AjaxResult.GetAddAjaxResult(true);
            }
            catch (Exception e)
            {
                result = AjaxResult.GetAddAjaxResult(false);
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}