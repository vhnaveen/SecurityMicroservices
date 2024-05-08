using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace SecurityMicroservice_B.Middlewares
{
    public class TcpSecMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public TcpSecMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
                var path = context.Request.Path.Value;

            // Exclude the security endpoint from the token check
            if (path.ToUpper().EndsWith("/SECURITY") || path.ToUpper().Contains("SWAGGER"))
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue("tcp-sec", out var extractedToken))
            {
                context.Response.StatusCode = 400; // Bad Request
                await context.Response.WriteAsync("tcp-sec token is missing");
                return;
            }

            var splitPath = path.TrimStart('/').Split('/');
            var endpointName = splitPath[splitPath.Length-2].ToLower() + "-" + splitPath[splitPath.Length-1].ToLower();
            var requiredScopes = _configuration.GetSection($"Endpoints:{endpointName}:Scopes").Get<List<string>>();
            var requireAllScopes = _configuration.GetValue<bool>($"Endpoints:{endpointName}:RequireAllScopes");

            if (requireAllScopes)
            {
                foreach (var scope in requiredScopes)
                {
                    if (!ValidateTokenScope(extractedToken, scope))
                    {
                        context.Response.StatusCode = 403; // Forbidden
                        await context.Response.WriteAsync($"Missing required scope: {scope}");
                        return;
                    }
                }
            }
            else
            {
                if (requiredScopes != null && !requiredScopes.Any(scope => ValidateTokenScope(extractedToken, scope)))
                {
                    context.Response.StatusCode = 403; // Forbidden
                    await context.Response.WriteAsync("Missing required scope");
                    return;
                }
            }

            await _next(context);
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
