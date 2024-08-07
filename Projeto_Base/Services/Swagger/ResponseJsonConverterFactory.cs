﻿using Services.DTOs;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Services.Swagger;

public class ResponseJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert == typeof(Response);
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        if(typeToConvert != typeof(Response))
            throw new NotSupportedException($"Cannot create converter for {typeToConvert}");

        return new ResponseJsonConverter();
    }
}
