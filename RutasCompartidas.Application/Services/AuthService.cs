using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using RutasCompartidas.Domain.Entities;
using RutasCompartidas.Infrastructure.Persistence;

namespace RutasCompartidas.Application.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> LoginAsync(string email, string password)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
    }
}

