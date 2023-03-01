using CriminalCheckerBackend.Model.Config;
using CriminalCheckerBackend.Services;
using CriminalCheckerBackend.Services.Database;
using CriminalCheckerBackend.Services.Password;
using CriminalCheckerBackend.Services.Validator;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PasswordService = CriminalCheckerBackend.Services.Password.PasswordService;

#if DEBUG
ConfigureDebug();
#else
ConfigureRelease();
#endif


// Registry dependencies.
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

    builder.Services.AddScoped<IPassword>(provider =>
    {
        var passwordOptions = provider.GetService<IOptions<PasswordServiceInfo>>()?.Value;
        if (passwordOptions == null)
            throw new ArgumentNullException(nameof(passwordOptions));

        if (passwordOptions.PathToSalt == null)
            throw new ArgumentNullException(nameof(passwordOptions.PathToSalt));
        
        return new PasswordService(passwordOptions.PathToSalt, passwordOptions.ItemsCount);
    });

    builder.Services.AddScoped<IDtoValidator>(provider => new DtoValidator());
}

// Configure authentication.
void ConfigureAuthentication(WebApplicationBuilder builder)
{
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.ExpireTimeSpan = TimeSpan.FromHours(2);
            options.LoginPath = "/signin";
            options.LogoutPath = "/signout";
        });
}

// Disable CORS control.
void ConfigureNoCors(WebApplicationBuilder builder)
{
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
}

// Configure debug server.
void ConfigureDebug()
{
    var builder = WebApplication.CreateBuilder(args);
    ConfigureAuthentication(builder);
    ConfigureNoCors(builder);

    RegisterDependencies(builder);
    // Add services to the container.
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddSwaggerGenNewtonsoftSupport();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseAuthentication();    // Checking who is connected user.
    app.UseAuthorization();     // Checking what permissions has connected user.
    app.MapControllers();
    app.UseCors("CORS_Policy");
    app.Run();
}


// Configure release server 
void ConfigureRelease()
{
    var builder = WebApplication.CreateBuilder(args);

    ConfigureAuthentication(builder);
    RegisterDependencies(builder);
    // Add services to the container.
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Add support special Newtonsoft for swagger.
    builder.Services.AddSwaggerGenNewtonsoftSupport();

    var app = builder.Build();
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}