using MercadoPago.Resource.Payment;

namespace POC_PaymentIntegration.Services.Interfaces;

public interface IMercadoPagoService
{
    Task<Payment> CreatePixPayment(decimal amount, string email);
    Task<Payment> CreateCardPayment(decimal amount, string token, string email);
}
