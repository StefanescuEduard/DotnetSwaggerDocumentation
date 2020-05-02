using API.WithAuthentication.Services;
using JwtAuthentication.Shared.Exceptions;
using JwtAuthentication.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.WithAuthentication.Controllers
{
    /// <summary>
    /// Provides authentication for users.
    /// </summary>
    [Route("identity/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService authenticationService;

        /// <summary>
        /// Constructs the controller state by initializing the `authenticationService` field.
        /// </summary>
        /// <param name="authenticationService">Provides authentication for a user.</param>
        public AuthenticationController(AuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        /// <summary>
        /// Authenticate the requested user.
        /// </summary>
        /// <param name="userCredentials">The requested user for authentication.</param>
        /// <returns>The generated Bearer Token if the authentication succeeds, otherwise returns
        /// a client or server error.</returns>
        /// <response code="200">If the authentication succeeds.</response>
        /// <response code="401">If the authentication fails.</response>
        /// <response code="500">If an exception is thrown.</response>
        [HttpPost]
        public IActionResult Authenticate([FromBody] UserCredentials userCredentials)
        {
            try
            {
                string token = authenticationService.Authenticate(userCredentials);
                return Ok(token);
            }
            catch (InvalidCredentialsException)
            {
                return Unauthorized();
            }
        }
    }
}