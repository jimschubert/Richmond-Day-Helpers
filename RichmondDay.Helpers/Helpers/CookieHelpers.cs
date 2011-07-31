using System;
using System.Web;

namespace RichmondDay.Helpers {
    public static class CookieHelpers {
        public static void WriteAuthenticationCookie(int userId, bool rememberMe) {
            HttpCookie myCookie = new HttpCookie(CookieIdentifiers.Login_Cookie);
            HttpContext.Current.Response.Cookies.Remove(CookieIdentifiers.Login_Cookie);
            HttpContext.Current.Response.Cookies.Add(myCookie);
            myCookie.Values.Add(CookieIdentifiers.Login_Cookie_Key, userId.ToString());
            if (rememberMe) {
                HttpContext.Current.Response.Cookies[CookieIdentifiers.Login_Cookie].Expires = DateTime.Now.AddDays(15);
            }
        }

        public static void DestroyAuthenticationCookie() {
            HttpCookie myCookie = new HttpCookie(CookieIdentifiers.Login_Cookie);
            HttpContext.Current.Response.Cookies.Remove(CookieIdentifiers.Login_Cookie);
            HttpContext.Current.Response.Cookies.Add(myCookie);
            HttpContext.Current.Response.Cookies[CookieIdentifiers.Login_Cookie].Expires = DateTime.Now.AddDays(-15);
        }

        public static void WriteRegionCookie(int regionId) {
            HttpCookie myCookie = new HttpCookie(CookieIdentifiers.Region_Cookie);
            HttpContext.Current.Response.Cookies.Remove(CookieIdentifiers.Region_Cookie);
            HttpContext.Current.Response.Cookies.Add(myCookie);
            myCookie.Values.Add(CookieIdentifiers.Region_Cookie_Key, regionId.ToString());
            HttpContext.Current.Response.Cookies[CookieIdentifiers.Region_Cookie].Expires = DateTime.Now.AddDays(15);
        }

        public static void WriteCartCookie(int cartId) {
            HttpCookie myCookie = new HttpCookie(CookieIdentifiers.Cart_Cookie);
            HttpContext.Current.Response.Cookies.Remove(CookieIdentifiers.Cart_Cookie);
            HttpContext.Current.Response.Cookies.Add(myCookie);
            myCookie.Values.Add(CookieIdentifiers.Cart_Cookie_Key, cartId.ToString());
            HttpContext.Current.Response.Cookies[CookieIdentifiers.Cart_Cookie].Expires = DateTime.Now.AddDays(30);
        }

        public static void WriteCookie(string cookieIdentifier, string cookieKeyIdentifier, string cookieKeyValue) {
            HttpCookie myCookie = new HttpCookie(cookieIdentifier);
            HttpContext.Current.Response.Cookies.Remove(cookieIdentifier);
            HttpContext.Current.Response.Cookies.Add(myCookie);
            myCookie.Values.Add(cookieKeyIdentifier, cookieKeyValue.ToString());
        }

        public static void WriteCookie(string cookieIdentifier, string cookieKeyIdentifier, string cookieKeyValue, DateTime expiry) {
            HttpCookie myCookie = new HttpCookie(cookieIdentifier);
            HttpContext.Current.Response.Cookies.Remove(cookieIdentifier);
            HttpContext.Current.Response.Cookies.Add(myCookie);
            myCookie.Values.Add(cookieKeyIdentifier, cookieKeyValue.ToString());
            if(expiry != null)
                HttpContext.Current.Response.Cookies[cookieIdentifier].Expires = expiry;
        }

        public static void DestroyCookie(string cookieIdentifier) {
            HttpCookie myCookie = new HttpCookie(cookieIdentifier);
            HttpContext.Current.Response.Cookies.Remove(cookieIdentifier);
            HttpContext.Current.Response.Cookies.Add(myCookie);
            HttpContext.Current.Response.Cookies[cookieIdentifier].Expires = DateTime.Now.AddDays(-15);
        }

        public static int GetCookieValue(string cookieIdentifier, string cookieKeyIdentifier) {
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(cookieIdentifier);

            if (cookie == null)
                return 0;

            int cookieValue = 0;
            int.TryParse(cookie.Values[cookieKeyIdentifier].ToString(), out cookieValue);

            return cookieValue;
        }

        public static int GetUserId() {
            Int32 userId = 0;
            if (HttpContext.Current.Request.Cookies.Get(CookieIdentifiers.Login_Cookie) != null) {
                HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(CookieIdentifiers.Login_Cookie);
                if(cookie.Values.Count > 0)
                    Int32.TryParse(cookie.Values[CookieIdentifiers.Login_Cookie_Key].ToString(), out userId);
            }

            return userId;
        }

        public static int GetCartID() {
            Int32 cartId = 0;
            if (HttpContext.Current.Request.Cookies.Get(CookieIdentifiers.Cart_Cookie) != null) {
                HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(CookieIdentifiers.Cart_Cookie);
                if (cookie.Values[CookieIdentifiers.Cart_Cookie_Key] != null)
                    Int32.TryParse(cookie.Values[CookieIdentifiers.Cart_Cookie_Key].ToString(), out cartId);
            }

            return cartId;
        }
    }

    public static class CookieIdentifiers {
        public static string Login_Cookie = "LoginCookie";
        public static string Login_Cookie_Key = "LoginCookieUserID";

        public static string Region_Cookie = "RegionCookie";
        public static string Region_Cookie_Key = "RegionCookieKey";

        public static string Cart_Cookie = "CartCookie";
        public static string Cart_Cookie_Key = "CartCookieKey";
    }
}