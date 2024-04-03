using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using LyncasSales.Domain.Data;
using LyncasSales.Domain.DTO;
using LyncasSales.Domain.ViewModel;
using LyncasSales.Token;
using Microsoft.AspNetCore.Authentication;

namespace LyncasSales.Controllers
{
    [ApiController]
    [Route("v1")]
    public class AcountController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly TokenService _tokenService;

        public AcountController(AppDbContext dbContext, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, TokenService tokenService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDTO createUserDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new IdentityUser
            {
                UserName = createUserDTO.Email,
                Email = createUserDTO.Email
            };

            var result = await _userManager.CreateAsync(user, createUserDTO.Password);

            if (result.Succeeded)
            {


                await _signInManager.SignInAsync(user, false);
                return Ok("User registered successfully");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }



        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest("Invalid Login Attempt");

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
                await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, claimsPrincipal);

                var tokens = await _tokenService.GenerateTokensAsync(model.Email);
                return Ok(tokens);
            }
            else
            {
                return BadRequest("Invalid Login Attempt");
            }
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            return Ok("Logout successful");
        }


    }

}
