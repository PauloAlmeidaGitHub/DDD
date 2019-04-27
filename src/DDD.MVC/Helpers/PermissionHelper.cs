using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace DDD.MVC.Helpers
{
    public static class PermissionHelper
    {
        //Extension Method para trabalhar com qualquer item da View - Permite limitar a visibilidade do usuário (Botão , link, etc.)
        public static MvcHtmlString IfClaimHelper(this MvcHtmlString value, string claimName, string claimValue)
        {
            return ValidadePermission(claimName, claimValue) ? value : MvcHtmlString.Empty;
        }

        //Extension Method para trabalhar com a própria View - Restringe acesso 
        public static bool IfClaim(this WebViewPage page, string claimName, string claimValue)
        {
            return ValidadePermission(claimName, claimValue);
        }

        private static bool ValidadePermission(string claimName, string claimValue)
        {
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var claim = identity.Claims.FirstOrDefault(c => c.Type == claimName);
            return claim != null && claim.Value.Contains(claimValue);
        }
    }
}