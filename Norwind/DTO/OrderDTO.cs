namespace Norwind.DTO
{
    public class OrderDTO : BaseModelDTO
    {
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? ShipperId { get; set; }
        public CustomerDTO Customer { get; set; }
        public EmployeeDTO Employee { get; set; }
        public ShipperDTO Shipper { get; set; }
        public ICollection<OrderDetailDTO> Details { get; set; }
    }
}
