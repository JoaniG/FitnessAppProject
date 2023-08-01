using Domain.Contracts;
using DTO.UserDTO;
using Microsoft.AspNetCore.Mvc;

namespace FitnessProject.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AccountController : ControllerBase
    {
		private readonly ILoginService _loginService;
		public AccountController(ILoginService loginService)
		{
			_loginService = loginService;
		}

		[HttpPost("register")]
		public IActionResult Register(RegisterDTO registerDTO)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest();
				}
				var token = _loginService.Register(registerDTO);

				if (token != null)
				{
					return Ok(token);
				}
				else
				{
					return NotFound();
				}
			}
			catch (ArgumentException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(600, ex);
			}
		}

		[HttpPost("login")]
		public IActionResult Login(LoginDTO loginDTO)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest();
				}
				var user = _loginService.Login(loginDTO);

				if (user != null)
				{
					return Ok(user);
				}
				else
				{
					return Unauthorized();
				}
			}
			catch (ArgumentException ex)
			{
				return Unauthorized(ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(700, ex);
			}
		}

		[HttpPatch("changePassword")]
		public IActionResult ChangePassword(PasswordChangeDTO passwordChangeDTO)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest();
				}

				_loginService.ChangePassword(passwordChangeDTO);

				return Ok();
			}
			catch (ArgumentException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(700, ex);
			}
		}
	}
}
