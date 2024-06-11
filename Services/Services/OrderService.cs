using AutoMapper;
using DataAccess.DataAccess;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Services.Contracts.DataAccess.Base;
using Services.Contracts.Services;
using Services.Models.Order;

namespace Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        /*        private readonly IOrderDataAccess _order;
                private readonly IOrderDetailDataAccess _orderDetail;*/
        private readonly ILogger<OrderService> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public OrderService(IOrderDataAccess order,
            ILogger<OrderService> logger,
            IMapper mapper,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }
        public async Task<OrderForView> GetAllOrders()
        {
            try
            {
                IEnumerable<Order> orders = await _unitOfWork.OrderDataAccess.GetAllOrders();
                IList<OrderForViewItems> items = new List<OrderForViewItems>();

                foreach (var order in orders)
                {
                    var user = await _userManager.FindByIdAsync(order.UserId);
                    var orderItem = _mapper.Map<OrderForViewItems>(order);
                    if (user != null)
                    {
                        orderItem.UserName = user.UserName;
                    }
                    items.Add(orderItem);
                }

                OrderForView response = new OrderForView
                {
                    Orders = items
                };

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(GetAllOrders));
            }
        }

        public async Task<bool> UpdateOrderStatus(OrderForUpdateStatus orderDto, int id)
        {
            try
            {
                Order order = await _unitOfWork.OrderDataAccess.GetOrderById(id);
                if (order != null)
                {
                    _mapper.Map(orderDto, order);
                    _unitOfWork.OrderDataAccess.UpdateOrder(order);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(UpdateOrderStatus));
            }
        }
        public async Task<bool> UpdateOrder(OrderForUpdate orderDto, int id)
        {
            try
            {
                Order order = await _unitOfWork.OrderDataAccess.GetOrderById(id);
                if (order != null)
                {
                    // list product trong Db
                    order.OrderDetails = order.OrderDetails ?? new List<OrderDetail>();
                    orderDto.OrderDetails = orderDto.OrderDetails ?? new List<OrderDetailForUpdate>();

                    var existingOrderDetails = order.OrderDetails;
                    var updatedOrderDetail = orderDto.OrderDetails;
                    var orderDetailsToRemove = existingOrderDetails.Where(pc => !updatedOrderDetail.Any(upc => upc.ProductId == pc.ProductId)).ToList();

                    foreach (var orderDetail in orderDetailsToRemove)
                    {
                        order.OrderDetails.Remove(orderDetail);
                    }
                    foreach (var orderDeatilDto in updatedOrderDetail)
                    {
                        var existingOrderDetail = order.OrderDetails.FirstOrDefault(pc => pc.ProductId == orderDeatilDto.ProductId);
                        if (existingOrderDetail != null)
                        {
                            existingOrderDetail.Quantity = orderDeatilDto.Quantity;
                        }
                        else
                        {
                            var newOrderDetail = _mapper.Map<OrderDetail>(orderDeatilDto);
                            order.OrderDetails.Add(newOrderDetail);
                        }
                    }
                    //order.OrderDetail = _mapper.Map<ICollection<OrderDetail>>(orderDto.OrderDetail);
                    _mapper.Map(orderDto, order);
                    _unitOfWork.OrderDataAccess.UpdateOrder(order);
                    await _unitOfWork.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(UpdateOrder));
            }
        }

        public async Task<bool> AddOrder(OrderForCreate orderDto)
        {
            try
            {
                Order order = _mapper.Map<Order>(orderDto);
                order.OrderDetails = _mapper.Map<ICollection<OrderDetail>>(orderDto.OrderDetails);
                await _unitOfWork.OrderDataAccess.AddOrder(order);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(AddOrder));
            }
        }

        public async Task<OrderForViewItems> GetOrderById(int id)
        {
            try
            {
                Order order = await _unitOfWork.OrderDataAccess.GetOrderById(id);
                if (order != null)
                {
                    var user = await _userManager.FindByIdAsync(order.UserId);
                    OrderForViewItems items = _mapper.Map<OrderForViewItems>(order);
                    if (user != null)
                    {
                        items.UserName = user.UserName;
                        items.UserId = user.Id;
                    }
                    return items;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(GetOrderById));
            }
        }
    }
}
