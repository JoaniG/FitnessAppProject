using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UserDTO
{
    public class RoutineExerciseDTO
    {
        public Guid RoutineId { get; set; }
        public Guid ExerciseId { get; set; }
        public int Sets { get; set; }
        public string? Description { get; set; }
    }
}
