using DTO.UserDTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IUserDomain
    {
        List<UserDTO> GetAllUsers();
        UserDTO GetUserById(Guid id);
        UserDTO GetByUsername(string username);
        void AddUserMeasurement(MeasurementInputDTO measurement);
        List<Measurement> GetUserMeasurement(Guid id);
    }
}
