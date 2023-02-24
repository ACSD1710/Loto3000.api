using Loto3000Application.Dto.UserDto;
using Loto3000Application.Exeption;
using Loto3000Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Loto3000.Controllers
{
    [Authorize]
    [Route("api/loto/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
      
        private readonly IUserService userServices;
        private readonly Serilog.ILogger logger;

        public UserController(IUserService userServices, Serilog.ILogger logger)
        {
            this.userServices = userServices;
            this.logger = logger;
            logger.Debug("");
            logger.Information("");
            logger.Warning("");
            logger.Error("");
        }

        [AllowAnonymous]
        [HttpPost("createUser")]
        public async Task<IActionResult> CreateUser(CreateUserDto model)
        {
            if (!ModelState.IsValid)
            {
                logger.Error("Data doesnt match");
                return BadRequest(ModelState);
            }
            try
            {
                var userModel = await userServices.CreateUser(model);
                return Created("api/loto/user/login", userModel);
            }
            catch(ValidationException ex)
            {
                logger.Warning($"Its something wrong for user", ex);
                return BadRequest();
            }
                
        }
        [Authorize(Roles = "User")]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto model)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            if (model is null)
            {
                return BadRequest(model);
            }
            try
            {
                await userServices.ChangeUserPassword(model, userId);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                
                return NotFound(ex);
            }
            catch (ValidationException ex)
            {
                
                return BadRequest(ex);
            }
            
        }

        [Authorize(Roles = "User")]
        [HttpPost("editUser")]
        public async Task<IActionResult> EditUser(UpdateUserDto model)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (model is null )
            {
                return BadRequest(model);
            }
            try
            {
                await userServices.UpdateUser(model, userId);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                logger.Warning($"Its something wrong for {userId} user{new ClaimsPrincipalWrapper(User).Id}", ex);
                return NotFound();
            }
            catch (ValidationException ex)
            {
                logger.Warning($"User must be older than 18 years{new ClaimsPrincipalWrapper(User).Name}", ex);
                return BadRequest();
            }
           

        }
        [Authorize(Roles = "User")]
        [HttpPost("deleteUser")]
        public async Task<IActionResult> DeleteUser()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                await userServices.DeleteUser(userId);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                logger.Warning($"User with this {userId} doesn't exists{new ClaimsPrincipalWrapper(User).Id}", ex);
                return BadRequest();
            }
            
        }
        [Authorize(Roles = "User")]
        [HttpGet("userInfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            try
            {
                var userInfo = await userServices.GetUserInfo(userId);
                return Ok(userInfo);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "User")]
        [HttpPost("buycredits")]
        public async Task<IActionResult> BuyCredits (UserBuyCreditsDto model)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            logger.Error("Its somethis wring with card data");

            if (model is null)
            {
                return BadRequest(model);
            }
            try
            {
                var massage = await userServices.BuyCreditsFromUser(model, userId);
                return Ok(massage);
            }
            catch (NotFoundException ex)
            {
                logger.Warning($"User With this {userId} {new ClaimsPrincipalWrapper(User).Id}", ex);
                return NotFound();
            }
            catch (ValidationException)
            {
                logger.Error("You have to fill all fields");
                return BadRequest();
            }

        }
        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult Login(LoginUserDto model)
        {
            try
            {
                var token = userServices.AuthenticateUser(model);
                return Ok(token.Token);
            }
            catch (NotFoundException)
            {
                logger.Debug("information doesnt match");
                return NotFound();
            }
        }
    }
}

