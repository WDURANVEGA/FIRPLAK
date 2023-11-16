using System.ComponentModel.DataAnnotations;

namespace FIRPLAKV4.Data.Entities
{
    public class OrderState
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }
    }
}
