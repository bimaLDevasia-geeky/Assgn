using System.Runtime.CompilerServices;
using System.Security.Claims;
using Hotel.Booking.Application.Command.Login.command;
using Hotel.Booking.Application.Command.RefreshToken.Commands;
using Hotel.Booking.Application.Command.Register;
using Hotel.Booking.Application.DTOs;
using Hotel.Booking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Booking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
      
        private readonly IMediator _mediator;

        public AuthController(IHttpContextAccessor httpContextAccessor, IMediator mediator)
        {
           
            _mediator = mediator;
        }

        [HttpGet("me")]
        [Authorize]
        public ActionResult GetCurrentUser()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var email = this.User.FindFirstValue(ClaimTypes.Email);
            var role = this.User.FindFirstValue(ClaimTypes.Role);
            Guid.TryParse(userId, out Guid parsedUserId);


            return Ok(new
            {
                Id = parsedUserId,
                Email = email,
                Role = role
            });
        }


        [HttpPost("register")]
        public async Task<ActionResult<Employee>> Register([FromBody] AdminRegisterCommand command)
        {
            // Registration logic here
            try
            {
                Employee employee = await _mediator.Send(command);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<AdminResponse>> Login([FromBody] AdminLoginCommand command)
        {
            try
            {
                AdminResponse response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost("refresh")]

        public async Task<IActionResult> RefreshToken()
        {
            try
            {
                string? refreshToken = Request.Cookies["refreshToken"] ;
                if (string.IsNullOrEmpty(refreshToken) )
                {
                    return Unauthorized(new { message = "Refresh token not found in context." });
                }
                RefreshAccessTokenCommand command = new RefreshAccessTokenCommand
                {
                    RefreshToken = refreshToken
                };
                string newAccessToken = await _mediator.Send(command);
                return Ok(new { Token = newAccessToken });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }


        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                Response.Cookies.Delete("refreshToken");
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
