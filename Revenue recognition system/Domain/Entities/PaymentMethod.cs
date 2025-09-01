﻿namespace Revenue_recognition_system.Models;

public class PaymentMethod
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}