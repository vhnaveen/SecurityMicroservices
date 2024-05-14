using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.PortableExecutable;

namespace SecurityMicroservice_A.Utilities
{
    /// <summary>
    /// CustomAuthorizationHandler
    /// </summary>
    public class CustomAuthorizationHandler : AuthorizationHandler<CustomAuthorizationRequirement>
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// CustomAuthorizationHandler
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public CustomAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// HandleRequirementAsync
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomAuthorizationRequirement requirement)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null || !httpContext.Request.Headers.TryGetValue("tcp-sec", out var extractedToken))
            {
                return Task.CompletedTask;
            }

            foreach (var scope in requirement.Scopes)
            {
                if (!ValidateTokenScope(extractedToken, scope))
                {
                    return Task.CompletedTask;
                }
            }

            context.Succeed(requirement);

            return Task.CompletedTask;
        }

        private bool ValidateTokenScope(string token, string requiredScope)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // Assuming the scope is a claim in the token
            var scopeClaim = jwtToken.Claims.FirstOrDefault(c => c.Value == requiredScope);
            if (scopeClaim == null)
            {
                return false;
            }

            // If the scope claim is a single string
            if (scopeClaim.Value == requiredScope)
            {
                return true;
            }

            // If the scope claim is a space-separated string of scopes
            var scopes = scopeClaim.Value.Split(' ');
            return scopes.Contains(requiredScope);
        }
    }
}
