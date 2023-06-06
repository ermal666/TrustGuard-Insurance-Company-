﻿namespace TrustGuard.Domain.Models;

public class Offer
{
    public int OfferId { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }

    public int InsuranceId { get; set; }
    public Insurance Insurance { get; set; }
}