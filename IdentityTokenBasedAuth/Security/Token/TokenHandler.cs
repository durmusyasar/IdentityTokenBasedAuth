﻿using IdentityTokenBasedAuth.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace IdentityTokenBasedAuth.Security.Token
{
    public class TokenHandler : ITokenHandler
    {
        private readonly CustomTokenOptions tokenOptions;
        public TokenHandler(IOptions<CustomTokenOptions> tokenOptions)
        {
            this.tokenOptions = tokenOptions.Value;
        }

        public AccessToken CreateAccessToken(AppUser user)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(tokenOptions.AccessTokenExpiration);
            var securityKey = SignHandler.GetSecurityKey(tokenOptions.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha384Signature);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: GetClaim(user),
                signingCredentials: signingCredentials
            );

            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);

            AccessToken accessToken = new AccessToken();

            accessToken.Token = token;
            accessToken.RefreshToken = CreateRRefreshToken();
            accessToken.Expration = accessTokenExpiration;

            return accessToken;
        }

        public void RevokeRefreshToken(AppUser user)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<Claim> GetClaim(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, $"{user.UserName}"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            return claims;
        }

        private string CreateRRefreshToken()
        {
            var numberByte = new Byte[32];
            using (var mg = RandomNumberGenerator.Create())
            {
                mg.GetBytes(numberByte);
                return Convert.ToBase64String(numberByte);
            }
        }
    }
}
