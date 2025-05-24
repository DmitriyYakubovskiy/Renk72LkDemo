using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;
using MimeKit;
using Org.BouncyCastle.Asn1.Ocsp;
using Renk72Lk.Helpers;
using Renk72Lk.Models;
using Renk72Lk.Models.DataBase;
using Renk72Lk.Settings;
using System.Security.Cryptography;
using System.Security.Policy;

namespace Renk72Lk.Services;

public class EmailService : IEmailSerivce
{
    private readonly IRazorViewEngine viewEngine;
    private readonly ITempDataProvider tempDataProvider;
    private readonly IServiceProvider serviceProvider;

    private string smtpHost;
    private int smtpPort;
    private string smtpUser;
    private string smtpPass;
    private string rootEmail;

    public EmailService(IRazorViewEngine viewEngine, ITempDataProvider tempDataProvider, IServiceProvider serviceProvider,
        IOptions<EmailSettings> emailSettings)
    {
        this.viewEngine = viewEngine;
        this.tempDataProvider = tempDataProvider;
        this.serviceProvider = serviceProvider;

        smtpHost = emailSettings.Value.SmtpHost!;
        smtpPort = emailSettings.Value.SmtpPort!;
        smtpUser = emailSettings.Value.SmtpUser!;
        smtpPass = emailSettings.Value.SmtpPass!;
        rootEmail = emailSettings.Value.RootEmail!;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string htmlContent)
    {
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Личный кабинет ООО «РЭНК»", smtpUser));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = subject;

            message.Body = new TextPart("html") { Text = htmlContent };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(smtpHost, smtpPort, true);
                await client.AuthenticateAsync(smtpUser, smtpPass);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw new Exception(ex.Message);
        }
    }

    public async Task NotifyUserAboutCreationBid(IModelMetadataProvider MetadataProvider, ModelStateDictionary ModelState, UserModel user, int bidId, string urlBids, string urlBid)
    {
        try
        {
            var htmlBody = ViewToString.RenderViewToStringAsync(viewEngine, tempDataProvider, serviceProvider, MetadataProvider, ModelState, "Email/MailTemplate", new MailModel()
            {
                Title = $"Здравствуйте, {user?.Surname} {user?.Name} {user?.Patronymic}!",
                SubTitle = new Subtitle()
                {
                    Text = $"Ваша заявка №{bidId} успешно оформлена и будет рассмотрена в ближайшее время.",
                },
                Button = new MailBlockWithLink()
                {
                    Text = "Перейти в мои заявки",
                    Url = urlBids
                },
                AddButton = new MailBlockWithLink()
                {
                    Url = urlBid
                },
                Text = "Спасибо за обращение!",
            });

            await SendEmailAsync(user?.Email!, $"#{bidId} - заявка", await htmlBody);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task NotifyAdminAboutCreationBid(IModelMetadataProvider MetadataProvider, ModelStateDictionary ModelState, UserModel user, int bidId, string urlBids, string urlBid)
    {
        try
        {
            var htmlBody = ViewToString.RenderViewToStringAsync(viewEngine, tempDataProvider, serviceProvider, MetadataProvider, ModelState, "Email/MailTemplate", new MailModel()
            {
                Title = $"Пользователь, {user?.Surname} {user?.Name} {user?.Patronymic}!",
                SubTitle = new Subtitle() { Text = $"Успешно оформил заявку №{bidId}" },
                Button = new MailBlockWithLink()
                {
                    Text = "Перейти в заявки",
                    Url = urlBids
                },
                AddButton = new MailBlockWithLink()
                {
                    Url = urlBid
                }
            });

            await SendEmailAsync(rootEmail, $"#{bidId} - заявка", await htmlBody);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task NotifyUserAboutNewBidStatus(IModelMetadataProvider MetadataProvider, ModelStateDictionary ModelState, UserModel user, int bidId, string bidStatusName, string urlBid)
    {
        try
        {
            var htmlBody = ViewToString.RenderViewToStringAsync(viewEngine, tempDataProvider, serviceProvider, MetadataProvider, ModelState, "Email/MailTemplate", new MailModel()
            {
                Title = $"Здравствуйте, {user?.Surname} {user?.Name} {user?.Patronymic}!",
                SubTitle = new Subtitle()
                {
                    Text = $"У вашей заявки №{bidId}. Новый статус: ",
                    BoldText = bidStatusName
                },
                Button = new MailBlockWithLink()
                {
                    Text = "Перейти к заявке",
                    Url = urlBid
                },
                Text = "С уважением!",
            });

            await SendEmailAsync(user?.Email!, $"#{bidId} - заявка. Новый статус.", await htmlBody);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task NotifyAdminAboutRegistration(IModelMetadataProvider MetadataProvider, ModelStateDictionary ModelState, RegistrationModel model, string url)
    {
        try
        {
            var htmlBody = ViewToString.RenderViewToStringAsync(viewEngine, tempDataProvider, serviceProvider, MetadataProvider, ModelState, "Email/MailTemplate", new MailModel()
            {
                Title = $"Новый пользователь: {model?.Surname} {model?.Name} {model?.Patronymic}!",
                SubTitle = new Subtitle()
                {
                    Text = "Успешно зарегистрирован в личном кабинете ООО «РЭНК»"
                },
                Login = model?.Login,
                Email = model?.Email,
                Telephone = model?.Phone,
                Button = new MailBlockWithLink()
                {
                    Text = "Перейти в личный кабинет",
                    Url = url
                },
                Text = "С уважением!",
            });

            await SendEmailAsync(rootEmail, "Регистрация в личном кабинете", await htmlBody);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task NotifyUserAboutRegistration(IModelMetadataProvider MetadataProvider, ModelStateDictionary ModelState, RegistrationModel model, string url)
    {
        try
        {
            var htmlBody = ViewToString.RenderViewToStringAsync(viewEngine, tempDataProvider, serviceProvider, MetadataProvider, ModelState, "Email/MailTemplate", new MailModel()
            {
                Title = $"Здравствуйте, {model?.Surname} {model?.Name} {model?.Patronymic}!",
                SubTitle = new Subtitle()
                {
                    Text = "Вы успешно зарегистрированы в личном кабинете ООО «РЭНК»"
                },
                Login = model?.Login,
                Email = model?.Email,
                Telephone = model?.Phone,
                Button = new MailBlockWithLink()
                {
                    Text = "Перейти в личный кабинет",
                    Url = url
                },
                Text = "Если это не Вы регистрировались в системе, свяжитесь с администрацией по тел. +7 (3452) 50-08-54.",
            });

            await SendEmailAsync(model?.Email!, "Регистрация в личном кабинете", await htmlBody);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task SendResetPasswordToken(IModelMetadataProvider MetadataProvider, ModelStateDictionary ModelState, UserModel user, string url)
    {
        try
        {
            var htmlBody = ViewToString.RenderViewToStringAsync(viewEngine, tempDataProvider, serviceProvider, MetadataProvider, ModelState, "Email/MailTemplate", new MailModel()
            {
                Title = $"Здравствуйте, {user?.Surname} {user?.Name} {user?.Patronymic}!",
                SubTitle = new Subtitle() { Text = "Мы получили запрос на сброс пароля для Вашей учетной записи. Для восстановления доступа к аккаунту перейдите по ссылке" },
                Login = user.UserName,
                Button = new MailBlockWithLink()
                {
                    Text = "Сбросить пароль",
                    Url = url
                },
                Text = "Если Вы не запрашивали сброс пароля, проигнорируйте данное письмо.",
            });

            await SendEmailAsync(user?.Email!, "Восстановление пароля", await htmlBody);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task Test(IModelMetadataProvider MetadataProvider, ModelStateDictionary ModelState, string url)
    {
        try
        {
            var htmlBody = ViewToString.RenderViewToStringAsync(viewEngine, tempDataProvider, serviceProvider, MetadataProvider, ModelState, "Email/MailTemplate", new MailModel()
            {
                Title = $"Здравствуйте, Админ!",
                SubTitle = new Subtitle() { Text = "Вы получили тестовое сообщение" },
                Button = new MailBlockWithLink()
                {
                    Text = "В лк",
                    Url =url
                },
                Text = "Тест.",
            });

            await SendEmailAsync(rootEmail, "Тест", await htmlBody);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task NotifyAdminAboutNewMessage(IModelMetadataProvider MetadataProvider, ModelStateDictionary ModelState, int bidId, string url)
    {
        try
        {
            var htmlBody = await ViewToString.RenderViewToStringAsync(viewEngine, tempDataProvider, serviceProvider, MetadataProvider, ModelState, "Email/MailTemplate", new MailModel()
            {
                Title = $"Здравствуйте!",
                SubTitle = new Subtitle() { Text = $"В заявке №{bidId} добавлено новое сообщение." },
                Button = new MailBlockWithLink()
                {
                    Text = "Перейти к заявке",
                    Url = url
                },
                Text = "С уважением!",
            });

            await SendEmailAsync(rootEmail!, $"#{bidId} - заявка. Новое сообщение.", htmlBody);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task NotifyUserAboutNewMessage(IModelMetadataProvider MetadataProvider, ModelStateDictionary ModelState, UserModel user, int bidId, string url)
    {
        try
        {
            var htmlBody = await ViewToString.RenderViewToStringAsync(viewEngine, tempDataProvider, serviceProvider, MetadataProvider, ModelState, "Email/MailTemplate", new MailModel()
            {
                Title = $"Здравствуйте, {user?.Surname} {user?.Name} {user?.Patronymic}!",
                SubTitle = new Subtitle()
                {
                    Text = $"В вашей заявка №{bidId} добавлено новое сообщение.",
                },
                Button = new MailBlockWithLink()
                {
                    Text = "Перейти к заявке",
                    Url = url
                },
                Text = "\"С уважением!",
            });

            await SendEmailAsync(user?.Email!, $"#{bidId} - заявка. Новое сообщение!", htmlBody);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
