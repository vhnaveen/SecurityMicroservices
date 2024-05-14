using Microsoft.AspNetCore.Authorization;

namespace SecurityMicroservice_A.Utilities
{
    public class CustomAuthorizationRequirement : IAuthorizationRequirement
    {
        public CustomAuthorizationRequirement(string[] scopes)
        {
            Scopes = scopes;
        }

        public string[] Scopes { get; }
    }
}
