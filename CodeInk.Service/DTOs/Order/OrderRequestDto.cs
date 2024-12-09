﻿namespace CodeInk.Service.DTOs.Order;
public class OrderRequestDto
{
    public ShippingAddressDto ShippingAddress { get; set; }
    public int DeliveryMethodId { get; set; }
    public IEnumerable<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
}
