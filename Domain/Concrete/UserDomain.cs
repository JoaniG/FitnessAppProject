using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO.UserDTO;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    internal class UserDomain : DomainBase, IUserDomain
    {
        public UserDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        private IUserRepository userRepository => _unitOfWork.GetRepository<IUserRepository>();
        private IMeasurementRepository measurementRepository => _unitOfWork.GetRepository<IMeasurementRepository>();

        public UserDTO GetByUsername(string username)
        {
            User user = userRepository.GetByUsername(username);
            return _mapper.Map<UserDTO>(user);
        }

        public List<UserDTO> GetAllUsers()
        {
            var users = userRepository.GetAll().AsEnumerable();
            List<UserDTO> userList = new List<UserDTO>();
            foreach(var user in users)
            {
                var listUser = _mapper.Map<UserDTO>(user);
                userList.Add(listUser);
            }
            return userList;
        }

        public UserDTO GetUserById(Guid id)
        {
            var user = userRepository.GetById(id);
            return _mapper.Map<UserDTO>(user);
        }

        public bool GetUserSettings(Guid id)
        {
            var user = userRepository.GetById(id);
            return user.WeightSetting;
        }

        public List<Measurement> GetUserMeasurement(Guid id)
        {
            var measurements = measurementRepository.GetByUserId(id);
            var sortedMeasurements = measurements.OrderByDescending(measurement => measurement.Date).ToList();
            return sortedMeasurements;
        }

        public void AddUserMeasurement(MeasurementInputDTO measurement)
        {
            Measurement measurementToAdd = new Measurement
            {
                Biceps = measurement.Biceps,
                Waist = measurement.Waist,
                Hips = measurement.Hips,
                Thighs = measurement.Thighs,
                Chest = measurement.Chest,
                UserId = measurement.UserId,
                Height = measurement.Height,
                Weight = measurement.Weight,
                Date = DateTime.Now,
                Id = Guid.NewGuid()
            };
            measurementRepository.Add(measurementToAdd);
            _unitOfWork.Save();
        }

        public void ChangeMeasurementUnit(ChangeMeasurementDTO changeMeasurement)
        {
            var user = userRepository.GetById(changeMeasurement.UserId);
            user.WeightSetting = changeMeasurement.WeightSetting;
            userRepository.Update(user);
            _unitOfWork.Save();

        }
    }
}
