using Core.DTOS.Shared;
using Core.DTOS.User;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IUsersService
    {
        Task<ListWithCountDto<UserDto>> GetAndCountAll(UserQueryDto userQueryDto);
        Task<UserDto> GetUserByIdAsync(int id);
        Task<UserDto> UpdateUserAsync(int id , UpdateUserDto updateUserDto);
        Task DeleteUserAsync(int id );
    }
}
