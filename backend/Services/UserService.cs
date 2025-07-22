using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<string?> RegisterUser(RegisterDto dto)
        {
            var existingUser = await _usersCollection.Find(u => u.Email == dto.Email).FirstOrDefaultAsync();
            if (existingUser != null)
                return null;

            var newUser = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role 
            };

            await _usersCollection.InsertOneAsync(newUser);
            return _jwtService.GenerateToken(newUser);
        }

        public async Task<string?> Login(LoginDto dto)
        {
            var user = await _usersCollection.Find(u => u.Email == dto.Email).FirstOrDefaultAsync();
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return null;

            return _jwtService.GenerateToken(user);
        }

        // public async Task<User?> GetById(string id)
        // {
        //     return await _usersCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
        // }
    }
}