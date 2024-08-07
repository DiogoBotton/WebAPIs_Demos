﻿namespace Services.DTOs.Results.Addresses;

public class AddressResult
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public string Cep { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string District { get; set; }
    public string Complement { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Reference { get; set; }
    public bool OutsideCity { get; set; }
}
