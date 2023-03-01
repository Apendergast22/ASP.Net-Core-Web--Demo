using CriminalCheckerBackend.Model.DataBase.Exceptions;
using CriminalCheckerBackend.Model.DTO;
using CriminalCheckerBackend.Services.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using ILogger = NLog.ILogger;

namespace CriminalCheckerBackend.Controllers
{
    /// <summary>
    /// Check users controller.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CheckController : ControllerBase
    {
        /// <summary>
        /// Current class logger.
        /// </summary>
        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Instance of <see cref="IDataBase"/>.
        /// </summary>
        private readonly IDataBase _db;

        /// <summary>
        /// Instance new object of <see cref="CheckController"/>.
        /// </summary>
        /// <param name="db">Instance of <see cref="IDataBase"/>.</param>
        public CheckController(IDataBase db)
        {
            _db = db;
        }

        /// <summary>
        /// Getting info about user from drinkers data base.
        /// </summary>
        /// <param name="userInfo">Information about target user.</param>
        /// <returns><see cref="Task"/> for response.</returns>
        /// <response code="200">Returns value is indicated that user is drinker.</response>
        /// <response code="400">Returns if requested data was invalidate/</response>
        [HttpPost("~/drinker")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CheckUserInDrinkersAsync([FromBody]UserRequest userInfo)
        {
            if (Log.IsInfoEnabled)
                Log.Info($"User {userInfo.Surname} {userInfo.Name} {userInfo.Patronymic} worries about their drink status");

            try
            {
                
                return Ok(new Dictionary<string,bool>{ { "doesUserDrinker", await _db.DoesUserDrinkerAsync(userInfo).ConfigureAwait(false) } });
            }
            catch (NewUserNotValidValueException exception)
            {
                var incorrectValue = exception.IsValueEmpty 
                    ? string.Empty 
                    : exception.ConflictPropertyValue;

                return ValidationProblem(
                    statusCode: StatusCodes.Status400BadRequest,
                    title: "Incorrect request data",
                    detail: incorrectValue,
                    type: nameof(NewUserNotValidValueException),
                    instance: exception.ConflictPropertyName);
            }
            catch
            {
                return BadRequest("Unknown error");
            }
        }

        /// <summary>
        /// Get police routing time.
        /// </summary>
        /// <returns><see cref="Task"/> for response.</returns>
        /// <response code="200">Returns value is indicated that user is drinker.</response>
        /// <response code="400">Returns if requested data was invalidate.</response>
        [Authorize()]
        [HttpPost("~/criminal")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CheckUserInCriminalAsync()
        {
            return Ok(new Dictionary<string, ulong> { { "policeRouteTimeInMinutes", 100UL } });
        }
    }
}