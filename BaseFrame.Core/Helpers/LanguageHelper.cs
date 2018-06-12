using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFrame.Core.Helpers
{
    public class LanguageHelper
    {
        static Dictionary<string, string> _dic;

        static LanguageHelper()
        {
            _dic = new Dictionary<string, string>();
            _dic.Add("", "");
            _dic.Add("Module", "模块");
            _dic.Add("Page", "页面");
            _dic.Add("Success", "成功");
            _dic.Add("Fail", "失败");
            _dic.Add("Add", "添加");
            _dic.Add("Delete", "删除");
            _dic.Add("Edit", "编辑");
            _dic.Add("Query", "查询");
            _dic.Add("Search", "查询");
            _dic.Add("Login", "登录");
            _dic.Add("Logout", "退出");
            _dic.Add("SystemManage", "系统管理");
            _dic.Add("RoleList", "角色管理");
            _dic.Add("UserList", "用户管理");
            _dic.Add("PermissionList", "权限管理");
            _dic.Add("AuditLogList", "日志管理");
            _dic.Add("UserName", "用户名");
            _dic.Add("Password", "密码");
            _dic.Add("ConfirmPassword", "确认密码");
            _dic.Add("Url", "网址");
            _dic.Add("Referrer", "访问来源");
            _dic.Add("HostName", "主机名");
            _dic.Add("HostAddress", "主机地址");
            _dic.Add("CreationTime", "创建时间");
            _dic.Add("Operation", "操作");
            _dic.Add("Name", "名称");
            _dic.Add("Type", "类型");
            _dic.Add("Controller", "控制器");
            _dic.Add("Action", "方法");
            _dic.Add("Order", "序号");
            _dic.Add("Remark", "备注");
            _dic.Add("Status", "状态");
            _dic.Add("Enable", "启用");
            _dic.Add("Disable", "停用");
            _dic.Add("EmailAddress", "邮箱");
            _dic.Add("PhoneNumber", "手机");
            _dic.Add("Role", "角色");
            _dic.Add("OldPassword", "旧密码");
        }

        public static string L(string name)
        {
            return _dic.ContainsKey(name) ? _dic[name] : name;
        }
    }
}
