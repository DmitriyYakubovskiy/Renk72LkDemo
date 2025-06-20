using Renk72Lk.Models;
using Renk72Lk.Models.DataBase;

namespace Renk72Lk.Services.Email;

public interface IRabbitMQProducerSerivce
{
    Task SendEmailAsync(string toEmail, string subject, string htmlContent);
    Task NotifyUserAboutCreationBid(UserModel user, int bidId, string urlBids, string urlBid);
    Task NotifyAdminAboutCreationBid(UserModel user, int bidId, string urlBids, string urlBid);
    Task NotifyUserAboutNewBidStatus(UserModel user, int bidId, string bidStatusName, string urlBid);
    Task NotifyAdminAboutRegistration(RegistrationModel model, string url);
    Task NotifyUserAboutRegistration(RegistrationModel model, string url);
    Task SendResetPasswordToken(UserModel user, string url);
    Task Test(string url);
    Task NotifyAdminAboutNewMessage(int bidId, string url);
    Task NotifyUserAboutNewMessage(UserModel user, int bidId, string url);
}