using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

public class JwtVerifier
{
    private readonly TokenValidationParameters _validationParams;
    private readonly JwtSecurityTokenHandler _handler = new();

    public JwtVerifier(string signingKey, string issuer, string audience)
    {
        _validationParams = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),

            ValidateIssuer = true,
            ValidIssuer = issuer,

            ValidateAudience = true,
            ValidAudience = audience,

            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromSeconds(10), // allowed clock drift
        };
    }

    public ClaimsPrincipal ValidateToken(string token, out SecurityToken validatedToken)
    {
        try
        {
            return _handler.ValidateToken(token, _validationParams, out validatedToken);
        }
        catch (Exception ex)
        {
            throw new Exception($"JWT validation failed: {ex.Message}");
        }
    }

    public bool TryValidateToken(string token, out ClaimsPrincipal principal)
    {
        principal = null;

        try
        {
            principal = _handler.ValidateToken(token, _validationParams, out _);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
