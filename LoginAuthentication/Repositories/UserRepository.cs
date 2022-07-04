using AutoMapper;
using LoginAuthentication.Data;
using LoginAuthentication.Dtos;
using LoginAuthentication.Interfaces;
using LoginAuthentication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginAuthentication.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly UserContext context;
        private readonly IMapper mapper;

        public UserRepository(UserContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<User> Create(RegisterDto dto)
        {
            User user = mapper.Map<User>(dto);

            user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            context.Users.Add(user);
            user.Id = await context.SaveChangesAsync();

            return user;
        }

        public async Task<User> GetByEmail(string email)
        {
            User emailUser = await context.Users.FirstOrDefaultAsync(x => x.Email == email);
            return emailUser;
        }

        public async Task<User> GetByIdUser(int idUser)
        {
            User User = await context.Users.FirstOrDefaultAsync(x => x.Id == idUser);
            return User;
        }
    }
}
