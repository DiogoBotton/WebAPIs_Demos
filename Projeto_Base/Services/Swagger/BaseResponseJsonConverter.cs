﻿using Services.DTOs;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Services.Swagger;

public class BaseResponseJsonConverter<T> : JsonConverter<BaseResponse<T>>
{
    public override BaseResponse<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(ref reader, options);
        if (errorResponse != null && !string.IsNullOrEmpty(errorResponse.Name))
        {
            return new BaseResponse<T>(errorResponse);
        }

        var result = JsonSerializer.Deserialize<T>(ref reader, options);
        return new BaseResponse<T>(result);
    }

    public override void Write(Utf8JsonWriter writer, BaseResponse<T> value, JsonSerializerOptions options)
    {
        if (value.Error != null)
        {
            JsonSerializer.Serialize(writer, value.Error, options);
        }
        else
        {
            JsonSerializer.Serialize(writer, value.Result, options);
        }
    }
}
