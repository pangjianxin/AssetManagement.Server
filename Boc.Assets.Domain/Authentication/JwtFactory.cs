using Boc.Assets.Domain.Models.Organizations;
using Microsoft.Extensions.Options;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.Authentication
{
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtIssuerOptions _jwtOptions;

        public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<string> CreateTokenAsync(Organization org)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat,
                    new DateTimeOffset(_jwtOptions.IssuedAt).ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64),
                new Claim("orgId", org.Id.ToString()),
                new Claim("orgRole",$"{(int)org.Role.Role}",ClaimValueTypes.Integer),
                new Claim("roleId",org.RoleId.ToString()),
                new Claim("orgName",org.OrgNam),
                new Claim("orgIdentifier",org.OrgIdentifier),
                new Claim("org2",org.Org2),
                new Claim("shortName",org.OrgShortNam),
            };
            var securityToken = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,//audience表示接收方，是要接收方来验证是否有效的一个字段
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }


    }
}