using Domain.Contracts;
using DTO;
using DTO.UserDTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace FitnessProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseDomain _exerciseDomain;
        public ExerciseController(IExerciseDomain exerciseDomain)
        {
            _exerciseDomain = exerciseDomain;
        }

        [HttpGet]
        [Route("getAllExercises")]
        public IActionResult GetAllExercises()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var exercises = _exerciseDomain.GetAllExercises();

                if (exercises != null)
                {
                    return Ok(exercises);
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
        [Route("routine/{routineId}")]
        public IActionResult GetExercisesByRoutineId([FromRoute] Guid routineId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                var routineExercise = _exerciseDomain.GetExercisesInRoutine(routineId);

                if (routineExercise != null)
                    return Ok(routineExercise);

                return NotFound();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetExerciseById([FromRoute] Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                var exercise = _exerciseDomain.GetExerciseById(id);

                if (exercise != null)
                    return Ok(exercise);

                return NotFound();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("add")]
        public IActionResult AddExercise(ExerciseDTO exercise)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                _exerciseDomain.AddExercise(exercise);
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
