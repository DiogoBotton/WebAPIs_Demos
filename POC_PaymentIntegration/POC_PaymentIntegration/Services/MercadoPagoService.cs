using MercadoPago.Config;
using MercadoPago.Resource.Payment;
using Microsoft.Extensions.Options;
using POC_PaymentIntegration.Options;
using POC_PaymentIntegration.Services.Interfaces;

namespace POC_PaymentIntegration.Services;

public class MercadoPagoService : IMercadoPagoService
{
    public MercadoPagoService(IOptionsSnapshot<MercadoPagoOptions> options)
    {
        MercadoPagoConfig.AccessToken = options.Value.AccessToken;
    }

    public async Task<Payment> CreateCardPayment(decimal amount, string token, string email)
    {
        throw new NotImplementedException();
    }

    public async Task<Payment> CreatePixPayment(decimal amount, string email)
    {
        throw new NotImplementedException();
    }
}
