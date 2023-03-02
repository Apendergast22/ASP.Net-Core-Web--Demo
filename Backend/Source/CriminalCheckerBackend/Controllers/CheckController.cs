using CriminalCheckerBackend.Model.DTO;
using CriminalCheckerBackend.Model.Exceptions;
using CriminalCheckerBackend.Services.Database;
using CriminalCheckerBackend.Services.ResponseBody;
using CriminalCheckerBackend.Services.TomTomApi.Route;
using CriminalCheckerBackend.Services.Validator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        /// Instance of <see cref="IDtoValidator"/>.
        /// </summary>
        private readonly IDtoValidator _validator;

        /// <summary>
        /// Instance of <see cref="IResponseBodyBuilder"/>.
        /// </summary>
        private readonly IResponseBodyBuilder _bodyBuilder;

        /// <summary>
        /// Instance of <see cref="IRouteCalculator"/>.
        /// </summary>
        private readonly IRouteCalculator _routeCalculator;

        /// <summary>
        /// Instance new object of <see cref="CheckController"/>.
        /// </summary>
        /// <param name="db">Instance of <see cref="IDataBase"/>.</param>
        /// <param name="validator">Instance of <see cref="IDtoValidator"/>.</param>
        /// <param name="bodyBuilder">Instance of <see cref="IResponseBodyBuilder"/>.</param>
        /// <param name="routeCalculator">Instance of <see cref="IRouteCalculator"/>.</param>
        public CheckController(
            IDataBase db,
            IDtoValidator validator,
            IResponseBodyBuilder bodyBuilder,
            IRouteCalculator routeCalculator)
        {
            _db = db;
            _validator = validator;
            _bodyBuilder = bodyBuilder;
            _routeCalculator = routeCalculator;
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
        public async Task<IActionResult> CheckUserInDrinkersAsync([FromBody]DrinkerDto userInfo)
        {
            if (Log.IsInfoEnabled)
                Log.Info($"User {userInfo.Surname} {userInfo.Name} {userInfo.Patronymic} worries about their drink status");

            try {
                _validator.Validate(userInfo);

                var isUserDrinker = userInfo.Id == null
                    ? await _db.DoesUserDrinkerAsync(userInfo).ConfigureAwait(false)
                    : await _db.DoesUserDrinkerAsync((int)userInfo.Id).ConfigureAwait(false);

                return Ok(new Dictionary<string,bool>{ { "doesUserDrinker", isUserDrinker } });
            }
            catch (InvalidDtoException exception) {
                return BadRequest(_bodyBuilder.Build(exception));
            }
            catch {
                return BadRequest(_bodyBuilder.Build());
            }
        }

        /// <summary>
        /// Get police routing time.
        /// </summary>
        /// <returns><see cref="Task"/> for response.</returns>
        /// <response code="200">Returns value is indicated that user is drinker.</response>
        /// <response code="400">Returns if requested data was invalidate.</response>
        /// <response code="401">Returns if user didn't find.</response>
        [Authorize]
        [HttpPost("~/criminal/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CheckUserInCriminalAsync(int id)
        {
            if (Log.IsInfoEnabled)
                Log.Info($"User with ID = {id} wan't to learn their criminal status");

            try {
                var user = await _db.RegisteredUsers.SingleOrDefaultAsync(u => u.UserId == id).ConfigureAwait(false);
                if (user == null)
                    return Unauthorized(_bodyBuilder.Build("Can't find user"));

                return Ok(new Dictionary<string, int>
                {
                    {
                        "policeRouteTimeInMinutes",
                        await _routeCalculator.CalculateRouteAsync(user.City, user.Address).ConfigureAwait(false)
                    }
                });
            }
            catch (DaDataNotFoundException) {
                return BadRequest(_bodyBuilder.Build("UserAddressNotFound"));
            }
            catch (TomTomException) {
                return BadRequest(_bodyBuilder.Build("RouteTimeNotFound"));
            }
            catch {
                return BadRequest(_bodyBuilder.Build());
            }
        }
    }
}