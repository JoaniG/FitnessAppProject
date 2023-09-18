using Domain.Contracts;
using DTO;
using DTO.UserDTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace FitnessProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoutineController : ControllerBase
    {
        private readonly IRoutineDomain _routineDomain;
        public RoutineController(IRoutineDomain routineDomain)
        {
            _routineDomain = routineDomain;
        }

        [HttpGet]
        [Route("{routineId}")]
        public IActionResult GetRotuine(Guid routineId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var routine = _routineDomain.GetRoutine(routineId);

                if (routine != null)
                {
                    return Ok(routine);
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
        [Route("routines/{userId}")]
        public IActionResult GetRotuinesByUserId(Guid userId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var routines = _routineDomain.GetRoutinesByUserId(userId);

                if (routines != null)
                {
                    return Ok(routines);
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


        [HttpPost("add")]
        public IActionResult AddRoutine(RoutineDTO routine)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var id = _routineDomain.MakeNewRoutine(routine);
                return Ok(id);                
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

        [HttpPost("create")]
        public IActionResult CreateRoutine(RoutinePost routine)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var id = _routineDomain.AddRoutine(routine);
                return Ok(id);
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

        [HttpPost("routine/exercises")]
        public IActionResult AddExercises(List<RoutineExerciseDTO> exercises)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                _routineDomain.AddExercisesInRoutine(exercises);
                return Ok();
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

        [HttpPut("update")]
        public IActionResult UpdateRoutine(RoutinePut routine)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                _routineDomain.UpdateRoutine(routine);
                return Ok();
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

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteRoutine(Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                _routineDomain.DeleteRoutine(id);
                return Ok();
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


    }
}
