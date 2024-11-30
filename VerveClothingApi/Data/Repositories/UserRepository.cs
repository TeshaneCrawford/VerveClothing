using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VerveClothingApi.DTOs;
using VerveClothingApi.Entities;
using VerveClothingApi.Interfaces;

namespace VerveClothingApi.Data.Repositories
{
    public class UserRepository(ApplicationDbContext context, IMapper mapper) : IUserRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user != null ? _mapper.Map<UserDto>(user) : null;
        }

        public async Task<UserDto?> GetByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user != null ? _mapper.Map<UserDto>(user) : null;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> CreateAsync(CreateUserDto createUserDto)
        {
            if (createUserDto == null)
                throw new ArgumentNullException(nameof(createUserDto));

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == createUserDto.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Email already exists");
            }

            try
            {
                var user = _mapper.Map<User>(createUserDto);
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password, BCrypt.Net.BCrypt.GenerateSalt(12));
                user.CreatedAt = DateTime.UtcNow;
                user.UpdatedAt = DateTime.UtcNow;

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return _mapper.Map<UserDto>(user);
            }
            catch (BCrypt.Net.SaltParseException ex)
            {
                throw new InvalidOperationException("Error hashing password", ex);
            }
        }

        public async Task<UserDto?> UpdateAsync(int id, UpdateUserDto updateUserDto)
        {
            if (updateUserDto == null)
                throw new ArgumentNullException(nameof(updateUserDto));

            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            try
            {
                _mapper.Map(updateUserDto, user);
                user.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                return _mapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error updating user", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
