using CriminalCheckerBackend.Model.Config;
using CriminalCheckerBackend.Services;
using CriminalCheckerBackend.Services.Database;
using CriminalCheckerBackend.Services.Password;
using CriminalCheckerBackend.Services.ResponseBody;
using CriminalCheckerBackend.Services.TomTomApi;
using CriminalCheckerBackend.Services.TomTomApi.Route;
using CriminalCheckerBackend.Services.Validator;
using Dadata;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
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
            throw new NullReferenceException(nameof(passwordOptions));
        if (passwordOptions.PathToSalt == null)
            throw new NullReferenceException(nameof(passwordOptions.PathToSalt));
        
        return new PasswordService(passwordOptions.PathToSalt, passwordOptions.ItemsCount);
    });

    builder.Services.AddScoped<IDtoValidator>(_ => new DtoValidator());
    builder.Services.AddScoped<IRouteCalculator>(provider =>
    {
        var cleanClient = provider.GetService<ICleanClientAsync>() ?? throw new NullReferenceException(nameof(TomTomUriBuilder));
        var uriBuilder = provider.GetService<TomTomUriBuilder>() ?? throw new NullReferenceException(nameof(TomTomUriBuilder));
        return new Client(uriBuilder, cleanClient);
    });
    
    builder.Services.AddScoped(provider =>
    {
        var tomTomOptions = provider.GetService<IOptions<TomTomInfo>>()?.Value;
        if (tomTomOptions == null)
            throw new NullReferenceException(nameof(tomTomOptions));
        if (string.IsNullOrWhiteSpace(tomTomOptions.BaseUri))
            throw new NullReferenceException(nameof(tomTomOptions.BaseUri));
        if (string.IsNullOrWhiteSpace(tomTomOptions.Token))
            throw new NullReferenceException(nameof(tomTomOptions.Token));

        return new TomTomUriBuilder(tomTomOptions.BaseUri, tomTomOptions.Token);
    });

    builder.Services.AddScoped<ICleanClientAsync>(provider =>
    {
        var daDataOptions = provider.GetService<IOptions<DaDataInfo>>()?.Value;
        if (daDataOptions == null)
            throw new NullReferenceException(nameof(daDataOptions));
        if (string.IsNullOrWhiteSpace(daDataOptions.ApiKey))
            throw new NullReferenceException(nameof(daDataOptions.ApiKey));
        if (string.IsNullOrWhiteSpace(daDataOptions.SecretKey))
            throw new NullReferenceException(nameof(daDataOptions.SecretKey));

        return string.IsNullOrWhiteSpace(daDataOptions.BaseUri) 
            ? new CleanClientAsync(daDataOptions.ApiKey, daDataOptions.SecretKey) 
            : new CleanClientAsync(daDataOptions.ApiKey, daDataOptions.SecretKey, daDataOptions.BaseUri);
    });

    builder.Services.AddScoped<IResponseBodyBuilder>(_ => new ResponseBodyBuilder());
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
#pragma warning disable CS8321
void ConfigureDebug()
#pragma warning restore CS8321
{
    var builder = WebApplication.CreateBuilder(args);
    ConfigureAuthentication(builder);
    ConfigureNoCors(builder);

    RegisterDependencies(builder);

    // Add services to the container.
    builder.Services.AddControllers().AddNewtonsoftJson();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddMvc();
    builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Users checker", Version = "v1" }); });
    builder.Services.AddSwaggerGenNewtonsoftSupport();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseAuthentication();    // Checking who is connected user.
    app.UseAuthorization();     // Checking what permissions has connected user.
    app.MapControllers();
    app.UseCors("CORS_Policy");
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<IDataBase>();
        context.RecreateDataBase();
    }
        
    app.Run();
}


// Configure release server.
#pragma warning disable CS8321
void ConfigureRelease()
#pragma warning restore CS8321
{
    var builder = WebApplication.CreateBuilder(args);

    ConfigureAuthentication(builder);
    RegisterDependencies(builder);

    // Add services to the container.
    builder.Services.AddControllers().AddNewtonsoftJson();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Users checker", Version = "v1" }); });

    // Add support special Newtonsoft for swagger.
    builder.Services.AddSwaggerGenNewtonsoftSupport();

    var app = builder.Build();
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}