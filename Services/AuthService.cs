using WatchlistApi.Data;
using WatchlistApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace WatchlistApi.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        // Valida email y contraseña del usuario
        public async Task<User?> ValidateUserAsync(string email, string password)
        {
            return await _context.Users
                .AsNoTracking() //Solo lectura
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        //Crear user
        public async Task<User> CreateUserAsync(string email, string password)
        {
            var user = new User
            {
                Email = email,
                Password = password
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}