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
}
