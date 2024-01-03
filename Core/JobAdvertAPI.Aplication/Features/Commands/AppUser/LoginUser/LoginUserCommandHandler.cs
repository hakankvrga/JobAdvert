using JobAdvertAPI.Aplication.Abstractions.Services;
using MediatR;

namespace JobAdvertAPI.Aplication.Features.Commands.AppUser.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
{
   readonly IAuthService _authService;

    public LoginUserCommandHandler(IAuthService authService)
    {
        _authService = authService; //authservice enjekte ediliyor
    }

    public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {
       var token= await _authService.LoginAsync(request.UsernameOrEmail, request.Password, 900);
        //Authservice üzerinden veriler çekilerek login işlemi gerçekleştiriliyor

        return new LoginUserSuccessCommandResponse()
        {
            Token = token //oluşturulan token döndürülüyor
        };
    }
}


