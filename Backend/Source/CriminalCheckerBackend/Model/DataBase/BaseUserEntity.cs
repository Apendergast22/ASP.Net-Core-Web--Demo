namespace CriminalCheckerBackend.Model.DataBase;

/// <summary>
/// Base user database  model.
/// </summary>
public abstract class BaseUserEntity
{
    /// <summary>
    /// Empty constructor.
    /// </summary>
    protected BaseUserEntity()
    {
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="userId">User identifier.</param>
    protected BaseUserEntity(int userId)
    {
        UserId = userId;
    }

    /// <summary>
    /// Get or set user id.
    /// </summary>
    public int UserId { get; set; }

}