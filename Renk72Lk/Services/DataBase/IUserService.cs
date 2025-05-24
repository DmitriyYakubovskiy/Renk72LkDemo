using Microsoft.AspNetCore.Identity;
using Renk72Lk.Models;
using Renk72Lk.Models.DataBase;

namespace Renk72Lk.Services.DataBase;

public interface IUserService
{
    Task<ResultModel> ResetPasswordAsync(string email, string token, string password);
    Task<string> GeneratePasswordResetTokenAsync(int userId);
    UserModel[] GetAll();
    Task<UserModel> GetByIdAsync(int id, bool setStories = false);
    Task<UserModel> GetByUserNameAsync(string name, bool setStories = false, bool setAddresses = false);
    Task<UserModel> GetByEmailAsync(string email, bool setStories = false);
    Task<ResultModel> LoginAsync(LoginModel model, string ipAddress);
    Task<ResultModel> RegisterUserAsync(RegistrationModel model, string ipAddress);
    Task LogOutAsync();
    Task<ResultModel> UpdateAsync(UserModel user);
    Task<ResultModel> RemoveAsync(int id);
    Task<(ResultModel, int)> LockAsync(int userIds);
    Task<IList<string>> GetUserRolesAsync(int userId);
    Task<string> GetUserClaimValueAsync(string userName, string claimType);
}