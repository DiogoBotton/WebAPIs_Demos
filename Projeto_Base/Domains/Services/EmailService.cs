using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text;
using Domains.Services.Interfaces;
using Domains.Services.Email;
using Domains.Options;

namespace Domains.Services;

/// <summary>
/// 
/// </summary>
public class EmailService : IEmailService
{
    private readonly EmailOptions options;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="optionsSnapshot"></param>
    public EmailService(IOptionsSnapshot<EmailOptions> optionsSnapshot)
    {
        options = optionsSnapshot.Value;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mail"></param>
    /// <param name="templateId"></param>
    /// <param name="templateData"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> SendTemplateEmail(EmailMessage mail, string templateId, object templateData, CancellationToken cancellationToken)
    {
        return await TrySendEmail(mail, templateId, templateData, cancellationToken);
    }

    private async Task<bool> TrySendEmail(EmailMessage mail, string templateId, object templateData, CancellationToken cancellationToken)
    {
        try
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, options.BrevoSendUrl);
            request.Headers.Add("api-key", options.Key);

            EmailTemplate emailTemplate = SetEmail(mail, templateId, templateData);
            var json = JsonSerializer.Serialize(emailTemplate);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
                return false;

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    private EmailTemplate SetEmail(EmailMessage mail, string templateId, object templateData)
    {
        var MessageVersionList = new List<MessageVersion>
        {
            new MessageVersion()
            {
                To = new List<ToSend>
                {
                    new ToSend { Email = mail.To }
                }
            }
        };

        EmailTemplate emailTemplate = new EmailTemplate
        {
            Sender = new SenderSend
            {
                Name = options.SenderName,
                Email = options.EmailSender
            },

            MessageVersions = MessageVersionList,
            Subject = mail.Subject,
            TemplateId = Convert.ToInt32(templateId),
            Params = templateData
        };

        return emailTemplate;
    }
}
