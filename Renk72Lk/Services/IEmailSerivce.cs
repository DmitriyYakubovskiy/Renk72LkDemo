using Microsoft.AspNetCore.Mvc.ModelBinding;
using Renk72Lk.Models;
using Renk72Lk.Models.DataBase;

namespace Renk72Lk.Services;

public interface IEmailSerivce
{
    Task SendEmailAsync(string toEmail, string subject, string htmlContent);
    Task NotifyUserAboutCreationBid(IModelMetadataProvider MetadataProvider, ModelStateDictionary ModelState, UserModel user, int bidId, string urlBids, string urlBid);
    Task NotifyAdminAboutCreationBid(IModelMetadataProvider MetadataProvider, ModelStateDictionary ModelState, UserModel user, int bidId, string urlBids, string urlBid);
    Task NotifyUserAboutNewBidStatus(IModelMetadataProvider MetadataProvider, ModelStateDictionary ModelState, UserModel user, int bidId, string bidStatusName, string urlBid);
    Task NotifyAdminAboutRegistration(IModelMetadataProvider MetadataProvider, ModelStateDictionary ModelState, RegistrationModel model, string url);
    Task NotifyUserAboutRegistration(IModelMetadataProvider MetadataProvider, ModelStateDictionary ModelState, RegistrationModel model, string url);
    Task SendResetPasswordToken(IModelMetadataProvider MetadataProvider, ModelStateDictionary ModelState, UserModel user, string url);
    Task Test(IModelMetadataProvider MetadataProvider, ModelStateDictionary ModelState, string url);
    Task NotifyAdminAboutNewMessage(IModelMetadataProvider MetadataProvider, ModelStateDictionary ModelState, int bidId, string url);
    Task NotifyUserAboutNewMessage(IModelMetadataProvider MetadataProvider, ModelStateDictionary ModelState, UserModel user, int bidId, string url);
}