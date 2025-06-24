using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<bool> IsEmailTaken(string email)
        {
            return await _usersCollection.Find(u => u.Email == email).AnyAsync();
        }

        public async Task<string?> Register(RegisterDto dto)
        {
            if (await IsEmailTaken(dto.Email))
                return null;

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var newUser = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = hashedPassword,
                Role = "user"
            };

            await _usersCollection.InsertOneAsync(newUser);

            return _jwtService.GenerateToken(newUser); // Retorna el token JWT directamente
        }

        public async Task<string?> SignIn(LoginDto dto)
        {
            var user = await _usersCollection.Find(u => u.Email == dto.Email).FirstOrDefaultAsync();
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, dto.Password))
                throw new Exception("Credenciales inválidas");

            return _jwtService.GenerateToken(user);
        }

        public async Task<User?> GetById(string id)
        {
            return await _usersCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
        }
    }
}