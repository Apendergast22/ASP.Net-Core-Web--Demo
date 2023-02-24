using CriminalCheckerBackend.Model;
using CriminalCheckerBackend.Services;
using CriminalCheckerBackend.Services.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

#if DEBUG
ConfigureDebug();
#else
ConfigureRelease();
#endif

// Registry dependencies
void RegisterDependencies(WebApplicationBuilder builder)
{
    if (builder == null)
        throw new ArgumentNullException(nameof(builder));

    builder.Services.ConfigureOptions<OptionsSetup>();
    builder.Services.AddDbContext<IDataBase, PostgreSql>((serviceProvider, options) =>
    {
        var dataBaseOptions = serviceProvider.GetService<IOptions<DataBaseInfo>>()?.Value;
        if (dataBaseOptions == null)
            throw new ArgumentNullException(nameof(dataBaseOptions));

        options.UseNpgsql(dataBaseOptions.ConnectionString,
            sqlOptionAction =>
            {
                sqlOptionAction.EnableRetryOnFailure(dataBaseOptions.MaxRetryCount);
                sqlOptionAction.CommandTimeout(dataBaseOptions.CommandTimeout);
            });

        options.EnableDetailedErrors(dataBaseOptions.EnableDetailedErrors);
    });
}

// Configure debug server 
void ConfigureDebug()
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("CORS_Policy", policyBuilder =>
        {
            policyBuilder.AllowAnyHeader();
            policyBuilder.AllowAnyMethod();
            policyBuilder.AllowAnyOrigin();
        });
    });

    builder.Services.AddRouting(r => r.SuppressCheckForUnhandledSecurityMetadata = true);

    RegisterDependencies(builder);
    // Add services to the container.
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.UseAuthorization();
    app.MapControllers();
    app.UseCors("CORS_Policy");
    app.Run();
}


// Configure release server 
void ConfigureRelease()
{
    var builder = WebApplication.CreateBuilder(args);

    RegisterDependencies(builder);
    // Add services to the container.
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}