using BlogApp.API.Features.Auth.Login;
using BlogApp.API.Service;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace BlogApp.API.Features.Auth
{
    public static class AuthEndpoint
    {
        public static void AddAuthGroupEnpoint(this WebApplication app)
        {
            var group = app.MapGroup("/auth").WithTags("Auth");

            group.MapPost("/login", async([FromServices] IMediator mediator, LoginCommand cmd) =>
            {
                try
                {
                    var result = await mediator.Send(cmd);
                    return Results.Ok(result);

                }
                catch (Exception ex)
                {
                    // Log the exception (not implemented here)
                    return Results.BadRequest(new { error = ex.Message });
                }
            }).AllowAnonymous();

            group.MapPost("/logout", [Authorize] async (HttpRequest request, ITokenRevocationService revocationService) =>
            {
                var authHeader = request.Headers["Authorization"].ToString();
                if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer ")) return Results.BadRequest("No token provided");

                var token = authHeader.Substring("Bearer ".Length).Trim();
                var handler = new JwtSecurityTokenHandler();
                try
                {
                    var jwt = handler.ReadJwtToken(token);
                    var expiry = jwt.ValidTo;

                    revocationService.RevokeToken(token, expiry);

                    return Results.Ok(new { message = "Logged out" });
                }
                catch
                {
                    return Results.BadRequest("Invalid token");
                }
            });
        }
    }
}
