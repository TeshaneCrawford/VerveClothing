using VerveClothingApi.DTOs;
using VerveClothingApi.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace VerveClothingApi.Services
{
    public interface IUserService
    {
        Task<UserDto> GetUserByIdAsync(int id);
        Task<UserDto> GetUserByEmailAsync(string email);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> CreateUserAsync(CreateUserDto createUserDto);
        Task<UserDto> UpdateUserAsync(int id, UpdateUserDto updateUserDto);
        Task<bool> DeleteUserAsync(int id);
    }

    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found");
            return user;
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty");

            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
                throw new KeyNotFoundException($"User with email {email} not found");
            return user;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            ValidateCreateUserDto(createUserDto);
            return await _userRepository.CreateAsync(createUserDto);
        }

        public async Task<UserDto> UpdateUserAsync(int id, UpdateUserDto updateUserDto)
        {
            ValidateUpdateUserDto(updateUserDto);
            var user = await _userRepository.UpdateAsync(id, updateUserDto);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found");
            return user;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteAsync(id);
        }

        private static void ValidateCreateUserDto(CreateUserDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new ValidationException("Email is required");
            if (string.IsNullOrWhiteSpace(dto.Password))
                throw new ValidationException("Password is required");
            if (string.IsNullOrWhiteSpace(dto.FirstName))
                throw new ValidationException("First name is required");
            if (string.IsNullOrWhiteSpace(dto.LastName))
                throw new ValidationException("Last name is required");
        }

        private static void ValidateUpdateUserDto(UpdateUserDto dto)
        {
            if (dto.FirstName != null && string.IsNullOrWhiteSpace(dto.FirstName))
                throw new ValidationException("First name cannot be empty");
            if (dto.LastName != null && string.IsNullOrWhiteSpace(dto.LastName))
                throw new ValidationException("Last name cannot be empty");
        }
    }
}
