using CriminalCheckerBackend.Model.DataBase.Exceptions;
using CriminalCheckerBackend.Model.DTO;
using CriminalCheckerBackend.Services.Database;
using Microsoft.AspNetCore.Mvc;
using NLog;
using ILogger = NLog.ILogger;

namespace CriminalCheckerBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DrinkersController : ControllerBase
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
        /// Instance new object of <see cref="DrinkersController"/>.
        /// </summary>
        /// <param name="db">Instance of <see cref="IDataBase"/>.</param>
        public DrinkersController(IDataBase db)
        {
            _db = db;
        }

        [HttpPost(Name = "SendPotentialDrinker")]
        public async Task<IActionResult> CheckTargetUser([FromBody]UserRequest userInfo)
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
    }
}