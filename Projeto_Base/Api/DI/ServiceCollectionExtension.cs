using FluentValidation;
using System.Reflection;
using Mapster;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Controllers;
using FluentValidation.Validators;
using System.Globalization;
using Microsoft.OpenApi.Any;
using Domains.Security;
using Domains.Services;
using Domains.Options;
using Domains.Models.Users.Interfaces;
using Domains.SeedWork;
using Infrastructure.Repositories.Base;
using Infrastructure.Repositories;
using Infrastructure.Contexts;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Services.Services.UserServices.Interfaces;
using Services.Services.UserServices;
using Services.DTOs;
using Infrastructure.Swagger;
using Services.Extensions;
using Api.Seed;
using Services.Paginator.Services;
using Services.Paginator.Services.Interfaces;

namespace Api.DI;

/// <summary>
/// API Dependencies Configuration
/// </summary>
public static partial class ServiceCollectionExtension
{
    #region Configurations

    private static void ConfigureFluentValidator(this IServiceCollection services, Assembly assembly)
    {
        var assemblies = new Assembly[] { assembly, typeof(ServiceCollectionExtension).Assembly };

        AssemblyScanner.FindValidatorsInAssemblies(assemblies, true)
            .ForEach(e => services.AddTransient(e.InterfaceType, e.ValidatorType));

        foreach (Type validatorType in typeof(ServiceCollectionExtension).Assembly.GetTypes())
            if (validatorType.IsAssignableTo(typeof(IPropertyValidator)))
                services.AddTransient(validatorType);
    }

    private static void ConfigureActor(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<Actor>();
    }

    private static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
    {
        UriOptions uriOptions = new();
        configuration.Bind("UriOptions", uriOptions);

        services.AddSingleton(uriOptions);

        services.AddCors(setup => setup.AddDefaultPolicy(policy =>
           policy.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod()));
    }

    private static void ConfigureResetPasswordOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<ResetCredentialsOptions>().Bind(configuration.GetSection(nameof(ResetCredentialsOptions)));
    }

    private static void ConfigureUriOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<UriOptions>().Bind(configuration.GetSection(nameof(UriOptions)));
    }

    private static void ConfigureEmailService(this IServiceCollection services)
    {
        services.AddScoped(typeof(EmailService));
    }

    private static void ConfigureSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Projeto Base .NET - V1", Version = "v1" });

            options.DocInclusionPredicate((docName, apiDesc) =>
            {
                if (docName == "v1")
                    return true;

                return false;
            });

            var jwtSecurityScheme = new OpenApiSecurityScheme()
            {
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            options.CustomSchemaIds(d => d.GetSchemaId());
            options.OperationFilter<BaseResponseOperationFilter>();

            options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });

            options.MapType<TimeSpan>(() => new OpenApiSchema
            {
                Type = "string",
                Example = new OpenApiString("00:00:00")
            });

            options.MapType<TimeSpan?>(() => new OpenApiSchema
            {
                Type = "string",
                Example = new OpenApiString("00:00:00")
            });

            options.IncludeXmlComments("Api.xml");
        });
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPaginatedService<>), typeof(PaginatedService<>));
        services.AddScoped<IUserService, UserService>();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
    }

    private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApiDbContext>(options =>
        {
            // Npgsql (Postgres)
            //options.UseNpgsql(configuration.GetConnectionString("Default"), 
            //    b => b.MigrationsAssembly("Infrastructure"));


            // Sql Server
            //options.UseSqlServer(configuration.GetConnectionString("Default"),
            //    b => b.MigrationsAssembly("Infrastructure"));

            // MySql
            options.UseMySql(configuration.GetConnectionString("Default"),
                         ServerVersion.AutoDetect(configuration.GetConnectionString("Default")),
                         b => b.MigrationsAssembly("Infrastructure"))
            .EnableDetailedErrors(false)
            .EnableSensitiveDataLogging(false);
        });

        services.AddScoped<DbContext, ApiDbContext>(sp => sp.GetService<ApiDbContext>());

        services.TryAddTransient<IValidatorFactory, ServiceProviderValidatorFactory>();
        services.AddFluentValidationRulesToSwagger();
    }

    private static void AddSeeder(this IServiceCollection services)
    {
        services.AddScoped<DbSeeder>();
    }
    #endregion

    /// <summary>
    /// API Dependencies Configuration
    /// </summary>
    public static void AddApiDependencies(this IServiceCollection services, IConfiguration configuration, Assembly assembly)
    {
        TypeAdapterConfig.GlobalSettings.Scan(assembly, typeof(ServiceCollectionExtension).Assembly);

#if DEBUG
        TypeAdapterConfig.GlobalSettings.Compiler = exp => exp.CompileWithDebugInfo();
#endif

        services.ConfigureUriOptions(configuration);

        services.ConfigureResetPasswordOptions(configuration);

        services.ConfigureCors(configuration);

        services.ConfigureEmailService();

        services.AddRepositories();

        services.AddServices();

        services.AddDbContext(configuration);

        services.AddSeeder();

        services.ConfigureFluentValidator(assembly);

        services.ConfigureActor();

        services.AddSingleton(configuration);

        services.ConfigureSwaggerGen();

        services.AddHealthChecks();
    }
}