using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    [Key]
    [Column("UserId", TypeName = "integer")]
    public int UserId { get; set; }

}