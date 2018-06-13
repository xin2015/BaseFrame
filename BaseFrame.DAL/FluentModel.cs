using BaseFrame.Core.CryptoTransverters;
using BaseFrame.Core.Helpers;
using BaseFrame.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;

namespace BaseFrame.DAL
{
    public partial class FluentModel : OpenAccessContext, IFluentModelUnitOfWork
    {
        private static string connectionStringName = @"BaseFrameConnection";

        private static BackendConfiguration backend = GetBackendConfiguration();

        private static MetadataSource metadataSource = new FluentModelMetadataSource();

        public FluentModel()
            : base(connectionStringName, backend, metadataSource)
        { }

        public FluentModel(string connection)
            : base(connection, backend, metadataSource)
        { }

        public FluentModel(BackendConfiguration backendConfiguration)
            : base(connectionStringName, backendConfiguration, metadataSource)
        { }

        public FluentModel(string connection, MetadataSource metadataSource)
            : base(connection, backend, metadataSource)
        { }

        public FluentModel(string connection, BackendConfiguration backendConfiguration, MetadataSource metadataSource)
            : base(connection, backendConfiguration, metadataSource)
        { }

        public IQueryable<SuncereUser> SuncereUsers
        {
            get
            {
                return this.GetAll<SuncereUser>();
            }
        }

        public IQueryable<SuncereRole> SuncereRoles
        {
            get
            {
                return this.GetAll<SuncereRole>();
            }
        }

        public IQueryable<SuncerePermission> SuncerePermissions
        {
            get
            {
                return this.GetAll<SuncerePermission>();
            }
        }

        public IQueryable<SuncereAuditLog> SuncereAuditLogs
        {
            get
            {
                return this.GetAll<SuncereAuditLog>();
            }
        }

        public static BackendConfiguration GetBackendConfiguration()
        {
            BackendConfiguration backend = new BackendConfiguration();
            backend.Backend = "MsSql";
            backend.ProviderName = "System.Data.SqlClient";
            //backend.Backend = "Oracle";
            //backend.ProviderName = "Oracle.ManagedDataAccess.Client";

            CustomizeBackendConfiguration(ref backend);

            return backend;
        }

        /// <summary>
        /// Allows you to customize the BackendConfiguration of FluentModel.
        /// </summary>
        /// <param name="config">The BackendConfiguration of FluentModel.</param>
        static partial void CustomizeBackendConfiguration(ref BackendConfiguration config);

        public void UpdateSchema()
        {
            var handler = GetSchemaHandler();
            string script = null;
            try
            {
                script = handler.CreateUpdateDDLScript(null);
            }
            catch
            {
                bool throwException = false;
                try
                {
                    handler.CreateDatabase();
                    script = handler.CreateDDLScript();
                }
                catch
                {
                    throwException = true;
                }
                if (throwException)
                    throw;
            }

            if (string.IsNullOrEmpty(script) == false)
            {
                handler.ExecuteDDLScript(script);
            }
        }

        public void InitData()
        {
            SuncereUser user = new SuncereUser()
            {
                UserName = "admin",
                Password = AsymmetricEncryption.Default.Encrypt("123456"),
                Status = true,
                IsStatic = true
            };
            SuncereRole role = new SuncereRole()
            {
                Name = LanguageHelper.L("admin"),
                Status = true,
                IsStatic = true
            };
            SuncerePermission system = new SuncerePermission()
            {
                ParentId = 0,
                Name = LanguageHelper.L("SystemManage"),
                Type = 0,
                Controller = "System",
                Action = "",
                Order = 99,
                Icon = "&#xe62e;",
                Status = true,
                IsStatic = true
            };
            SuncerePermission roleList = new SuncerePermission()
            {
                ParentId = 1,
                Name = LanguageHelper.L("RoleList"),
                Type = 1,
                Controller = "System",
                Action = "RoleList",
                Order = 0,
                Icon = "",
                Status = true,
                IsStatic = true
            };
            SuncerePermission userList = new SuncerePermission()
            {
                ParentId = 1,
                Name = LanguageHelper.L("UserList"),
                Type = 1,
                Controller = "System",
                Action = "UserList",
                Order = 1,
                Icon = "",
                Status = true,
                IsStatic = true
            };
            SuncerePermission permissionList = new SuncerePermission()
            {
                ParentId = 1,
                Name = LanguageHelper.L("PermissionList"),
                Type = 1,
                Controller = "System",
                Action = "PermissionList",
                Order = 2,
                Icon = "",
                Status = true,
                IsStatic = true
            };
            SuncerePermission auditLogList = new SuncerePermission()
            {
                ParentId = 1,
                Name = LanguageHelper.L("AuditLogList"),
                Type = 1,
                Controller = "System",
                Action = "AuditLogList",
                Order = 3,
                Icon = "",
                Status = true,
                IsStatic = true
            };
            Add(user);
            Add(role);
            Add(system);
            Add(roleList);
            Add(userList);
            Add(permissionList);
            Add(auditLogList);

            user.SuncereRoles.Add(role);
            role.SuncereUsers.Add(user);
            role.SuncerePermissions.Add(system);
            system.SuncereRoles.Add(role);
            role.SuncerePermissions.Add(roleList);
            roleList.SuncereRoles.Add(role);
            role.SuncerePermissions.Add(userList);
            userList.SuncereRoles.Add(role);
            role.SuncerePermissions.Add(permissionList);
            permissionList.SuncereRoles.Add(role);
            role.SuncerePermissions.Add(auditLogList);
            auditLogList.SuncereRoles.Add(role);
            SaveChanges();
        }
    }

    public interface IFluentModelUnitOfWork : IUnitOfWork
    {
        IQueryable<SuncereUser> SuncereUsers
        {
            get;
        }
        IQueryable<SuncereRole> SuncereRoles
        {
            get;
        }
        IQueryable<SuncerePermission> SuncerePermissions
        {
            get;
        }
        IQueryable<SuncereAuditLog> SuncereAuditLogs
        {
            get;
        }
    }
}
