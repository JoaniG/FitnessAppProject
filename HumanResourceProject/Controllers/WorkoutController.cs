using Domain.Contracts;
using DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace FitnessProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutDomain _workoutDomain;
        public WorkoutController(IWorkoutDomain workoutDomain)
        {
            _workoutDomain = workoutDomain;
        }

        [HttpGet]
        [Route("getAll/{userId}")]
        public IActionResult GetAllWorkouts(Guid userId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var workouts = _workoutDomain.GetWorkoutsByUser(userId);

                if (workouts != null)
                {
                    return Ok(workouts);
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
        [Route("{id}")]
        public IActionResult GetWorkout(Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var workout = _workoutDomain.GetWorkout(id);

                if (workout != null)
                {
                    return Ok(workout);
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
        [Route("exercises/{workoutId}")]
        public IActionResult GetWorkoutExercises(Guid workoutId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var exercises = _workoutDomain.GetWorkoutExercises(workoutId);

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

        [HttpPost("exercises")]
        public IActionResult AddExercises(List<WorkoutExerciseDTO> exercises)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                _workoutDomain.AddExercisesInWorkout(exercises);
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

        [HttpPost("set")]
        public IActionResult AddSets(List<SetDTO> sets)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                _workoutDomain.AddSets(sets);
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

        [HttpPost("add")]
        public IActionResult AddWorkout(WorkoutDTO workout)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var id = _workoutDomain.CreateWorkout(workout);
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

        [HttpPost("addWorkout")]
        public IActionResult AddWholeWorkout(WorkoutPOST workout)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var id = _workoutDomain.AddWorkout(workout);
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
    }
}
