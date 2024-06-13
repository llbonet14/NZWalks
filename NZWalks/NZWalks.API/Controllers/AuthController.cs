using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (!identityResult.Succeeded)
                return BadRequest("Something went wrong");

            if (registerRequestDto.Roles is not null && registerRequestDto.Roles.Length != 0)
                identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

            if (!identityResult.Succeeded)
                return BadRequest("Something went wrong");

            return Ok("User was registered");
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);

            if (user is null)
                return BadRequest("Invalid credentials");

            var signInResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (!signInResult)
                return BadRequest("Invalid credentials");

            var userRoles = await userManager.GetRolesAsync(user);

            if (userRoles is null)
                return BadRequest("Invalid credentials");

            var jwtToken = tokenRepository.CreateJWTToken(user, [.. userRoles]);
            var response = new LoginResponseDto
            {
                JwtToken = jwtToken
            };

            return Ok(response);
        }
    }
}
