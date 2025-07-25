using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Renk72Lk.Models;
using Renk72Lk.Settings;
using System.Text;
using System.Text.Json;

namespace Renk72Lk.Services.Email;

public class RabbitMQConsumerService : BackgroundService
{
    private readonly IConnectionFactory factory;
    private readonly ILogger<RabbitMQConsumerService> logger;

    private string queueName;
    private string smtpHost;
    private int smtpPort;
    private string smtpUser;
    private string smtpPass;

    public RabbitMQConsumerService(IOptions<EmailSettings> emailSettings, IOptions<RabbitMQSettings> rabbitSettings, ILogger<RabbitMQConsumerService> logger)
    {
        queueName = rabbitSettings.Value.QueueName!;
        smtpHost = emailSettings.Value.SmtpHost!;
        smtpPort = emailSettings.Value.SmtpPort!;
        smtpUser = emailSettings.Value.SmtpUser!;
        smtpPass = emailSettings.Value.SmtpPass!;

        this.logger = logger;
        factory = new ConnectionFactory()
        {
            HostName = rabbitSettings.Value.Host!,
            UserName = rabbitSettings.Value.UserName!,
            Password = rabbitSettings.Value.Password!,
        };
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var json = Encoding.UTF8.GetString(body);
            var mailData = JsonSerializer.Deserialize<MailDataModel>(json);

            if (mailData != null) await SendEmailAsync(mailData.ToEmail!, mailData.Subject!, mailData.HtmlBody!);

            await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
        };

        await channel.BasicConsumeAsync(queueName, autoAck: false, consumer: consumer);

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }

    private async Task SendEmailAsync(string toEmail, string subject, string htmlContent)
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
            logger.LogError($"Ошибка отправки почтового сообщения(to {toEmail}): {ex.Message}");
        }
    }
}
