﻿using BaseFrame.DAL;
using BaseFrame.DAL.Entities;
using BaseFrame.Web.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaseFrame.Web.Attributes
{
    [SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Justification = "Unsealed so that subclassed types can set properties in the default constructor or override our behavior.")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class SuncereAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public bool IsDefault { get; set; }

        // This method must be thread-safe since it is called by the thread-safe OnCacheAuthorization() method.
        protected virtual bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool result;
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }
            HttpRequestBase request = httpContext.Request;
            if (IsDefault)
            {
                string[] texts = request.Path.Split('/');
                Controller = texts[0];
                Action = texts[1];
            }
            FluentModel db = httpContext.Session.GetFluentModel();
            db.Add(new SuncereAuditLog()
            {
                UserName = httpContext.Session.GetCurrentUser().UserName,
                Url = request.Url.PathAndQuery,
                Referrer = request.UrlReferrer == null ? string.Empty : request.UrlReferrer.PathAndQuery,
                HostName = request.UserHostName,
                HostAddress = request.UserHostAddress
            });
            db.SaveChanges();
            if (string.IsNullOrEmpty(Controller) || string.IsNullOrEmpty(Action))
            {
                result = true;
            }
            else
            {
                List<SuncerePermission> userPermissions = httpContext.Session.GetUserPermissions();
                if (userPermissions == null)
                {
                    result = false;
                }
                else
                {
                    result = userPermissions.Any(o => o.Controller == Controller && o.Action == Action);
                }
            }
            return result;
        }

        private void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            validationStatus = OnCacheAuthorization(new HttpContextWrapper(context));
        }

        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (OutputCacheAttribute.IsChildActionCacheActive(filterContext))
            {
                // If a child action cache block is active, we need to fail immediately, even if authorization
                // would have succeeded. The reason is that there's no way to hook a callback to rerun
                // authorization before the fragment is served from the cache, so we can't guarantee that this
                // filter will be re-run on subsequent requests.
                throw new InvalidOperationException("AuthorizeAttribute CannotUseWithinChildActionCache");
            }

            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true)
                                     || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true);

            if (skipAuthorization)
            {
                return;
            }
            if (filterContext.HttpContext.Session.GetCurrentUser() == null)
            {
                HandleUnauthorizedRequest(filterContext, false);
            }
            else if (AuthorizeCore(filterContext.HttpContext))
            {
                // ** IMPORTANT **
                // Since we're performing authorization at the action level, the authorization code runs
                // after the output caching module. In the worst case this could allow an authorized user
                // to cause the page to be cached, then an unauthorized user would later be served the
                // cached page. We work around this by telling proxies not to cache the sensitive page,
                // then we hook our custom authorization code into the caching mechanism so that we have
                // the final say on whether a page should be served from the cache.

                HttpCachePolicyBase cachePolicy = filterContext.HttpContext.Response.Cache;
                cachePolicy.SetProxyMaxAge(new TimeSpan(0));
                cachePolicy.AddValidationCallback(CacheValidateHandler, null /* data */);
            }
            else
            {
                HandleUnauthorizedRequest(filterContext, true);
            }
        }

        protected virtual void HandleUnauthorizedRequest(AuthorizationContext filterContext, bool isLogined)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                ReflectedActionDescriptor descriptor = filterContext.ActionDescriptor as ReflectedActionDescriptor;
                if (descriptor == null || descriptor.MethodInfo.ReturnType.Name == "ActionResult")
                {
                    if (isLogined)
                    {
                        filterContext.Result = new RedirectResult("/Home/Page404");
                    }
                    else
                    {
                        filterContext.Result = new RedirectResult(string.Format("/Home/Login?returnUrl={0}", filterContext.HttpContext.Request.Url.PathAndQuery));
                    }
                    return;
                }
            }
            filterContext.Result = new HttpUnauthorizedResult();
        }

        // This method must be thread-safe since it is called by the caching module.
        protected virtual HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            return (httpContext.Session.GetCurrentUser() != null && AuthorizeCore(httpContext)) ? HttpValidationStatus.Valid : HttpValidationStatus.IgnoreThisRequest;
        }
    }
}
