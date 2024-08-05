using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace Services.DTOs.Filters;

public class GuidFilter
{
    [BindNever]
    [JsonIgnore]
    public Guid Id { get; set; }
}
