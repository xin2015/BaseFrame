using BaseFrame.Core.CryptoTransverters;
using BaseFrame.Core.Enums;
using BaseFrame.Core.Models;
using BaseFrame.DAL;
using BaseFrame.DAL.Entities;
using BaseFrame.DAL.Repositories;
using BaseFrame.Web.Attributes;
using BaseFrame.Web.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaseFrame.Web.Controllers
{
    public class SystemController : Controller
    {
        #region 角色管理
        [SuncereAuthorize(IsDefault = true)]
        public ActionResult RoleList(DateTime? startTime, DateTime? endTime, string keyword)
        {
            FluentModel db = Session.GetFluentModel();
            SuncereRoleRepository repository = new SuncereRoleRepository(db);
            List<SuncereRole> list = repository.Query(startTime, endTime, keyword).ToList();
            ViewData["startTime"] = startTime;
            ViewData["endTime"] = endTime;
            ViewData["keyword"] = keyword;
            return View(list);
        }

        [SuncereAuthorize(Controller = "System", Action = "RoleList")]
        public ActionResult RoleAdd()
        {
            FluentModel db = Session.GetFluentModel();
            List<SuncerePermission> permissionList = db.SuncerePermissions.Where(o => o.Status).ToList();
            ViewData["SuncerePermissions"] = permissionList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SuncereAuthorize(Controller = "System", Action = "RoleList")]
        public ActionResult RoleAdd(SuncereRole model)
        {
            AjaxResult result;
            try
            {
                int[] permissionIds = Request.Params["SuncerePermissions"].Split(',').Select(o => int.Parse(o)).ToArray();
                FluentModel db = Session.GetFluentModel();
                SuncereRoleRepository repository = new SuncereRoleRepository(db);
                SuncerePermissionRepository permissionRepository = new SuncerePermissionRepository(db);
                if (repository.IsExist(model.Name))
                {
                    throw new Exception("名称已存在。");
                }
                model.Status = true;
                model.CreatorUserId = Session.GetCurrentUser().Id;
                foreach (int permissionId in permissionIds)
                {
                    SuncerePermission permission = permissionRepository.FirstOrDefault(permissionId);
                    model.SuncerePermissions.Add(permission);
                    permission.SuncereRoles.Add(model);
                }
                db.Add(model);
                db.SaveChanges();
                result = AjaxResult.GetAddAjaxResult(true);
            }
            catch (Exception e)
            {
                result = AjaxResult.GetAddAjaxResult(false);
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [SuncereAuthorize(Controller = "System", Action = "RoleList")]
        public ActionResult RoleEdit(int id)
        {
            FluentModel db = Session.GetFluentModel();
            SuncereRoleRepository repository = new SuncereRoleRepository(db);
            List<SuncerePermission> permissionList = db.SuncerePermissions.Where(o => o.Status).ToList();
            ViewData["SuncerePermissions"] = permissionList;
            return View(repository.FirstOrDefault(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SuncereAuthorize(Controller = "System", Action = "RoleList")]
        public ActionResult RoleEdit(int id, SuncereRole model)
        {
            AjaxResult result;
            try
            {
                int[] permissionIds = Request.Params["SuncerePermissions"].Split(',').Select(o => int.Parse(o)).ToArray();
                FluentModel db = Session.GetFluentModel();
                SuncereRoleRepository repository = new SuncereRoleRepository(db);
                SuncerePermissionRepository permissionRepository = new SuncerePermissionRepository(db);
                SuncereRole role = repository.FirstOrDefault(id);
                role.Remark = model.Remark;
                role.LastModifierUserId = Session.GetCurrentUser().Id;
                foreach (SuncerePermission permission in role.SuncerePermissions)
                {
                    permission.SuncereRoles.Remove(role);
                }
                role.SuncerePermissions.Clear();
                foreach (int permissionId in permissionIds)
                {
                    SuncerePermission permission = permissionRepository.FirstOrDefault(permissionId);
                    role.SuncerePermissions.Add(permission);
                    permission.SuncereRoles.Add(role);
                }
                db.SaveChanges();
                result = AjaxResult.GetEditAjaxResult(true);
            }
            catch (Exception e)
            {
                result = AjaxResult.GetEditAjaxResult(false);
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [SuncereAuthorize(Controller = "System", Action = "RoleList")]
        public ActionResult RoleDelete(string ids)
        {
            AjaxResult result;
            try
            {
                int[] idArray = ids.Split(',').Select(o => int.Parse(o)).ToArray();
                FluentModel db = Session.GetFluentModel();
                SuncereRoleRepository repository = new SuncereRoleRepository(db);
                IQueryable<SuncereRole> query = repository.Query(idArray);
                foreach (SuncereRole role in query)
                {
                    foreach (SuncereUser user in role.SuncereUsers)
                    {
                        user.SuncereRoles.Remove(role);
                    }
                    foreach (SuncerePermission permission in role.SuncerePermissions)
                    {
                        permission.SuncereRoles.Remove(role);
                    }
                    role.SuncereUsers.Clear();
                    role.SuncerePermissions.Clear();
                }
                db.Delete(query);
                db.SaveChanges();
                result = AjaxResult.GetDeleteAjaxResult(true);
            }
            catch (Exception e)
            {
                result = AjaxResult.GetDeleteAjaxResult(false);
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [SuncereAuthorize(Controller = "System", Action = "RoleList")]
        public ActionResult RoleEnable(int id, bool status)
        {
            AjaxResult result;
            try
            {
                FluentModel db = Session.GetFluentModel();
                SuncereRoleRepository repository = new SuncereRoleRepository(db);
                SuncereRole role = repository.FirstOrDefault(id);
                role.Status = status;
                role.LastModifierUserId = Session.GetCurrentUser().Id;
                db.SaveChanges();
                result = AjaxResult.GetEditAjaxResult(true);
            }
            catch (Exception e)
            {
                result = AjaxResult.GetEditAjaxResult(false);
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }
        #endregion

        #region 用户管理
        [SuncereAuthorize(IsDefault = true)]
        public ActionResult UserList(DateTime? startTime, DateTime? endTime, string keyword)
        {
            FluentModel db = Session.GetFluentModel();
            SuncereUserRepository repository = new SuncereUserRepository(db);
            List<SuncereUser> list = repository.Query(startTime, endTime, keyword).ToList();
            ViewData["startTime"] = startTime;
            ViewData["endTime"] = endTime;
            ViewData["keyword"] = keyword;
            return View(list);
        }

        [SuncereAuthorize(Controller = "System", Action = "UserList")]
        public ActionResult UserAdd()
        {
            FluentModel db = Session.GetFluentModel();
            List<SuncereRole> list = db.SuncereRoles.Where(o => o.Status).ToList();
            ViewData["SuncereRoles"] = list;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SuncereAuthorize(Controller = "System", Action = "UserList")]
        public ActionResult UserAdd(SuncereUser model)
        {
            AjaxResult result;
            try
            {
                int[] roleIds = Request.Params["SuncereRoles"].Split(',').Select(o => int.Parse(o)).ToArray();
                FluentModel db = Session.GetFluentModel();
                SuncereUserRepository repository = new SuncereUserRepository(db);
                if (repository.IsExist(model.UserName))
                {
                    throw new Exception("用户名已存在。");
                }
                model.Password = AsymmetricEncryption.Default.Encrypt(model.Password);
                model.Status = true;
                model.CreatorUserId = Session.GetCurrentUser().Id;
                SuncereRoleRepository roleRepository = new SuncereRoleRepository(db);
                foreach (int roleId in roleIds)
                {
                    SuncereRole role = roleRepository.FirstOrDefault(roleId);
                    role.SuncereUsers.Add(model);
                    model.SuncereRoles.Add(role);
                }
                db.Add(model);
                db.SaveChanges();
                result = AjaxResult.GetAddAjaxResult(true);
            }
            catch (Exception e)
            {
                result = AjaxResult.GetAddAjaxResult(false);
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [SuncereAuthorize(Controller = "System", Action = "UserList")]
        public ActionResult UserEdit(int id)
        {
            FluentModel db = Session.GetFluentModel();
            SuncereUserRepository repository = new SuncereUserRepository(db);
            List<SuncereRole> list = db.SuncereRoles.Where(o => o.Status).ToList();
            ViewData["SuncereRoles"] = list;
            return View(repository.FirstOrDefault(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SuncereAuthorize(Controller = "System", Action = "UserList")]
        public ActionResult UserEdit(int id, SuncereUser model)
        {
            AjaxResult result;
            try
            {
                int[] roleIds = Request.Params["SuncereRoles"].Split(',').Select(o => int.Parse(o)).ToArray();
                FluentModel db = Session.GetFluentModel();
                SuncereUserRepository repository = new SuncereUserRepository(db);
                SuncereRoleRepository roleRepository = new SuncereRoleRepository(db);
                SuncereUser user = repository.FirstOrDefault(id);
                if (!string.IsNullOrEmpty(model.Password))
                {
                    user.Password = AsymmetricEncryption.Default.Encrypt(model.Password);
                }
                user.EmailAddress = model.EmailAddress;
                user.PhoneNumber = model.PhoneNumber;
                foreach (SuncereRole role in user.SuncereRoles)
                {
                    role.SuncereUsers.Remove(user);
                }
                user.SuncereRoles.Clear();
                foreach (int roleId in roleIds)
                {
                    SuncereRole role = roleRepository.FirstOrDefault(roleId);
                    user.SuncereRoles.Add(role);
                    role.SuncereUsers.Add(user);
                }
                user.Remark = model.Remark;
                user.LastModifierUserId = Session.GetCurrentUser().Id;
                db.SaveChanges();
                result = AjaxResult.GetEditAjaxResult(true);
            }
            catch (Exception e)
            {
                result = AjaxResult.GetEditAjaxResult(false);
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [SuncereAuthorize(Controller = "System", Action = "UserList")]
        public ActionResult UserDelete(string ids)
        {
            AjaxResult result;
            try
            {
                int[] idArray = ids.Split(',').Select(o => int.Parse(o)).ToArray();
                FluentModel db = Session.GetFluentModel();
                SuncereUserRepository repository = new SuncereUserRepository(db);
                IQueryable<SuncereUser> query = repository.Query(idArray);
                foreach (SuncereUser user in query)
                {
                    foreach (SuncereRole role in user.SuncereRoles)
                    {
                        role.SuncereUsers.Remove(user);
                    }
                    user.SuncereRoles.Clear();
                }
                db.Delete(query);
                db.SaveChanges();
                result = AjaxResult.GetDeleteAjaxResult(true);
            }
            catch (Exception e)
            {
                result = AjaxResult.GetDeleteAjaxResult(false);
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [SuncereAuthorize(Controller = "System", Action = "UserList")]
        public ActionResult UserEnable(int id, bool status)
        {
            AjaxResult result;
            try
            {
                FluentModel db = Session.GetFluentModel();
                SuncereUserRepository repository = new SuncereUserRepository(db);
                SuncereUser role = repository.FirstOrDefault(id);
                role.Status = status;
                role.LastModifierUserId = Session.GetCurrentUser().Id;
                db.SaveChanges();
                result = AjaxResult.GetEditAjaxResult(true);
            }
            catch (Exception e)
            {
                result = AjaxResult.GetEditAjaxResult(false);
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }
        #endregion

        #region 权限管理
        [SuncereAuthorize(IsDefault = true)]
        public ActionResult PermissionList(DateTime? startTime, DateTime? endTime, string keyword)
        {
            FluentModel db = Session.GetFluentModel();
            SuncerePermissionRepository repository = new SuncerePermissionRepository(db);
            List<SuncerePermission> list = repository.Query(startTime, endTime, keyword).ToList();
            ViewData["startTime"] = startTime;
            ViewData["endTime"] = endTime;
            ViewData["keyword"] = keyword;
            return View(list);
        }

        [SuncereAuthorize(Controller = "System", Action = "PermissionList")]
        public ActionResult PermissionAdd()
        {
            FluentModel db = Session.GetFluentModel();
            List<SuncerePermission> modules = db.SuncerePermissions.Where(o => o.Status && o.Type == (int)PermissionType.Module).ToList();
            return View(modules);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SuncereAuthorize(Controller = "System", Action = "PermissionList")]
        public ActionResult PermissionAdd(SuncerePermission model)
        {
            AjaxResult result;
            try
            {
                FluentModel db = Session.GetFluentModel();
                SuncerePermissionRepository repository = new SuncerePermissionRepository(db);
                if (repository.IsExist(model.Controller, model.Action))
                {
                    throw new Exception("权限已存在。");
                }
                if (model.Type == (int)PermissionType.Module)
                {
                    model.ParentId = 0;
                    model.Action = null;
                }
                model.Status = true;
                model.CreatorUserId = Session.GetCurrentUser().Id;
                SuncereRole role = db.SuncereRoles.First();
                role.SuncerePermissions.Add(model);
                model.SuncereRoles.Add(role);
                db.Add(model);
                db.SaveChanges();
                result = AjaxResult.GetAddAjaxResult(true);
            }
            catch (Exception e)
            {
                result = AjaxResult.GetAddAjaxResult(false);
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [SuncereAuthorize(Controller = "System", Action = "PermissionList")]
        public ActionResult PermissionEdit(int id)
        {
            FluentModel db = Session.GetFluentModel();
            SuncerePermissionRepository repository = new SuncerePermissionRepository(db);
            List<SuncerePermission> modules = db.SuncerePermissions.Where(o => o.Status && o.Type == (int)PermissionType.Module).ToList();
            ViewData["SuncerePermissions"] = modules;
            return View(repository.FirstOrDefault(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SuncereAuthorize(Controller = "System", Action = "PermissionList")]
        public ActionResult PermissionEdit(int id, SuncerePermission model)
        {
            AjaxResult result;
            try
            {
                FluentModel db = Session.GetFluentModel();
                SuncerePermissionRepository repository = new SuncerePermissionRepository(db);
                SuncerePermission permission = repository.FirstOrDefault(id);
                permission.Name = model.Name;
                permission.ParentId = model.ParentId;
                permission.Controller = model.Controller;
                permission.Action = model.Action;
                permission.Order = model.Order;
                permission.Remark = model.Remark;
                permission.LastModifierUserId = Session.GetCurrentUser().Id;
                db.SaveChanges();
                result = AjaxResult.GetEditAjaxResult(true);
            }
            catch (Exception e)
            {
                result = AjaxResult.GetEditAjaxResult(false);
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [SuncereAuthorize(Controller = "System", Action = "PermissionList")]
        public ActionResult PermissionDelete(string ids)
        {
            AjaxResult result;
            try
            {
                int[] idArray = ids.Split(',').Select(o => int.Parse(o)).ToArray();
                FluentModel db = Session.GetFluentModel();
                SuncerePermissionRepository repository = new SuncerePermissionRepository(db);
                IQueryable<SuncerePermission> query = repository.Query(idArray);
                foreach (SuncerePermission permission in query)
                {
                    foreach (SuncereRole role in permission.SuncereRoles)
                    {
                        role.SuncerePermissions.Remove(permission);
                    }
                    permission.SuncereRoles.Clear();
                }
                db.Delete(query);
                db.SaveChanges();
                result = AjaxResult.GetDeleteAjaxResult(true);
            }
            catch (Exception e)
            {
                result = AjaxResult.GetDeleteAjaxResult(false);
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [SuncereAuthorize(Controller = "System", Action = "PermissionList")]
        public ActionResult PermissionEnable(int id, bool status)
        {
            AjaxResult result;
            try
            {
                FluentModel db = Session.GetFluentModel();
                SuncerePermissionRepository repository = new SuncerePermissionRepository(db);
                SuncerePermission role = repository.FirstOrDefault(id);
                role.Status = status;
                role.LastModifierUserId = Session.GetCurrentUser().Id;
                db.SaveChanges();
                result = AjaxResult.GetEditAjaxResult(true);
            }
            catch (Exception e)
            {
                result = AjaxResult.GetEditAjaxResult(false);
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }
        #endregion

        #region 日志管理
        [SuncereAuthorize(IsDefault = true)]
        public ActionResult AuditLogList(DateTime? startTime, DateTime? endTime, string keyword)
        {
            FluentModel db = Session.GetFluentModel();
            SuncereAuditLogRepository repository = new SuncereAuditLogRepository(db);
            List<SuncereAuditLog> list = repository.Query(startTime, endTime, keyword).ToList();
            ViewData["startTime"] = startTime;
            ViewData["endTime"] = endTime;
            ViewData["keyword"] = keyword;
            return View(list);
        }

        #region 后台分页
        [SuncereAuthorize]
        public ActionResult AuditLogList2()
        {
            return View();
        }

        [HttpPost]
        [SuncereAuthorize]
        public ActionResult AuditLogList2(DateTime? startTime, DateTime? endTime, string keyword, int draw, int start, int length, Dictionary<string, string>[] order)
        {
            FluentModel db = Session.GetFluentModel();
            SuncereAuditLogRepository repository = new SuncereAuditLogRepository(db);
            IQueryable<SuncereAuditLog> query = repository.Query(startTime, endTime, keyword);
            List<SuncereAuditLog> list = query.Skip(start).Take(length).ToList();
            return Json(new { draw = draw, recordsTotal = query.Count(), recordsFiltered = query.Count(), data = list.Select(o => new string[] { o.UserName, o.Url, o.Referrer, o.HostName, o.HostAddress, o.CreationTime.ToString("yyyy-MM-dd HH:mm") }) }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        [HttpPost]
        [SuncereAuthorize(Controller = "System", Action = "AuditLogList")]
        public ActionResult AuditLogDelete(string ids)
        {
            AjaxResult result;
            try
            {
                int[] idArray = ids.Split(',').Select(o => int.Parse(o)).ToArray();
                FluentModel db = Session.GetFluentModel();
                SuncereAuditLogRepository repository = new SuncereAuditLogRepository(db);
                IQueryable<SuncereAuditLog> query = repository.Query(idArray);
                db.Delete(query);
                db.SaveChanges();
                result = AjaxResult.GetDeleteAjaxResult(true);
            }
            catch (Exception e)
            {
                result = AjaxResult.GetDeleteAjaxResult(false);
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }
        #endregion
    }
}