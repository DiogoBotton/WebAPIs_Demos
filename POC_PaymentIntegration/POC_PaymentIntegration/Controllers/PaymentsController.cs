using MercadoPago.Resource.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POC_PaymentIntegration.Services;
using POC_PaymentIntegration.Services.Interfaces;

namespace POC_PaymentIntegration.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController : ControllerBase
{
    private readonly IMercadoPagoService _mercadoPagoService;

    public PaymentsController(IMercadoPagoService mercadoPagoService)
    {
        _mercadoPagoService = mercadoPagoService;
    }

    [HttpPost("pix")]
    public async Task<IActionResult> CreatePixPayment([FromBody] PixPaymentRequest request)
    {
        Payment payment = await _mercadoPagoService.CreatePixPayment(request.Amount, request.Email);
        return Ok(new { qrCode = payment.PointOfInteraction.TransactionData.QrCodeBase64, paymentId = payment.Id });
    }

    [HttpPost("card")]
    public async Task<IActionResult> CreateCardPayment([FromBody] CardPaymentRequest request)
    {
        Payment payment = await _mercadoPagoService.CreateCardPayment(request.Amount, request.Token, request.Email);
        return Ok(payment);
    }
}

public class PixPaymentRequest
{
    public decimal Amount { get; set; }
    public string Email { get; set; }
}

public class CardPaymentRequest
{
    public decimal Amount { get; set; }
    public string Token { get; set; }
    public string Email { get; set; }
}
