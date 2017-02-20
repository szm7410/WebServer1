using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace TestV3.Service
{
    public class Util
    {
        //static string dgDomain = "https://msdg-edog.live.com";//edog
        static string dgDomain = "https://msdg.live.com";//prod
        //static string dgDomain = "https://localhost:44300";//local
        //static string dgDomain = "https://dgonebox2.live.com";
        public static void RedirectUserToSignIn(string dgtoken, string redirectUrl)
        {
            if (HttpContext.Current == null)
            {
                throw new Exception("无法在非http请求中使用");
            }
            HttpResponse Response = HttpContext.Current.Response;
            string postUrl = string.Format("{0}/signin.aspx?redirect_url={1}", dgDomain, redirectUrl);
            StringBuilder sb = new StringBuilder(string.Format("<html><head></head><body onload=\"document.form1.submit()\"><form name=\"form1\" method=\"post\" action=\"{0}\" >", postUrl));
            sb.Append(string.Format("<input name=\"DGToken\" type=\"hidden\" value=\"{0}\"></form></body></html>", dgtoken));
            Response.Write(sb.ToString());
            Response.End();
        }

    }
}