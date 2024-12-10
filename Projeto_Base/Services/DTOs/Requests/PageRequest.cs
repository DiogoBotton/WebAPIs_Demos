using Microsoft.AspNetCore.Mvc.ModelBinding;
using Services.Paginator.Enums;
using System.Text.Json.Serialization;

namespace Services.DTOs.Requests;

public abstract class PageRequest
{
    [JsonIgnore]
    [BindNever]
    public virtual int MaxPageSize { get; set; } = 50;

    private int _pageSize = 30;

    public int Page { get; set; } = 1;
    public int PageSize { 
        get => _pageSize; 
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
    public SortDirection SortDirection { get; set; }

    public string SortingProperty { get; set; }
}