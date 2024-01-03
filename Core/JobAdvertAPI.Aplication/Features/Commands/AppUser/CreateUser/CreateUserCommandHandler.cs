using JobAdvertAPI.Aplication.Abstractions.Services;
using JobAdvertAPI.Aplication.DTOs.User;
using JobAdvertAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JobAdvertAPI.Aplication.Features.Commands.AppUser.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
{
    readonly IUserService _userService;
    private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;


    public CreateUserCommandHandler(IUserService userService, UserManager<Domain.Entities.Identity.AppUser> userManager)
    {
        _userService = userService;
        _userManager = userManager;
    }

    public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
    {

        CreateUserResponse response = await _userService.CreateAsync(new() 
        {
            Email = request.email,
            NameSurname = request.nameSurname,
            UserName = request.userName,
            Password = request.password,
            PasswordConfirm = request.passwordConfirm
        }); //alınan parametrelerle kullanıcı oluşturuluyor

        if (response.Succeeded) //kullanıcı oluşturulduysa
        {
            var user = await _userManager.FindByNameAsync(request.userName);
            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, AppRole.EmployerRole); 
                //kullanıcıya employer rolü atanıyor
            }
        }
        return new()
        {
            Message= response.Message,
            Succeeded = response.Succeeded
        };


    }
}
