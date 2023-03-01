using CriminalCheckerBackend.Model.DTO;
using CriminalCheckerBackend.Services.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        /// Instance of <see cref="IDataBase"/>.
        /// </summary>
        private readonly IDataBase _db;

        /// <summary>
        /// Create new instance <see cref="UserController"/>.
        /// </summary>
        /// <param name="db">Instance of <see cref="IDataBase"/>.</param>
        public UserController(IDataBase db)
        {
            _db = db;
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
            return Ok();
            return BadRequest();
            return Unauthorized();
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
            return Ok();
            return BadRequest();
            return Unauthorized();
        }

        /// <summary>
        /// Sign Out current user.
        /// </summary>
        /// <returns><see cref="Task"/> for response.</returns>
        /// <response code="200">Returns value is indicated that user is drinker.</response>
        /// <response code="500">Returns if some error happened.</response>
        [HttpPost("~/signin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SignOutUserAsync([FromBody] SignInDto dto)
        {
            return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
