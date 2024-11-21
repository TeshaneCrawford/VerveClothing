using VerveClothingApi.DTOs;

namespace VerveClothingApi.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDto> GetByIdAsync(int id);
        Task<UserDto> GetByEmailAsync(string email);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> CreateAsync(CreateUserDto createUserDto);
        Task<UserDto> UpdateAsync(int id, UpdateUserDto updateUserDto);
        Task<bool> DeleteAsync(int id);
    }
}
