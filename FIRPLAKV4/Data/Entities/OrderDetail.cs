using System.ComponentModel.DataAnnotations;

namespace FIRPLAKV4.Data.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Product Product { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime DeliveryDate { get; set; }

        public Order Order { get; set; }
    }
}
