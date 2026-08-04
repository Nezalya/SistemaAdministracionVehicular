using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SAV.Infrastructure.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SAV.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwt;

        public TokenService(IOptions<JwtSettings> jwt)
        {
            _jwt = jwt.Value;
        }

        public TokenGenerado GenerarToken(Usuario usuario)
        {
            var claims = new List<Claim>
    {
        new(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
        new(ClaimTypes.Name, usuario.Username),
        new(ClaimTypes.Email, usuario.Email),
        new(ClaimTypes.Role, usuario.Rol.ToString())
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expira = DateTime.UtcNow.AddMinutes(_jwt.ExpirationMinutes);

            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer, audience: _jwt.Audience,
                claims: claims, expires: expira, signingCredentials: credenciales);

            return new TokenGenerado
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expira = expira
            };
        }
    }
}