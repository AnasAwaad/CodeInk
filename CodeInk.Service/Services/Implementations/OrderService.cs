using AutoMapper;
using CodeInk.Core.Entities.OrderEntities;
using CodeInk.Core.Exceptions;
using CodeInk.Core.Repositories;
using CodeInk.Core.Service;
using CodeInk.Service.DTOs.Order;
using CodeInk.Service.Services.Interfaces;

namespace CodeInk.Service.Services.Implementations;
public class OrderService : IOrderService
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<DeliveryMethod> _deliverMethod;
    private readonly IGenericRepository<Order> _orderRepo;
    private readonly IBookService _bookService;

    public OrderService(IMapper mapper, IGenericRepository<DeliveryMethod> deliverMethod, IBookService bookService, IGenericRepository<Order> orderRepo)
    {
        _mapper = mapper;
        _deliverMethod = deliverMethod;
        _bookService = bookService;
        _orderRepo = orderRepo;
    }
    public async Task<OrderResultDto> CreateOrderAsync(OrderRequestDto orderRquest, string userEmail)
    {
        #region Get delivery method

        var deliveryMethod = await _deliverMethod.GetByIdAsync(orderRquest.DeliveryMethodId)
                            ?? throw new DeliveryMethodNotFoundException(orderRquest.DeliveryMethodId);
        #endregion

        #region Fill order item list with items and calc subTotal

        var orderItems = new List<OrderItem>();
        decimal subTotal = 0;
        foreach (var item in orderRquest.OrderItems)
        {
            var bookFromDb = await _bookService.GetBookByIdAsync(item.BookId)
                            ?? throw new BookNotFoundException(item.BookId);


            var orderItem = new OrderItem(item.BookId,
                                          item.BookName,
                                          item.BookPictureUrl,
                                          item.Quantity,
                                          item.Quantity * bookFromDb.Price);

            subTotal += orderItem.Price;

            orderItems.Add(orderItem);
        }

        #endregion

        #region Todo : Payment
        #endregion

        #region Create Order

        var address = _mapper.Map<Address>(orderRquest.ShippingAddress);
        var order = new Order(userEmail, address, deliveryMethod, orderItems, subTotal);
        await _orderRepo.CreateAsync(order);

        #endregion

        return _mapper.Map<OrderResultDto>(order);
    }
}
