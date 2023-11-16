using FIRPLAKV4.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FIRPLAKV4.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Cliente")]
        public string Customer { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Dirección de destino")]
        public string DestinationAddress { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una transportadora.")]
        [Display(Name = "Transportadora")]
        public int ConveyorId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        //[Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un responsable.")]
        [Display(Name = "Responsable")]
        public string ResponsibleId { get; set; }

        //[Required(ErrorMessage = "El campo {0} es obligatorio.")]
        //[Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un estado.")]
        [Display(Name = "Estado de Orden")]
        public int? OrderStateId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Productos")]
        public string Details { get; set; }

        public DateTime? RecievedAt { get; set; }


        public IEnumerable<SelectListItem>? Conveyors { get; set; }

        public IEnumerable<SelectListItem>? Users { get; set; }

        public IEnumerable<SelectListItem>? OrderStates { get; set; }

        public IEnumerable<SelectListItem>? Products { get; set; }
    }
}
