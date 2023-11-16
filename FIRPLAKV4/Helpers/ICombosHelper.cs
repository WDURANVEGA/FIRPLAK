using FIRPLAKV4.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FIRPLAKV4.Helpers
{
    public interface ICombosHelper
    {
        public Task<IEnumerable<SelectListItem>> GetComboConveyorsAsync();
        public Task<IEnumerable<SelectListItem>> GetComboUsersAsync();
        public Task<IEnumerable<SelectListItem>> GetComboOrderStatesAsync();
        public Task<IEnumerable<SelectListItem>> GetComboProductsAsync();
    }

    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboConveyorsAsync()
        {
            List<SelectListItem> list = await _context.Conveyors.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
            }).ToListAsync();


            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una transportadora...]",
                Value = "-1"
            });

            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboUsersAsync()
        {
            List<SelectListItem> list = await _context.Users.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.FullName,
            }).ToListAsync();


            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un usuario...]",
                Value = "-1"
            });

            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboOrderStatesAsync()
        {
            List<SelectListItem> list = await _context.OrderStates.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
            }).ToListAsync();


            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un estado de orden...]",
                Value = "-1"
            });

            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboProductsAsync()
        {
            List<SelectListItem> list = await _context.Products.Include(p => p.ProductType).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = $"{c.Name} ({c.ProductType.Name})",
            }).ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un Producto...]",
                Value = "-1"
            });

            return list;
        }
    }
}
