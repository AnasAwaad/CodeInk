﻿namespace CodeInk.Repository.Models;
public class BasketItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public string PictureUrl { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
