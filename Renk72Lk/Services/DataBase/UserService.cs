using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Renk72Lk.DataAccess.Entities;
using Renk72Lk.DataAccess.Enums;
using Renk72Lk.DataAccess.Extensions;
using Renk72Lk.Models;
using Renk72Lk.Models.DataBase;
using System.Security.Claims;

namespace Renk72Lk.Services.DataBase;

public class UserService : IUserService
{
    private readonly SignInManager<UserEntity> signInManager;
    private readonly RoleManager<IdentityRole<int>> roleManager;
    private readonly IAuthHistoryService authHistoryService;
    private readonly IFileService attachmentFileService;
    private readonly IAddressService addressService;
    private readonly IMapper mapper;

    public UserService(SignInManager<UserEntity> signInManager, RoleManager<IdentityRole<int>> roleManager, 
        IAuthHistoryService authStoryService, IFileService attachmentFileService,
        IAddressService addressService, IMapper mapper)
    {
        this.signInManager = signInManager;
        this.roleManager = roleManager;
        this.authHistoryService = authStoryService;
        this.attachmentFileService = attachmentFileService;
        this.addressService = addressService;
        this.mapper = mapper;
    }

    public async Task<ResultModel> ResetPasswordAsync(string email, string token, string password)
    {
        var user = await signInManager.UserManager.FindByEmailAsync(email);
        if (user == null) return new ResultModel(false);
        var isTokenValid = await signInManager.UserManager.VerifyUserTokenAsync(user!, "Default", "ResetPassword", token);
        if (!isTokenValid)
        {
            return new ResultModel(false).AddErrors(["Неверный токен"]);
        }

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        user.UpdatedAt = DateTime.Now;
        var result = await signInManager.UserManager.UpdateAsync(user);

        return new ResultModel(true);
    }

    public async Task<string> GeneratePasswordResetTokenAsync(int userId)
    {
        var user = await signInManager.UserManager.FindByIdAsync(userId.ToString());
        return await signInManager.UserManager.GeneratePasswordResetTokenAsync(user!);
    }

    public async Task<ResultModel> RegisterUserAsync(RegistrationModel model, string ipAddress)
    {
        try
        {
            await LogOutAsync();
            
            var resultUserName = await signInManager.UserManager.FindByNameAsync(model.Login);
            var resultEmail = await signInManager.UserManager.FindByEmailAsync(model.Email);
            if (resultEmail != null) return new ResultModel(false).AddErrors(["Аккаунт с такой почтой уже существует"], nameof(model.Email));
            if (resultUserName != null) return new ResultModel(false).AddErrors(["Аккаунт с таким логином уже существует"], nameof(model.Login));

            var user = new UserEntity()
            {
                UserName = model.Login,
                Email = model.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Name = model.Name,
                Surname = model.Surname,
                Patronymic = model.Patronymic,
                PhoneNumber = model.Phone,
                Snils = model.Snils,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            if (model.UserDataAgreementFormFile == null) return new ResultModel(false).AddErrors(["Не загружен файл"], nameof(model.UserDataAgreementFormFile));

            user.UserDataAgreementFileId = (await attachmentFileService.CreateUserDataAgreementFileAsync(model.UserDataAgreementFormFile)).Id;
            
            var result = await signInManager.UserManager.CreateAsync(user);

            if (result.Succeeded)
            {
                user = await signInManager.UserManager.FindByEmailAsync(model.Email);
                await signInManager.UserManager.AddToRoleAsync(user!, UserRole.User.GetDescription());    
                await signInManager.UserManager.AddClaimAsync(user!, new Claim(UserType.UserType.GetDescription(), UserType.NaturalPerson.GetDescription()));
                await LoginAsync(new LoginModel() { Login = user.UserName!, Password = model.Password }, ipAddress);

                return new ResultModel(true);
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    Console.WriteLine(item.Description);
                }
                return new ResultModel(false).AddErrors(["Ошибка. Попробуйте позже"], nameof(model.Login));
            }
        }   
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new ResultModel(false).AddErrors([$"Ошибка: {ex.Message}. Попробуйте позже"], nameof(model.Login)); 
        }
    }

    public UserModel[] GetAll()
    {
        return mapper.Map<UserModel[]>(signInManager.UserManager.Users.ToArray());
    }

    public async Task<UserModel> GetByEmailAsync(string email, bool setStories = false)
    {
        var user = mapper.Map<UserModel>(await signInManager.UserManager.FindByEmailAsync(email));
        if (setStories) user.Stories = mapper.Map<List<AuthHistoryModel>>(authHistoryService.GetAll(user.Id, 5));
        return user;
    }

    public async Task<UserModel> GetByIdAsync(int id, bool setStories = false)
    {
        var user = mapper.Map<UserModel>(await signInManager.UserManager.FindByIdAsync(id.ToString()));
        if (setStories) user.Stories = mapper.Map<List<AuthHistoryModel>>(authHistoryService.GetAll(user.Id, 5));
        return user;
    }

    public async Task<UserModel> GetByUserNameAsync(string userName, bool setStories = false, bool setAddresses = false)
    {
        var user = mapper.Map<UserModel>(await signInManager.UserManager.FindByNameAsync(userName));

        if (setStories) user.Stories = mapper.Map<List<AuthHistoryModel>>(authHistoryService.GetAll(user.Id, 5));
        if (setAddresses)
        {
            if(user.ActualAddressId != null) user.ActualAddress = mapper.Map<AddressModel>(addressService.GetById(user.ActualAddressId!.Value));
            if (user.RegistrationAddressId != null) user.RegistrationAddress = mapper.Map<AddressModel>(addressService.GetById(user.RegistrationAddressId!.Value));
        }

        return user;
    }

    public async Task<ResultModel> LoginAsync(LoginModel model, string ipAddress)
    {
        try
        {
            if (string.IsNullOrEmpty(model.Login)) return new ResultModel(false).AddErrors(["Введите логин"], nameof(model.Login));
            if (string.IsNullOrEmpty(model.Password)) return new ResultModel(false).AddErrors(["Введите пароль"], nameof(model.Password));

            var user = await signInManager.UserManager.FindByNameAsync(model.Login);
            if (user == null)
            {
                user = await signInManager.UserManager.FindByEmailAsync(model.Login!);
                if(user==null) return new ResultModel(false).AddErrors(["Неверный логин или пароль"], nameof(model.Login));
            }

            var resultPassword = BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash);
            if (!resultPassword) return new ResultModel(false).AddErrors(["Неверный логин или пароль"], nameof(model.Login));

            await LogOutAsync();
            await signInManager.SignInAsync(user, true);
            await authHistoryService.CreateAsync(new AuthHistoryModel { LoginDateTime = DateTime.Now, UserId = user.Id, LoginIp = ipAddress });
            return new ResultModel(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new ResultModel(false).AddErrors([$"Ошибка: {ex.Message}. Попробуйте позже"], nameof(model.Login));
        }
    }

    public async Task LogOutAsync()
    {
        await signInManager.SignOutAsync();
    }

    public async Task<ResultModel> UpdateAsync(UserModel model)
    {
        try
        {
            var entity = await signInManager.UserManager.FindByNameAsync(model.UserName!);
            if (entity == null) new ResultModel(false).AddErrors(["Ошибка. Некорректный пользователь"]);
            if (!string.IsNullOrEmpty(model.Password)) entity!.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
            entity!.PhoneNumber = model.PhoneNumber;
            entity.Name = model.Name;
            entity.Surname = model.Surname;
            entity.Patronymic = model.Patronymic;
            entity.Snils = model.Snils;
            entity.DateOfBirth = model.DateOfBirth;
            entity.PlaceOfBirth = model.PlaceOfBirth;
            entity.PassportType = model.PassportType;
            entity.PassportSeries = model.PassportSeries;
            entity.PassportNumber = model.PassportNumber;
            entity.PassportDate = model.PassportDate;
            entity.PassportIssuedBy = model.PassportIssuedBy;
            if(entity.ActualAddressId == null)
            {
                var id = await addressService.CreateAsync(model.ActualAddress);
                entity.ActualAddressId = id;
            }
            else
            {
                model.ActualAddress.Id = entity.ActualAddressId!.Value;
                addressService.Update(model.ActualAddress);
            }
            if (entity.RegistrationAddressId == null)
            {
                var id = await addressService.CreateAsync(model.RegistrationAddress);
                entity.RegistrationAddressId = id;
            }
            else
            {
                model.RegistrationAddress.Id = entity.RegistrationAddressId!.Value;
                addressService.Update(model.RegistrationAddress);
            }
            entity.UpdatedAt = DateTime.Now;

            await signInManager.UserManager.UpdateAsync(entity);
            return new ResultModel(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new ResultModel(false).AddErrors([$"Ошибка: {ex.Message}"], nameof(model.UserName));
        }
    }

    public async Task<ResultModel> RemoveAsync(int id)
    {
        var user = await signInManager.UserManager.FindByIdAsync(id.ToString());
        if (user == null) return new ResultModel(false);

        var result = await signInManager.UserManager.DeleteAsync(user);
        
        if (result.Succeeded) return new ResultModel(true);
        else return new ResultModel(false);
    }

    public async Task<(ResultModel, int)> LockAsync(int userId)
    {
        try
        {
            var user = await signInManager.UserManager.FindByIdAsync(userId.ToString());
            if (user == null) return (new ResultModel(false).AddErrors(["Некорректный пользователь"]), 0);
            if(!roleManager.RoleExistsAsync("Бан").Result) return (new ResultModel(false).AddErrors(["Некорректная роль"]), 0);
            //TODO
            //if (roleManager.)
            //{
            //    user.Status = 1;
            //    await signInManager.UserManager.AddToRoleAsync(user, "Бан");
            //    await signInManager.UserManager.UpdateSecurityStampAsync(user);
            //    await signInManager.UserManager.UpdateAsync(user);
            //}
            //else
            //{
            //    user.Status = 0;
            //    await signInManager.UserManager.RemoveFromRoleAsync(user, "Бан");
            //    await signInManager.UserManager.UpdateSecurityStampAsync(user);
            //    await signInManager.UserManager.UpdateAsync(user);
            //}

            return (new ResultModel(true), 1);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            return (new ResultModel(false).AddErrors([ex.Message]), 0);
        }
    }

    public async Task<IList<string>> GetUserRolesAsync(int userId)
    {
        var user = await signInManager.UserManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return new List<string>();
        }

        return await signInManager.UserManager.GetRolesAsync(user);
    }

    public async Task<string> GetUserClaimValueAsync(string userName, string claimType)
    {
        var user = await signInManager.UserManager.FindByNameAsync(userName);
        if (user == null)
        {
            return string.Empty;
        }

        var claims = await signInManager.UserManager.GetClaimsAsync(user);
        var claim = claims.FirstOrDefault(c => c.Type == claimType);

        return claim?.Value ?? string.Empty;
    }
}
