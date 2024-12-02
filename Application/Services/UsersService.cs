using AutoMapper;
using Core.DTOS.Shared;
using Core.DTOS.User;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly RoleManager<ApplicationRole> roleManager;

        public UsersService(
            UserManager<ApplicationUser> userManager,
            IMapper mapper, 
            IUnitOfWork unitOfWork, 
            RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.roleManager = roleManager;
        }




        public async Task<ListWithCountDto<UserDto>> GetAndCountAll(UserQueryDto userQueryDto)
        {
            var spec = userQueryDto.BuildSpecification()
                .WithLimit(userQueryDto.Limit)
                .WithPage(userQueryDto.Page)
                .Build();

            var users = await unitOfWork.Repository<ApplicationUser>().GetAllAsync(spec);
            var count = await unitOfWork.Repository<ApplicationUser>().GetCountAsync(userQueryDto.BuildSpecification().Build());

            return new ListWithCountDto<UserDto>
            {
                Count = count,
                Items = mapper.Map<IEnumerable<UserDto>>(users)
            };
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {

            var user = await GetUser(id);

            var role = await userManager.GetRolesAsync(user);

            var userDto = mapper.Map<UserDto>(user);

            userDto.Roles = role;

            return userDto;

        }

        public async Task<UserDto> UpdateUserAsync(int id, UpdateUserDto updateUserDto)
        {
            var user = await GetUser(id);

            mapper.Map(updateUserDto ,user);

            var result = await userManager.UpdateAsync(user);

            CheckResult(result);

            return mapper.Map<UserDto>(user);


        }

        public async Task DeleteUserAsync(int id)
        {
            ApplicationUser user = await GetUser(id);

            var result = await userManager.DeleteAsync(user);

            CheckResult(result);

            return;
        }

        private async Task<ApplicationUser> GetUser(int id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());

            if (user == null)
                throw new NotFoundException("User not found");

            return user;
        }

        private static void CheckResult(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                var errors = new List<string>();

                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }

                throw new BadRequestException(err: errors);
            }
        }

        private async Task<ApplicationRole> CheckRole(int roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId.ToString());


            if (role == null) throw new NotFoundException("Role does not exist.");

            return role;
        }
    }
}
