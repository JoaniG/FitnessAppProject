using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class WorkoutDTO
    {
        public DateTime TimeStarted { get; set; }
        public DateTime TimeEnded { get; set; }
        public Guid UserId { get; set; }
    }

    public class WorkoutPOST
    {
        public DateTime TimeStarted { get; set; }
        public DateTime TimeEnded { get; set; }
        public Guid UserId { get; set; }
        public WorkoutExercisePost[] workoutExercises { get; set; }

    }

    public class WorkoutExercisePost
    {
        public string? Description { get; set; } = null!;
        public Guid ExerciseId { get; set; }
        public SetPost[]? Sets { get; set; }
    }

    public class SetPost
    {
        public int Reps { get; set; }
        public int Weight { get; set; }
    }
}
