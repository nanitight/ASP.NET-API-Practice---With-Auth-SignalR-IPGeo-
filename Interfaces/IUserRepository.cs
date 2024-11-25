﻿using WebApplication1.Models;

namespace RunGroupTUT.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetAllUsersAsync();
        Task<AppUser> GetUserByIdAsync(string id);

        bool Add(AppUser user);
        bool Update(AppUser user);
        bool Delete(AppUser user);

        bool Save();

    }
}
