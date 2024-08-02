namespace Domains.Options;

public class ResetCredentialsOptions
{
    public int CodeExpirationTime { get; set; }
    public string ResetPasswordUri { get; set; }
    public string ResetEmailUri { get; set; }
    public string CompanyName { get; set; }

    //With Template Settings
    public string DesktopResetPasswordTemplateId { get; set; }
    public string DesktopResetEmailTemplateId { get; set; }
    public string MobileResetPasswordTemplateId { get; set; }
    public string TempPasswordTemplateId { get; set; }
}
