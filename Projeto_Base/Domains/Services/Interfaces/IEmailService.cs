using Domains.Services.Email;

namespace Domains.Services.Interfaces;

/// <summary>
/// 
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mail"></param>
    /// <param name="templateId"></param>
    /// <param name="templateData"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<bool> SendTemplateEmail(EmailMessage mail, string templateId, object templateData, CancellationToken cancellationToken);
}