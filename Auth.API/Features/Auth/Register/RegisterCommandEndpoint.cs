using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Features.Auth.Register
{
    public static class RegisterCommandEndpoint
    {
        public static RouteGroupBuilder RegisterCommandEndpointItem(this RouteGroupBuilder group)
        {
            group.MapPost("/register", async (RegisterCommand command, [FromServices] IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            }).WithName("Register").AllowAnonymous();

            return group;
        }
    }
}
