using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos;
using backend.Enums;
using DentalClinicApi.Contexts;
using DentalClinicApi.Dtos;
using DentalClinicApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using MongoDB.Driver;

namespace DentalClinicApi.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _usersCollection;
        private readonly JwtService _jwtService;

        public UserService(MongoDbContext context, JwtService jwtService)
        {
            _usersCollection = context.Users;
            _jwtService = jwtService;
        }

        public async Task<LoginResponseDto?> RegisterUser(RegisterDto dto)
        {
            var user = await _usersCollection.Find(u => u.Email == dto.Email).FirstOrDefaultAsync();
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return null;

            var token = _jwtService.GenerateToken(user);

            return new LoginResponseDto
            {
                Token = token,
                Name = dto.Name,
                Email = dto.Email,
                Role = user.Role.ToString()
            };
        }

        public async Task<string?> Login(LoginDto dto)
        {
            var user = await _usersCollection.Find(u => u.Email == dto.Email).FirstOrDefaultAsync();
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return null;

            return _jwtService.GenerateToken(user);
        }
    }
}