using Loto3000Application.Exeption;
using Loto3000Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Loto3000.Controllers
{
    [Authorize]
    [Route("api/lotto/[controller]")]
    [ApiController]
    public class DrawController : ControllerBase
    {
        private readonly IDrawService drawService;
        private readonly Serilog.ILogger logger;
        public DrawController(IDrawService drawService, Serilog.ILogger logger)
        {
            this.drawService = drawService;
            this.logger = logger;
            logger.Debug("");
            logger.Information("");
            logger.Warning("");
            logger.Error("");
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("create-drow")]
        public async Task<IActionResult> CreateDrow()
        {
            int adminId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                var draw = await drawService.CreateDrowFromAdmin(adminId);
                return Created("api/lotto/draw/createDrow", draw);
            }
            catch (NotFoundException ex)
            {
                logger.Warning($"Its something wrong for {adminId} admin {new ClaimsPrincipalWrapper(User).Id}", ex);
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred, contact the admin!{ex}");
            }


        }
        [Authorize(Roles = "Administrator")]
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            int adminId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                var draws = await drawService.GetAllDrow(adminId);
                return Ok(draws);
            }
            catch (NotFoundException ex)
            {
                logger.Warning($"Its something wrong for {adminId} admin {new ClaimsPrincipalWrapper(User).Id}", ex);
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred, contact the admin!{ex}");
            }

        }
        [Authorize(Roles = "Administrator")]
        [HttpPost("deletedrow")]
        public async Task<IActionResult> DeleteDrow ()
        {
            int adminId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                await drawService.DeleteActiveDrow(adminId);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                logger.Warning($"Its something wrong for {adminId} admin {new ClaimsPrincipalWrapper(User).Id}", ex);
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred, contact the admin!{ex}");
            }
        }
       
    }
}
