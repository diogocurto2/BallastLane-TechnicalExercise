using BallastLane.Presentation.WebAPI.Controllers.Security.Dtos;
using BallastLane.Products.Application.UseCases.Dtos;
using BallastLane.Security.Application.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BallastLane.Presentation.WebAPI.Controllers.Security
{
    [ApiController]
    [Route("api/security/login")]
    public class LoginController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ILoginUseCase _loginUseCase;

        public LoginController(
            IConfiguration configuration,
            ILoginUseCase loginUseCase)
        {
            _config = configuration;
            _loginUseCase = loginUseCase;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginDetails">
        /// User data for login.
        /// For this sample use this:
        /// {
        ///     "Username" : "om",
        ///     "Password" : "password"
        /// }
        /// </param>
        /// <returns code="200">
        /// Returns when user and password is valid
        /// </returns>
        /// <returns code="404">
        /// Retorns 404 when user is not authorized.
        /// </returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetProductByIdOutput))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Execute([FromBody] UserDto loginDetails)
        {
            bool resultado = ValidateUser(loginDetails);
            if (resultado)
            {
                var tokenString = GenerateTokenJwt();
                return Ok(new { token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }

        //Todo: refactor to domain
        private bool ValidateUser(UserDto loginDetails)
        {
            var user = _loginUseCase.Authenticate(loginDetails.UserName, loginDetails.Password);

            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Todo: refactor to domain
        private string GenerateTokenJwt()
        {
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var expiry = DateTime.Now.AddMinutes(60);
            var securityKey = new SymmetricSecurityKey
                              (Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials
                              (securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: issuer,
                                             audience: audience,
                                             expires: DateTime.Now.AddMinutes(120),
                                             signingCredentials: credentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
    }
}
