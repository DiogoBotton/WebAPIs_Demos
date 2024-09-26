using FluentValidation.Results;
using Services.DTOs;
using System.Net;

namespace Services.Extensions;

public static class ValidationExtensions
{
    public static Error ErrorResponse(
        this ValidationResult validationResult, 
        string name = "Validation_Error", 
        string description = "Um ou mais erros ocorreram na validação.")
    {
        var fieldErrors = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToList());

        return new Error(name, description, fieldErrors);
    }
}
