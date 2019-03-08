using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace TodoListService.Controllers
{
    public abstract class ClaimsControllerBase : ApiController
    {
        private ClaimsIdentity userClaims;

        protected ClaimsControllerBase()
        {
            userClaims = User.Identity as ClaimsIdentity;
        }

        /// <summary>
        /// Assure the presence of a scope claim containing a specific scope (i.e. access_as_user)
        /// </summary>
        /// <param name="scopeName">The name of the scope</param>
        protected void CheckAccessTokenScope(string scopeName)
        {
            // Make sure access_as_user scope is present
            string scopeClaimValue = userClaims.FindFirst("http://schemas.microsoft.com/identity/claims/scope")?.Value;
            if (!string.Equals(scopeClaimValue, scopeName, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    ReasonPhrase = $@"Please request an access token to scope '{scopeName}'"
                });
            }
        }
    }
}