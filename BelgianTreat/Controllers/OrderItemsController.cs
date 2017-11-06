using AutoMapper;
using BelgianTreat.Data;
using BelgianTreat.Data.Entities;
using BelgianTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelgianTreat.Controllers
{
    [Route("/api/orders/{orderid}/items")]
    public class OrderItemsController : Controller
    {
        private readonly IBelgianRepository _repository;
        private readonly ILogger<OrderItemsController> _logger;
        private readonly IMapper _mapper;

        public OrderItemsController(IBelgianRepository repository,
          ILogger<OrderItemsController> logger,
          IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        // /api/orders/1/items
        [HttpGet]
        public IActionResult Get(int orderId)
        {
            var order = _repository.GetOrderById(orderId);
            if (order != null)
            {
                return Ok(_mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items));
            }

            return NotFound();
        }

        // /api/orders/1/items/1
        [HttpGet("{id}")]
        public IActionResult Get(int orderId, int id)
        {
            var order = _repository.GetOrderById(orderId);
            if (order != null)
            {
                var item = order.Items.Where(i => i.Id == id).FirstOrDefault();
                if (item != null)
                {
                    return Ok(_mapper.Map<OrderItem, OrderItemViewModel>(item));
                }
            }
            return NotFound();

        }
    }
}
