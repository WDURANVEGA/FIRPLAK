using FIRPLAKV4.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FIRPLAKV4.DTOs
{
    public class OrderDTO2
    {
        [Display(Name = "Consecutivo")]
        public int Id { get; set; }

        [Display(Name = "Cliente")]
        public string Customer { get; set; }

        [Display(Name = "Dirección de destino")]
        public string DestinationAddress { get; set; }

        [Display(Name = "Transportadora")]
        public Conveyor Conveyor { get; set; }

        [Display(Name = "Responsable")]
        public User Responsible { get; set; }

        [Display(Name = "Detalles")]
        public IEnumerable<OrderDetail> Details { get; set; }

        [Display(Name = "Fecha de recibido")]
        public DateTime? RecievedAt { get; set; }

        [Display(Name = "Quien recibió")]
        public string? Reciever { get; set; }


        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un estado.")]
        [Display(Name = "Estado de Orden")]
        public int? OrderStateId { get; set; }

        public IEnumerable<SelectListItem> OrderStates { get; set; }
    }
}
