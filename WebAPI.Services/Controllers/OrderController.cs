using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts.Services;
using Services.Models.Order;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<AppUser> _userManager;
        public OrderController(IOrderService orderService, UserManager<AppUser> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }
        // GET: api/<OrderController>
        [HttpGet]
        public async Task<IActionResult> Get()
        { 
            OrderForView combos = await _orderService.GetAllOrders();
            return Ok(combos);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderForCreate orderDto)
        {
            var combo = await _orderService.AddOrder(orderDto);
            return Ok(combo);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var combo = await _orderService.GetOrderById(id);
            return Ok(combo);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromBody] OrderForUpdateStatus orderDto, int id)
        {
            var combo = await _orderService.UpdateOrderStatus(orderDto, id);
            return Ok(combo);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] OrderForUpdate orderDto, int id)
        {
            var combo = await _orderService.UpdateOrder(orderDto, id);
            return Ok(combo);
        }
    }
}
