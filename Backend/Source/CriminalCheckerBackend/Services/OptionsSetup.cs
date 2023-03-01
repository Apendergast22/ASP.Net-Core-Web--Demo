using CriminalCheckerBackend.Model.Config;
using Microsoft.Extensions.Options;

namespace CriminalCheckerBackend.Services;

/// <summary>
/// Setup info from application config.
/// </summary>
public class OptionsSetup : IConfigureOptions<DataBaseInfo>, IConfigureOptions<PasswordServiceInfo>
{
    /// <summary>
    /// Configuration service.
    /// </summary>
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Instance new object of <see cref="OptionsSetup"/>.
    /// </summary>
    /// <param name="configuration">Configuration properties proxy.</param>
    public OptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <inheritdoc />
    public void Configure(DataBaseInfo options)
    {
        options.ConnectionString = _configuration.GetConnectionString("MainDataBase");
        _configuration.GetSection("DatabaseOption").Bind(options);
    }

    /// <inheritdoc />
    public void Configure(PasswordServiceInfo options)
    {
        options.PathToSalt = _configuration.GetSection("PathToSalt").Value;
        var count = _configuration.GetSection("ItemsCount").Value ?? throw new NullReferenceException("Can't get items count from application settings file.");
        options.ItemsCount = int.Parse(count);
    }
}