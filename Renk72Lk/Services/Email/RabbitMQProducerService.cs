using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Renk72Lk.Helpers;
using Renk72Lk.Models;
using Renk72Lk.Models.DataBase;
using Renk72Lk.Settings;
using System.Text;
using System.Text.Json;

namespace Renk72Lk.Services.Email;

public class RabbitMQProducerService : IRabbitMQProducerSerivce
{
    private readonly IRazorViewEngine viewEngine;
    private readonly IServiceProvider serviceProvider;
    private readonly IModelMetadataProvider metadataProvider;
    private readonly ITempDataProvider tempDataProvider;
    private readonly IConnectionFactory factory;
    private readonly IChannel channel;
    private readonly IConnection connection;
    private readonly ILogger<RabbitMQProducerService> logger;   

    private string rootEmail;
    private string queueName;

    public RabbitMQProducerService(IRazorViewEngine viewEngine, IServiceProvider serviceProvider, IModelMetadataProvider metadataProvider,
        ITempDataProvider tempDataProvider, IOptions<EmailSettings> emailSettings, IOptions<RabbitMQSettings> rabbitSettings, ILogger<RabbitMQProducerService> logger)
    {
        this.viewEngine = viewEngine;
        this.serviceProvider = serviceProvider;
        this.metadataProvider = metadataProvider;
        this.tempDataProvider = tempDataProvider;

        rootEmail = emailSettings.Value.RootEmail!;
        queueName = rabbitSettings.Value.QueueName!;

        factory = new ConnectionFactory()
        {
            HostName = rabbitSettings.Value.Host!,
            UserName = rabbitSettings.Value.UserName!,
            Password = rabbitSettings.Value.Password!,
        };

        connection = factory.CreateConnectionAsync().Result;
        channel = connection.CreateChannelAsync().Result;
        channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        this.logger = logger;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string htmlBody)
    {
        try
        {
            var emailData = new
            {
                ToEmail = toEmail,
                Subject = subject,
                HtmlBody = htmlBody
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(emailData));

            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: queueName, body: body);
        }
        catch (Exception ex)
        {
            logger.LogInformation($"Ошибка при создании письма: {ex.Message}");
        }
    }

    public async Task NotifyUserAboutCreationBid(UserModel user, int bidId, string urlBids, string urlBid)
    {
        try
        {
            var htmlBody = ViewToString.RenderViewToStringAsync(viewEngine, tempDataProvider, serviceProvider, metadataProvider, "Email/MailTemplate", new MailTemplateModel()
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
            logger.LogInformation($"Ошибка при создании письма: {ex.Message}");
        }
    }

    public async Task NotifyAdminAboutCreationBid(UserModel user, int bidId, string urlBids, string urlBid)
    {
        try
        {
            var htmlBody = ViewToString.RenderViewToStringAsync(viewEngine, tempDataProvider, serviceProvider, metadataProvider,"Email/MailTemplate", new MailTemplateModel()
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
            logger.LogInformation($"Ошибка при создании письма: {ex.Message}");
        }
    }

    public async Task NotifyUserAboutNewBidStatus(UserModel user, int bidId, string bidStatusName, string urlBid)
    {
        try
        {
            var htmlBody = ViewToString.RenderViewToStringAsync(viewEngine, tempDataProvider, serviceProvider, metadataProvider, "Email/MailTemplate", new MailTemplateModel()
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
            logger.LogInformation($"Ошибка при создании письма: {ex.Message}");
        }
    }

    public async Task NotifyAdminAboutRegistration(RegistrationModel model, string url)
    {
        try
        {;
            var htmlBody = ViewToString.RenderViewToStringAsync(viewEngine, tempDataProvider, serviceProvider, metadataProvider, "Email/MailTemplate", new MailTemplateModel()
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
            logger.LogInformation($"Ошибка при создании письма: {ex.Message}");
        }
    }

    public async Task NotifyUserAboutRegistration(RegistrationModel model, string url)
    {
        try
        {
            var modelState = new ModelStateDictionary();
            var htmlBody = ViewToString.RenderViewToStringAsync(viewEngine, tempDataProvider, serviceProvider, metadataProvider, "Email/MailTemplate", new MailTemplateModel()
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
            logger.LogInformation($"Ошибка при создании письма: {ex.Message}");
        }
    }

    public async Task SendResetPasswordToken(UserModel user, string url)
    {
        try
        {
            var htmlBody = ViewToString.RenderViewToStringAsync(viewEngine, tempDataProvider, serviceProvider, metadataProvider, "Email/MailTemplate", new MailTemplateModel()
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
            logger.LogInformation($"Ошибка при создании письма: {ex.Message}");
        }
    }

    public async Task Test(string url)
    {
        try
        {
            var htmlBody = ViewToString.RenderViewToStringAsync(viewEngine, tempDataProvider, serviceProvider, metadataProvider, "Email/MailTemplate", new MailTemplateModel()
            {
                Title = $"Здравствуйте, Админ!",
                SubTitle = new Subtitle() { Text = "Вы получили тестовое сообщение" },
                Button = new MailBlockWithLink()
                {
                    Text = "В лк",
                    Url = url
                },
                Text = "Тест.",
            });

            await SendEmailAsync(rootEmail, "Тест", await htmlBody);
        }
        catch (Exception ex)
        {
            logger.LogInformation($"Ошибка при создании письма: {ex.Message}");
        }
    }

    public async Task NotifyAdminAboutNewMessage(int bidId, string url)
    {
        try
        {
            var htmlBody = await ViewToString.RenderViewToStringAsync(viewEngine, tempDataProvider, serviceProvider, metadataProvider, "Email/MailTemplate", new MailTemplateModel()
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
            logger.LogInformation($"Ошибка при создании письма: {ex.Message}");
        }
    }

    public async Task NotifyUserAboutNewMessage(UserModel user, int bidId, string url)
    {
        try
        {
            var htmlBody = ViewToString.RenderViewToStringAsync(viewEngine, tempDataProvider, serviceProvider, metadataProvider, "Email/MailTemplate", new MailTemplateModel()
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

            await SendEmailAsync(user.Email!, $"#{bidId} - заявка. Новое сообщение.", await htmlBody);
        }
        catch (Exception ex)
        {
            logger.LogInformation($"Ошибка при создании письма: {ex.Message}");
        }
    }
}
