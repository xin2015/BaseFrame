using BaseFrame.DAL;
using BaseFrame.DAL.Entities;
using System.Collections.Generic;
using System.Web;

namespace BaseFrame.Web.Extensions
{
    public static class HttpSessionStateBaseExtension
    {
        public const string FluentModel = "FluentModel";
        public const string CurrentUser = "CurrentUser";
        public const string UserPermissions = "UserPermissions";

        public static FluentModel GetFluentModel(this HttpSessionStateBase session)
        {
            FluentModel db = session[FluentModel] as FluentModel;
            if (db == null)
            {
                db = new FluentModel();
                session[FluentModel] = db;
            }
            return db;
        }

        public static void CloseFluentModel(this HttpSessionStateBase session)
        {
            FluentModel db = session[FluentModel] as FluentModel;
            if (db != null)
            {
                db.Dispose();
                session[FluentModel] = null;
            }
        }

        public static void SetCurrentUser(this HttpSessionStateBase session, SuncereUser user)
        {
            session[CurrentUser] = user;
        }

        public static SuncereUser GetCurrentUser(this HttpSessionStateBase session)
        {
            return session[CurrentUser] as SuncereUser;
        }

        public static void SetUserPermissions(this HttpSessionStateBase session, List<SuncerePermission> userPermissions)
        {
            session[UserPermissions] = userPermissions;
        }

        public static List<SuncerePermission> GetUserPermissions(this HttpSessionStateBase session)
        {
            return session[UserPermissions] as List<SuncerePermission>;
        }
    }
}