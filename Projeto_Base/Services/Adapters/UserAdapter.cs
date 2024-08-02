using Domains.Models.Users;
using Mapster;
using Services.DTOs.Requests.Users;
using Services.DTOs.Results.Users;

namespace Services.Adapters;

public class UserAdapter : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UserCreate, User>();

        config.NewConfig<User, UserResult>();

        config.NewConfig<User, UserSimpleResult>();
    }
}
