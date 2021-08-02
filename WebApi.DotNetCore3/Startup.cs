using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using WebApi.DotNetCore3.Contexts;
using WebApi.DotNetCore3.Helpers;
using WebApi.DotNetCore3.Interfaces;
using WebApi.DotNetCore3.Repositories;
using WebApi.DotNetCore3.Services;

namespace WebApi.DotNetCore3
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Necessário adicionar ao serviço os Controllers para ser usado no método Configure
            services.AddControllers();

            // Injeção de Dependência para vincular interface de repositório com o repositório em si
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            services.AddScoped<IFilesService, FilesService>();

            // Injeção de dependência da connectionString do banco de dados
            // Para funcionar o método UseSqlServer na versão 3.1 do .NET Core, é preciso instalar o framework "Microsoft.EntityFrameWorkCore.SqlServer"
            // Para funcionar o Code First com os comandos "add-migration" e "update-database", é preciso instalar o framework "Microsoft.EntityFrameWorkCore.Tools"
            services.AddDbContext<ProdutosContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //*
            //Configurando o JWT (Autentificação)
            //*
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            }).AddJwtBearer("JwtBearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //Verificando...

                    //Quem esta solicitando
                    ValidateIssuer = true,

                    //Quem esta recebendo
                    ValidateAudience = true,

                    //Valida o tempo de expiração do token
                    ValidateLifetime = true,

                    //Forma da criptografia
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("produtos-chave-autenticacao")),

                    //Tempo de expiração do token
                    ClockSkew = TimeSpan.FromMinutes(30),

                    // Nome da issuer, de onde está vindo
                    ValidIssuer = "WebApi.DotNetCore3.Produtos",

                    // Nome da audience, de onde está vindo
                    ValidAudience = "WebApi.DotNetCore3.Produtos"
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:8080")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();
                                  });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime hostApplicationLifetime)
        {
            // Método que configura serviços em tempo de execução, Antes IApplicationLifeTime, Agora IHostApplicationLifeTime
            // Com ApplicationStarted, sempre que a API iniciar, irá executar os métodos registrados, no caso o método AddDefaultValuesDb
            hostApplicationLifetime.ApplicationStarted.Register(() =>
            {
                app.AddDefaultValuesDb();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            // Realiza mapeamento dos Controllers
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
