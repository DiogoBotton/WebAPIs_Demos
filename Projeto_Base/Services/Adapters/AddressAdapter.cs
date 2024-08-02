using Domains.Models.Addresses;
using Mapster;
using Services.DTOs.Results.Addresses;

namespace Services.Adapters;

public class AddressAdapter : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Address, AddressResult>();
    }
}
