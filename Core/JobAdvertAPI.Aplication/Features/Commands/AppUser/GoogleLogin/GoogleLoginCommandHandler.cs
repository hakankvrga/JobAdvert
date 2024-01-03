
using JobAdvertAPI.Aplication.Abstractions.Services;
using MediatR;

namespace JobAdvertAPI.Aplication.Features.Commands.AppUser.GoogleLogin;

public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
{

    readonly IAuthService _authService;

    public GoogleLoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }
    public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
    {
    var token=   await  _authService.GoogleLoginAsync(request.IdToken, 900); // googleLoginAsync fonksiyonu kullanılarak token oluşturuldu 
        return new() // oluşturulan token döndürüldü
        {
            Token = token
        };

    }
}
