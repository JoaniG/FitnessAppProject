using DTO.UserDTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class RoutinePost
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public List<RoutineExercisePost> RoutineExercises { get; set; }

    }

    public class RoutineExercisePost
    {
        public Guid ExerciseId { get; set; }
        public int Sets { get; set; }
        public string? Description { get; set; }
    }

    public class RoutinePut
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public List<RoutineExercisePut> RoutineExercises { get; set; }
        public Guid Id { get; set; }
    }

    public class RoutineExercisePut
    {
        public Guid RoutineId { get; set; }
        public Guid ExerciseId { get; set; }
        public int Sets { get; set; }
        public string? Description { get; set; }
    }

}
