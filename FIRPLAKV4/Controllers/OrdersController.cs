using FIRPLAKV4.Data;
using FIRPLAKV4.Data.Entities;
using FIRPLAKV4.DTOs;
using FIRPLAKV4.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FIRPLAKV4.Controllers
{
    public class OrdersController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;

        public OrdersController(DataContext context, ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders.Include(o => o.Responsible)
                                             .Include(o => o.OrderState)
                                             .ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            OrderDTO dto = new OrderDTO
            {
                Conveyors = await _combosHelper.GetComboConveyorsAsync(),
                Users = await _combosHelper.GetComboUsersAsync(),
                OrderStates = await _combosHelper.GetComboOrderStatesAsync(),
                Products = await _combosHelper.GetComboProductsAsync(),
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderDTO dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<OrderDetailDTO> detailsDTO = JsonConvert.DeserializeObject<List<OrderDetailDTO>>(dto.Details);

                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            Order order = new Order
                            {
                                Conveyor = await _context.Conveyors.Where(c => c.Id == dto.ConveyorId).FirstOrDefaultAsync(),
                                Responsible = await _context.Users.Where(u => u.Id == dto.ResponsibleId).FirstOrDefaultAsync(),
                                Customer = dto.Customer,
                                DestinationAddress = dto.DestinationAddress,
                                OrderState = await _context.OrderStates.Where(s => s.Name == "Activo").FirstOrDefaultAsync()
                            };

                            await _context.Orders.AddAsync(order);
                            await _context.SaveChangesAsync();

                            foreach(OrderDetailDTO detailDto in detailsDTO)
                            {
                                OrderDetail detail = new OrderDetail
                                {
                                    DeliveryDate = detailDto.DeliveryDate,
                                    Product = await _context.Products.FindAsync(detailDto.ProductId),
                                    Amount = detailDto.Amount,
                                    Order = order
                                };

                                await _context.OrderDetails.AddAsync(detail);
                            }

                            await _context.SaveChangesAsync();

                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            ModelState.AddModelError("Basic", "Error al generar la venta");
                            return View(dto);
                        }
                    }

                    return Redirect(nameof(Index));
                }

                dto.Conveyors = await _combosHelper.GetComboConveyorsAsync();
                dto.Users = await _combosHelper.GetComboUsersAsync();
                dto.OrderStates = await _combosHelper.GetComboOrderStatesAsync();
                dto.Products = await _combosHelper.GetComboProductsAsync();

                return View(dto);
            }
            catch (Exception ex)
            {
                dto.Conveyors = await _combosHelper.GetComboConveyorsAsync();
                dto.Users = await _combosHelper.GetComboUsersAsync();
                dto.OrderStates = await _combosHelper.GetComboOrderStatesAsync();
                dto.Products = await _combosHelper.GetComboProductsAsync();

                ModelState.AddModelError("Exception", ex.Message);

                return View(dto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Dispatch(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            Order? order = await _context.Orders.Include(o => o.Details)
                                                    .ThenInclude(d => d.Product)
                                                        .ThenInclude(p => p.ProductType)
                                                .Include(o => o.Responsible)
                                                .Include(o => o.OrderState)
                                                .Include(o => o.Conveyor)
                                                .FirstOrDefaultAsync(o => o.Id == id);

            if (order is null)
            {
                return NotFound();
            }

            OrderDTO2 dto = new OrderDTO2
            {
                Conveyor = order.Conveyor,
                Customer = order.Customer,
                DestinationAddress = order.DestinationAddress,
                Details = order.Details,
                Id = order.Id,
                OrderStateId = order.OrderState.Id,
                OrderStates = await _combosHelper.GetComboOrderStatesAsync(),
                Responsible = order.Responsible,

                RecievedAt = order.RecievedAt,
                Reciever = order.Reciever,
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Dispatch(OrderDTO2 dto)
        {
            try
            {
                Order? order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == dto.Id);

                if (order is null)
                {
                    return NotFound();
                }

                order.OrderState = await _context.OrderStates.FirstOrDefaultAsync(o => o.Id == dto.OrderStateId);
                order.RecievedAt = dto.RecievedAt;
                order.Reciever = dto.Reciever;

                _context.Orders.Update(order);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                dto.OrderStates = await _combosHelper.GetComboOrderStatesAsync();

                ModelState.AddModelError("Exception", ex.Message);

                return View(dto);
            }
        }

    }
}
