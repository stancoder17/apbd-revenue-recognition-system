﻿namespace Revenue_recognition_system.Domain.Entities;

public class IndividualClient : Client
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Pesel { get; set; } = null!;
    public bool IsDeleted { get; set; }
}