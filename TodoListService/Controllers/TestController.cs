using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace TodoListService.Controllers
{
    /// <summary>
    /// Controller for PoC functionality
    /// </summary>
    [Authorize]
    public class TestController : ClaimsControllerBase
    {
        // GET api/test
        /// <summary>
        /// Checks if the user is logged in and returns the username.
        /// </summary>
        /// <returns></returns>
        public string Get()
        {
            CheckAccessTokenScope("access_as_user");

            Claim subject = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier);
            return subject.Subject.Claims.FirstOrDefault(claim => claim.Type == "preferred_username")?.Value;
        }
    }
}
