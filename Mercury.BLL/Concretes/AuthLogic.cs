using AutoMapper;
using Mercury.BLL.Dtos;
using Mercury.BLL.Intefaces;
using Mercury.DAL.Entities;
using Mercury.DAL.Repository;
using Mercury.DAL.Repository.Interfaces;
using Mercury.Infrastructure.Interfaces;
using Mercury.Infrastructure.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.BLL.Concretes
{
    public class AuthLogic : IAuthLogic
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Infrastructure.Interfaces.IAuthService _authService;

        public AuthLogic(Infrastructure.Interfaces.IAuthService authService, IUserRepository userRepository,
            IMapper mapper, IUnitOfWork unitOfWork)
        {
            _authService = authService;
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDto> SignInUser(UserCredentialsDto credentials)
        {
            var user = await _userRepository
                .GetWithThrow(u => u.Email == credentials.Email);

            var encryptedPassword = _mapper.Map<User, Password>(user);

            _authService.VerifyPasswordHash(credentials.Password, encryptedPassword);

            var mappedUser = _mapper.Map<User, UserDto>(user);
            //mappedUser.Token = _authService.GenerateToken(user.Id);

            return mappedUser;
        }

        public async Task<UserDto> GetUserByEmail(string email)
        {
            var user = await _userRepository
                .Get(u => u.Email == email);

            return _mapper.Map<User, UserDto>(user);
        }

        public async Task<bool> ValidateUser(long id, string password)
        {
            var user = await _userRepository
                .GetWithThrow(u => u.Id == id);

            var encryptedPassword = _mapper.Map<User, Password>(user);

            var isValid = _authService.VerifyPasswordHash(password, encryptedPassword);

            return isValid;

        }

        public async Task<UserDto> SignUpPlayer(SignUpDto credentials)
        {
            var user = await _userRepository
                .Get(u => u.Email == credentials.Email);

            if (user != null)
            {
                throw new InvalidOperationException();
            }

            var encryptedPassword = _authService.CreatePasswordHash(credentials.Password);

            user = new User
            {
                Name = credentials.Name,
                Email = credentials.Email,
                PasswordHash = encryptedPassword.PasswordHash,
                PasswordSalt = encryptedPassword.PasswordSalt
            };

            _userRepository.Add(user);
            await _unitOfWork.CompleteAsync();

            var mappedUser = _mapper.Map<User, UserDto>(user);
            //mappedUser.Token = _authService.GenerateToken(user.Id);

            return mappedUser;        
        }
    }
}
