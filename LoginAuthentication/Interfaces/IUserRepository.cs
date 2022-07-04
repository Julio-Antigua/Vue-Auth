using LoginAuthentication.Dtos;
using LoginAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginAuthentication.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Create(RegisterDto user);
        Task<User> GetByEmail(string email);
        Task<User> GetByIdUser(int idUser);

    }
}
