using AutoMapper;
using DataAccess.DataAccess;
using Domain.Entities;
using EllipticCurve.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Services.Contracts.Services;
using Services.Models.Order;

namespace Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderDataAccess _order;
        private readonly ILogger<OrderService> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public OrderService(IOrderDataAccess order,
            ILogger<OrderService> logger,
            IMapper mapper,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _order = order;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<OrderForView> GetAllOrders()
        {
            try
            {
                IEnumerable<Order> orders = await _order.GetAllOrders();
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
        /// <summary>
        /// Order Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public Task<OrderForViewItems> GetOrderById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
