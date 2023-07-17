using Microsoft.IdentityModel.Tokens;
using POC_Shared.Certificates;
using POC_Shared.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace POC_Shared.Helpers
{
    public class JwtAuth
    {
        private readonly SigningAudienceCertificate _signingAudienceCertificate;

        public JwtAuth()
        {
            _signingAudienceCertificate = new SigningAudienceCertificate();
        }

        public string GenerateTokenAsymmetric()
        {
            var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, "exemplo@email.com"),

                    new Claim(JwtRegisteredClaimNames.Jti, "1"),

                    new Claim(JwtRegisteredClaimNames.UniqueName, "Exemplo")
                };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = _signingAudienceCertificate.GetAudienceSigningKey()
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }
        public string GenerateTokenSymmetric(JwtSecrets jwt)
        {
            var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, "exemplo@email.com"),

                    new Claim(JwtRegisteredClaimNames.Jti, "1"),

                    new Claim(JwtRegisteredClaimNames.UniqueName, "Exemplo")

                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwt.ValidIssuer,
                audience: jwt.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
