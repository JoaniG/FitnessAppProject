using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class WorkoutExerciseDTO
    {
        public string? Description { get; set; } = null!;
        public Guid WorkoutId { get; set; }
        public Guid ExerciseId { get; set; }
        public SetDTO[]? Sets { get; set; }
    }
}
