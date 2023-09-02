using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ExerciseDTO
    {
        public string Name { get; set; } = null!;
        public int PrimaryTarget { get; set; }
        public int? SecondaryTarget { get; set; }
    }
}
