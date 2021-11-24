using Microsoft.AspNetCore.Authorization;

namespace Shareds.Authentication.Attributes
{
    public class RolesAttribute : AuthorizeAttribute
    {
        public RolesAttribute(params string[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
    }

  
}
