using System.Security.Claims;
using CriminalCheckerBackend.Model;
using CriminalCheckerBackend.Model.DataBase.Exceptions;
using CriminalCheckerBackend.Model.DTO;
using CriminalCheckerBackend.Model.Errors;
using CriminalCheckerBackend.Services.Database;
using CriminalCheckerBackend.Services.Password;
using CriminalCheckerBackend.Services.ResponseBody;
using CriminalCheckerBackend.Services.Validator;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using ILogger = NLog.ILogger;

namespace CriminalCheckerBackend.Controllers
{
    /// <summary>
    /// Users access controller.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
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
        /// Instance of <see cref="IPassword"/>.
        /// </summary>
        private readonly IPassword _passwordService;

        /// <summary>
        /// Instance of <see cref="IResponseBodyBuilder"/>.
        /// </summary>
        private readonly IResponseBodyBuilder _bodyBuilder;

        /// <summary>
        /// Create new instance <see cref="UserController"/>.
        /// </summary>
        /// <param name="db">Instance of <see cref="IDataBase"/>.</param>
        /// <param name="validator">Instance of <see cref="IDtoValidator"/>.</param>
        /// <param name="passwordService">Instance of <see cref="IPassword"/>.</param>
        /// <param name="bodyBuilder">Instance of <see cref="IResponseBodyBuilder"/>.</param>
        public UserController(
            IDataBase db,
            IDtoValidator validator,
            IPassword passwordService,
            IResponseBodyBuilder bodyBuilder)
        {
            _db = db;
            _validator = validator;
            _passwordService = passwordService;
            _bodyBuilder = bodyBuilder;
        }

        /// <summary>
        /// Sign Up new user.
        /// </summary>
        /// <returns><see cref="Task"/> for response.</returns>
        /// <response code="200">Returns value is indicated that user is drinker.</response>
        /// <response code="400">Returns if requested data is invalid.</response>
        /// <response code="401">Returns if request data is correct, but token won't create.</response>
        [HttpPost("~/signup")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SignUpUserAsync([FromBody] SignUpDto dto)
        {
            try {
                _validator.Validate(dto);
                await _db.RegistrationNewUserAsync(new NewUserInfo(dto, _passwordService.Hash(dto.Password)))
                    .ConfigureAwait(false);

                return await SignInUserAsync(new SignInDto { Email = dto.Email, Password = dto.Password, KeepSign = false })
                    .ConfigureAwait(false);
            }
            catch (InvalidDtoException exception) {
                return BadRequest(_bodyBuilder.Build(exception));
            }
            catch (UserExistsException) {
                return BadRequest(_bodyBuilder.Build("ExistsAlready"));
            }
            catch (FileNotFoundException) {
                return Unauthorized(_bodyBuilder.Build());
            }
            catch {
                return BadRequest(_bodyBuilder.Build(string.Empty));
            }
        }

        /// <summary>
        /// Sign In user.
        /// </summary>
        /// <returns><see cref="Task"/> for response.</returns>
        /// <response code="200">Returns value is indicated that user is drinker.</response>
        /// <response code="400">Returns if requested data is invalid.</response>
        /// <response code="401">Returns if request pair of login-password is incorrect.</response>
        [HttpPost("~/signin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SignInUserAsync([FromBody] SignInDto dto)
        {
            // Log
            try {
                _validator.Validate(dto);
                var user = await _db.RegisteredUsers
                    .SingleOrDefaultAsync(u => u.Email == dto.Email)
                    .ConfigureAwait(false);

                if (user == null)
                    return Unauthorized();

                if (!_passwordService.VerifyPassword(dto.Password, user.Hash))
                    return BadRequest();

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, "User"),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties { AllowRefresh = true, IsPersistent = false };
                if (dto.KeepSign)
                    authProperties.ExpiresUtc = DateTimeOffset.Now.AddDays(30);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authProperties);
                return Ok();
            }
            catch (InvalidDtoException exception) {
                return BadRequest((_bodyBuilder.Build(exception)));
            }
            catch (FileNotFoundException) {
                return Unauthorized(_bodyBuilder.Build());
            }
            catch {
                return BadRequest(_bodyBuilder.Build(string.Empty));
            }
        }

        /// <summary>
        /// Sign Out current user.
        /// </summary>
        /// <returns><see cref="Task"/> for response.</returns>
        /// <response code="200">Returns value is indicated that user is drinker.</response>
        /// <response code="500">Returns if some error happened.</response>
        [Authorize]
        [HttpPost("~/signout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SignOutUserAsync([FromBody] SignInDto dto)
        {
            // Log
            try {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Ok();
            }
            catch {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
