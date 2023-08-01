using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Routine
    {
        public Routine()
        {
            RoutineExercises = new HashSet<RoutineExercise>();
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<RoutineExercise> RoutineExercises { get; set; }
    }
}
