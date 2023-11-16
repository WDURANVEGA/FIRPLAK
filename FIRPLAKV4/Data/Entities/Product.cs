using System.ComponentModel.DataAnnotations;

namespace FIRPLAKV4.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public ProductType ProductType { get; set; }
    }
}
