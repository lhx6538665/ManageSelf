using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class UserData : BaseData<User>
    {
        public ClaimsIdentity CreateIdentity(User user, string authenticationType)
        {
            ClaimsIdentity _identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
            _identity.AddClaim(new Claim(ClaimTypes.Name, user.Username));
            _identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            _identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity"));
            _identity.AddClaim(new Claim("DisplayName", user.DisplayName));
            foreach (var role in user.Role)
            {
                _identity.AddClaim(new Claim(ClaimTypes.Role, role.Name));
            }
            return _identity;
        }
    }
}
