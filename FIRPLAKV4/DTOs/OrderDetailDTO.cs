using FIRPLAKV4.Data.Entities;

namespace FIRPLAKV4.DTOs
{
    public class OrderDetailDTO
    {

        public int ProductId { get; set; }

        public int Amount { get; set; }

        public DateTime DeliveryDate { get; set; }
    }
}
