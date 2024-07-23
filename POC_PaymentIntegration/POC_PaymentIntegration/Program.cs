using POC_PaymentIntegration.Options;
using POC_PaymentIntegration.Services;
using POC_PaymentIntegration.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMercadoPagoService, MercadoPagoService>();

builder.Services.AddOptions<MercadoPagoOptions>().Bind(builder.Configuration.GetSection(nameof(MercadoPagoOptions)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
