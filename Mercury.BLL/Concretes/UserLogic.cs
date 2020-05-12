using AutoMapper;
using Mercury.BLL.Dtos;
using Mercury.BLL.Intefaces;
using Mercury.DAL.Entities;
using Mercury.DAL.Helpers;
using Mercury.DAL.Repository;
using Mercury.DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.BLL.Concretes
{
    public class UserLogic : IUserLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserLogic(IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> GetUser(long userId)
        {
            var user = await _userRepository.GetWithThrow(u => u.Id == userId);

            var mappedUser = _mapper.Map<User, UserDto>(user);

            return mappedUser;
        }

        public async Task<List<UserDto>> GetUsers(int itemPerPage, int page)
        {
            var query = new QueryDate<User, long>
            {
                CurrentPage = page,
                ItemsPerPage = itemPerPage,
                SortBy = u => u.Id
            };

            var users = await _userRepository.GetAllWithPaging(query);

            var mappedUsers = _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);

            return mappedUsers.ToList();
        }

        public async Task<UserDto> CreateUser(NewUserDto user)
        {
            if (user == null)
            {
                throw new InvalidOperationException();
            }

            var userInDb = _mapper.Map<NewUserDto, User>(user);

            _userRepository.Add(userInDb);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<User, UserDto>(userInDb);
        }

        public async Task<UserDto> UpdateUser(UserDto user)
        {
            var userInDb = await _userRepository.GetWithThrow(u => u.Id == user.Id);

            _mapper.Map(user, userInDb);

            await _unitOfWork.CompleteAsync();

            return _mapper.Map<User, UserDto>(userInDb);
        }

        public async Task DeleteUser(long userId)
        {
            var userInDb = await _userRepository.GetWithThrow(u => u.Id == userId);

            _userRepository.Remove(userInDb);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<UserDto>> GetSubscribingUsers()
        {
            var usersList = await _userRepository.GetAll();

            var result = usersList.Where(u => u.IsNewsletterEnabled).ToList();

            if (result == null)
                return new List<UserDto>();
                
            return _mapper.Map<List<User>, List<UserDto>>(result);
        }
    }
}
