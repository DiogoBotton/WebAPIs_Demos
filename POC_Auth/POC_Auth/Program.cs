using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using POC_Shared.Certificates;
using POC_Shared.Options;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptions<JwtSecrets>().Bind(builder.Configuration.GetSection(nameof(JwtSecrets)));

var signingIssuerCertificate = new SigningIssuerCertificate();
RsaSecurityKey issuerSigningKey = signingIssuerCertificate.GetIssuerSigningKey();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,

                    //ValidIssuer = builder.Configuration["JwtSecrets:ValidIssuer"],
                    ValidateIssuer = false,

                    //ValidAudience = builder.Configuration["JwtSecrets:ValidAudience"],
                    ValidateAudience = false,

                    //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSecrets:SecretKey"])),
                    IssuerSigningKey = issuerSigningKey,
                    ClockSkew = TimeSpan.FromMinutes(1),
                };
            });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
