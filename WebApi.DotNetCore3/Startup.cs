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
using WebApi.DotNetCore3.Contexts;
using WebApi.DotNetCore3.Helpers;
using WebApi.DotNetCore3.Interfaces;
using WebApi.DotNetCore3.Repositories;

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
            // Necess�rio adicionar ao servi�o os Controllers para ser usado no m�todo Configure
            services.AddControllers();

            // Inje��o de Depend�ncia para vincular interface de reposit�rio com o reposit�rio em si
            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            // Inje��o de depend�ncia da connectionString do banco de dados
            // Para funcionar o m�todo UseSqlServer na vers�o 3.1 do .NET Core, � preciso instalar o framework "Microsoft.EntityFrameWorkCore.SqlServer"
            // Para funcionar o Code First com os comandos "add-migration" e "update-database", � preciso instalar o framework "Microsoft.EntityFrameWorkCore.Tools"
            services.AddDbContext<ProdutosContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime hostApplicationLifetime)
        {
            // M�todo que configura servi�os em tempo de execu��o, Antes IApplicationLifeTime, Agora IHostApplicationLifeTime
            // Com ApplicationStarted, sempre que a API iniciar, ir� executar os m�todos registrados, no caso o m�todo AddDefaultValuesDb
            hostApplicationLifetime.ApplicationStarted.Register(() =>
            {
                app.AddDefaultValuesDb();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // Realiza mapeamento dos Controllers
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
