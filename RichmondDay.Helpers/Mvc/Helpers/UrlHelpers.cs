using System.Web.Routing;
using System.Text.RegularExpressions;

namespace System.Web.Mvc {
    public static class UrlHelpers {
        /// <summary>
        /// Redirects to action.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        /// <param name="responseCode">The response code.</param>
        /// <param name="actionName">Name of the action.</param>
        public static void RedirectToAction(this ControllerContext filterContext, string actionName) {
            if (String.IsNullOrEmpty(actionName))
                throw new ArgumentNullException("actionName");

            RouteValueDictionary values = new RouteValueDictionary();
            values.Add("action", actionName);

            RedirectToAction(filterContext, values);
        }

        /// <summary>
        /// Redirects to action.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        /// <param name="responseCode">The response code.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="controllerName">Name of the controller.</param>
        public static void RedirectToAction(this ControllerContext filterContext, string actionName, string controllerName) {
            if (String.IsNullOrEmpty(actionName))
                throw new ArgumentNullException("actionName");

            if (String.IsNullOrEmpty(controllerName))
                throw new ArgumentNullException("controllerName");

            RouteValueDictionary values = new RouteValueDictionary();
            values.Add("action", actionName);
            values.Add("controller", controllerName);

            RedirectToAction(filterContext, values);
        }

        /// <summary>
        /// Redirects to action.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        /// <param name="responseCode">The response code.</param>
        /// <param name="values">The values.</param>
        public static void RedirectToAction(this ControllerContext filterContext, RouteValueDictionary values) {
            VirtualPathData virtualPath = RouteTable.Routes.GetVirtualPath(filterContext.RequestContext, values);

            string url = null;
            if (virtualPath != null)
                url = virtualPath.VirtualPath;

            filterContext.HttpContext.Response.Redirect(url);
        }

        public static string CreateUrlSlug(this string value) {
            if (String.IsNullOrEmpty(value)) return "";

            value = Regex.Replace(value, @"&\w+;", "");             // remove entities
            value = Regex.Replace(value, @"[^A-Za-z0-9\-\s]", "");  // remove anything that is not letters, numbers, dash, or space
            value = value.Trim();                                   // remove any leading or trailing spaces left over
            value = Regex.Replace(value, @"\s+", "-");              // replace spaces with single dash
            value = Regex.Replace(value, @"\-{2,}", "-");           // if we end up with multiple dashes, collapse to single dash
            value = value.ToLower();                                // make it all lower case

            if (value.Length > 80)                                  // if it's too long, clip it
                value = value.Substring(0, 79);

            if (value.EndsWith("-"))                                // remove trailing dash, if there is one
                value = value.Substring(0, value.Length - 1);

            return value;
        }
    }
}