using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UserDTO
{
    public class MeasurementInputDTO
    {
        public int? Chest { get; set; }
        public int? Waist { get; set; }
        public int? Hips { get; set; }
        public int? Biceps { get; set; }
        public int? Thighs { get; set; }
        public int? Weight { get; set; }
        public int? Height { get; set; }
        public Guid UserId { get; set; }
    }

    public class ChangeMeasurementDTO
    {
        public Guid UserId { get; set; }
        public bool WeightSetting { get; set; }
    }
}
