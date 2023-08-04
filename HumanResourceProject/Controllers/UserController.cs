using Domain.Contracts;
using DTO.UserDTO;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceProject.Controllers

{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserDomain _userDomain;

        public UserController(IUserDomain userDomain)
        {
            _userDomain = userDomain;
        }


        [HttpGet]
        [Route("getAllUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var users = _userDomain.GetAllUsers();

                if (users != null)
                {
                    return Ok(users);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpGet]
        [Route("{userId}")]
        public IActionResult GetUserById([FromRoute] Guid userId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                var user = _userDomain.GetUserById(userId);

                if (user != null)
                    return Ok(user);

                return NotFound();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        [Route("username/{username}")]
        public IActionResult GetUserByUsername([FromRoute] string username)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                var user = _userDomain.GetByUsername(username);

                if (user != null)
                    return Ok(user);

                return NotFound();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("measurements/{userId}")]
        public IActionResult GetUserMeasurements([FromRoute] Guid userId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                var userMeasurements = _userDomain.GetUserMeasurement(userId);

                if (userMeasurements != null)
                    return Ok(userMeasurements);

                return NotFound();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("measurements/add")]
        public IActionResult AddMeasurement([FromBody] MeasurementInputDTO measurement)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                if (measurement is null)
                    return BadRequest("Object is null");

                _userDomain.AddUserMeasurement(measurement);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

    }
}


