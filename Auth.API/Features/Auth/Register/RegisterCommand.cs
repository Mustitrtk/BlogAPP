using MediatR;

namespace Auth.API.Features.Auth.Register
{
    public record RegisterCommand(string username, string password) : IRequest<IResult>;
}
