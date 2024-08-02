using System.Reflection;
using Api.DI;
using Infrastructure.Extensions;
using Services.Swagger;

var builder = WebApplication.CreateBuilder(args);

var assembly = Assembly.GetExecutingAssembly();

builder.Configuration.AddJsonFile("appsettings.local.json", true, true);

builder.Services.AddApiDependencies(builder.Configuration, assembly);

builder.Services.AddControllers(options =>
    {
        options.Filters.Add<CustomActionFilter>();
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new BaseResponseJsonConverterFactory());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Pipeline
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await app.RunDatabaseMigrations<Program>(scope.ServiceProvider, app.Logger);
}

if (app.Environment.IsProduction())
{
    app.UseHsts();
    app.UseHttpsRedirection();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();

    //app.MapGet("/seed",
    //    async (DbSeeder dbSeeder) =>
    //    {
    //        (bool isSeed, string? error) = await dbSeeder.SeedData();
    //        return Results.Ok(new { Seeder = isSeed, Error = error });
    //    });
}

if (!builder.Environment.IsProduction() && !builder.Environment.IsEnvironment("Test"))
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
        c.InjectStylesheet("/css/Swagger.css");
        c.DisplayRequestDuration();
    });
}

app.UseStaticFiles();

app.UseCors();

app.UseRouting();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
#endregion

await app.RunAsync();