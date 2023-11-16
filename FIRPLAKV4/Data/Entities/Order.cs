using System.ComponentModel.DataAnnotations;

namespace FIRPLAKV4.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Customer { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string DestinationAddress { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Conveyor Conveyor { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public User Responsible { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public OrderState OrderState { get; set; }

        public IEnumerable<OrderDetail> Details { get; set; }

        public DateTime? RecievedAt { get; set; }

        public string? Reciever { get; set; }
    }
}
