using BaseFrame.Core.CryptoTransverters;
using BaseFrame.Core.Helpers;
using BaseFrame.Core.Models;
using BaseFrame.DAL;
using BaseFrame.DAL.Entities;
using BaseFrame.DAL.Repositories;
using BaseFrame.Web.Attributes;
using BaseFrame.Web.Extensions;
using BaseFrame.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaseFrame.Web.Controllers
{
    public class HomeController : Controller
    {
        [SuncereAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        [SuncereAuthorize]
        public ActionResult Page404()
        {
            return View();
        }

        public ActionResult TestError()
        {
            throw new HttpException("测试报错！");
        }

        #region Login / Logout
        public ActionResult Login(string returnUrl)
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Request.ApplicationPath;
            }
            SuncereUser user = Session.GetCurrentUser();
            if (user == null)
            {
                ViewData["returnUrl"] = returnUrl;
                return View();
            }
            else
            {
                return Redirect(returnUrl);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string userName, string password, string returnUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(userName))
                {
                    throw new Exception("请输入用户名");
                }
                if (string.IsNullOrEmpty(password))
                {
                    throw new Exception("请输入密码");
                }
                FluentModel db = Session.GetFluentModel();
                SuncereUserRepository userRepository = new SuncereUserRepository(db);
                SuncereUser user = userRepository.FirstOrDefault(userName, true);
                if (user == null)
                {
                    throw new Exception("用户名不存在或已停用，请核对后重新登录");
                }
                if (AsymmetricEncryption.Default.Decrypt(user.Password) != password)
                {
                    throw new Exception("密码错误，请核对后重新登录");
                }
                user.LastLoginTime = DateTime.Now;
                user.LastLoginHostAddress = Request.UserHostAddress;
                db.SaveChanges();

                Session.SetCurrentUser(user);

                List<SuncerePermission> userPermissions = new List<SuncerePermission>();
                foreach (SuncereRole role in user.SuncereRoles.Where(o => o.Status))
                {
                    foreach (SuncerePermission permission in role.SuncerePermissions.Where(o => o.Status))
                    {
                        if (!userPermissions.Contains(permission))
                        {
                            userPermissions.Add(permission);
                        }
                    }
                }
                Session.SetUserPermissions(userPermissions);

                return Redirect(returnUrl);
            }
            catch (Exception e)
            {
                ViewData["message"] = e.Message;
                return View();
            }
        }

        #region 前端加密
        public ActionResult Login2(string returnUrl)
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Request.ApplicationPath;
            }
            SuncereUser user = Session.GetCurrentUser();
            if (user == null)
            {
                ViewData["returnUrl"] = returnUrl;
                ViewData["PublicKey"] = RSACSharpJavaConvertHelper.RSAPublicKeyCSharpToJava(AsymmetricEncryption.Default.ExportParameters(false));
                return View();
            }
            else
            {
                return Redirect(returnUrl);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login2(string cipher, string returnUrl)
        {
            AjaxResult ar;
            try
            {
                string text = AsymmetricEncryption.Default.Decrypt(cipher);
                LoginInfo li = JsonConvert.DeserializeObject<LoginInfo>(text);
                if (string.IsNullOrEmpty(li.UserName))
                {
                    throw new Exception("请输入用户名");
                }
                if (string.IsNullOrEmpty(li.Password))
                {
                    throw new Exception("请输入密码");
                }
                FluentModel db = Session.GetFluentModel();
                SuncereUserRepository userRepository = new SuncereUserRepository(db);
                SuncereUser user = userRepository.FirstOrDefault(li.UserName, true);
                if (user == null)
                {
                    throw new Exception("用户名不存在或已停用，请核对后重新登录");
                }
                if (AsymmetricEncryption.Default.Decrypt(user.Password) != li.Password)
                {
                    throw new Exception("密码错误，请核对后重新登录");
                }
                user.LastLoginTime = DateTime.Now;
                user.LastLoginHostAddress = Request.UserHostAddress;
                db.SaveChanges();

                Session.SetCurrentUser(user);

                List<SuncerePermission> userPermissions = new List<SuncerePermission>();
                foreach (SuncereRole role in user.SuncereRoles.Where(o => o.Status))
                {
                    foreach (SuncerePermission permission in role.SuncerePermissions.Where(o => o.Status))
                    {
                        if (!userPermissions.Contains(permission))
                        {
                            userPermissions.Add(permission);
                        }
                    }
                }
                Session.SetUserPermissions(userPermissions);

                ar = AjaxResult.GetLoginAjaxResult(true);
            }
            catch (Exception e)
            {
                ar = new AjaxResult(false, e.Message);
            }
            return Json(ar, JsonRequestBehavior.DenyGet);
        }
        #endregion

        public ActionResult Logout()
        {
            Session.CloseFluentModel();
            Session.SetCurrentUser(null);
            Session.SetUserPermissions(null);

            return RedirectToAction("Login");
        }
        #endregion

        [SuncereAuthorize]
        public ActionResult CurrentUserEdit()
        {
            return View(Session.GetCurrentUser());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SuncereAuthorize]
        public ActionResult CurrentUserEdit(int id, SuncereUser model, string OldPassword)
        {
            AjaxResult result;
            try
            {
                FluentModel db = Session.GetFluentModel();
                SuncereUserRepository repository = new SuncereUserRepository(db);
                SuncereUser user = repository.FirstOrDefault(id);
                if (!string.IsNullOrEmpty(model.Password))
                {
                    if (OldPassword == AsymmetricEncryption.Default.Decrypt(user.Password))
                    {
                        user.Password = AsymmetricEncryption.Default.Encrypt(model.Password);
                    }
                    else
                    {
                        throw new Exception("旧密码错误。");
                    }
                }
                user.EmailAddress = model.EmailAddress;
                user.PhoneNumber = model.PhoneNumber;
                user.Remark = model.Remark;
                user.LastModifierUserId = id;
                db.SaveChanges();
                result = AjaxResult.GetEditAjaxResult(true);
            }
            catch (Exception e)
            {
                result = AjaxResult.GetEditAjaxResult(false);
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }
    }
}