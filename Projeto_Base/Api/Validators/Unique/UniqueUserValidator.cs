using FluentValidation;
using FluentValidation.Validators;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Api.Validators.Unique;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class UniqueUserValidator<T> : AsyncPropertyValidator<T, string>
{
    private readonly ApiDbContext apiDbContext;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="apiDbContext"></param>
    public UniqueUserValidator(ApiDbContext apiDbContext)
    {
        this.apiDbContext = apiDbContext;
    }

    /// <summary>
    /// 
    /// </summary>
    public override string Name => nameof(UniqueUserValidator<T>);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="errorCode"></param>
    /// <returns></returns>
    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return "Um usuário com este email já existe.";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="value"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async override Task<bool> IsValidAsync(ValidationContext<T> context, string value, CancellationToken cancellationToken)
    {
        return !await apiDbContext.Users.AnyAsync(d => d.Email == value.ToLower(), cancellationToken);
    }
}
