using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Microsoft.Extensions.Options;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Boc.Assets.Web.Auth.Authentication
{
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly IOrganizationService _organizationService;

        public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions, IOrganizationService organizationService)
        {
            _jwtOptions = jwtOptions.Value;
            _organizationService = organizationService;
        }

        public async Task<string> CreateTokenAsync(string orgIdentifier)
        {
            OrgDto orgDto = await _organizationService.GetByOrgIdentifierAsync(orgIdentifier);
            var claims = new Claim[]
            {
                new Claim("orgId", orgDto.OrgId.ToString()),
                new Claim("jti", await _jwtOptions.JtiGenerator()),
                new Claim("iat",
                    new DateTimeOffset(_jwtOptions.IssuedAt).ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64),
                new Claim("orgRole",orgDto.Role.ToString()),
                new Claim("roleId",orgDto.RoleId.ToString()),
                new Claim("orgName",orgDto.OrgNam),
                new Claim("orgIdentifier",orgDto.OrgIdentifier),
                new Claim("org2",orgDto.Org2),
                new Claim("managementLineId",orgDto.ManagementLineId.ToString()),
            };
            var securityToken = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }


    }
}