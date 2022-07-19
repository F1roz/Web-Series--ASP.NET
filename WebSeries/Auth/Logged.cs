using System.Web;
using System.Web.Mvc;


namespace WebSeriesApplication.Auth
{
    public class Logged : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
             if (httpContext.Session["user_name"] != null)
                {
                    return true;
                }
                return false;
            
            return true;
    } }
}