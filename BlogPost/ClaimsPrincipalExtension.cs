using BlogPost.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogPost
{
    public static class ClaimsPrincipalExtension
    {
        public static string GetFirstName(this ClaimsPrincipal principal)
        {
            var firstName = principal.Claims.FirstOrDefault(c => c.Type == "FirstName");
            return firstName?.Value;
        }

        public static string GetLastName(this ClaimsPrincipal principal)
        {
            var lastName = principal.Claims.FirstOrDefault(c => c.Type == "LastName");
            return lastName?.Value;
        }

        public static string GetFullName(this ClaimsPrincipal principal)
        {
            var fullname = principal.Claims.FirstOrDefault(c => c.Type == "FullName");
            return fullname?.Value;
        }
        public static string GetProfileImage(this ClaimsPrincipal principal)
        {
            var fullname = principal.Claims.FirstOrDefault(c => c.Type == "ProfileImageUrl");
            return fullname?.Value;
        }
    }

    public class MyUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser>
    {
        public MyUserClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            IOptions<IdentityOptions> optionsAccessor)
                : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("FullName", user.FirstName + " " + user.LastName ?? ""));
            identity.AddClaim(new Claim("ProfileImageUrl", user.UserImage ?? ""));
            return identity;
        }
    }
}
