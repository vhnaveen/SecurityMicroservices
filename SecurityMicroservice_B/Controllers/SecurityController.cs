namespace SecurityMicroservice_B.Controllers
{/// <summary>
 /// SecurityController
 /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SecurityController : ControllerBase
    {
        /// <summary>
        /// GetJWTToken
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "security")]
        public JWTToken GetJWTToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("hwNiIVgIwaX6itoB6KhpnZUvbS5Bp5DX"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim("Scopes", "create-curriculum"),
                new Claim("Scopes", "delete-curriculum")
            };

            var token = new JwtSecurityToken("https://tcp-dev.intel.com",
                "https://tcp-dev.intel.com",
                claims,
                expires: DateTime.MaxValue,
                signingCredentials: credentials);

            return new JWTToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };

        }


    }

    /// <summary>
    /// JWTToken
    /// </summary>
    public class JWTToken
    {
        /// <summary>
        /// Token
        /// </summary>
        public string? Token { get; set; }
    }
}
