namespace Norwind.DTO
{
    public class OrderDetailDTO : BaseModelDTO
    {
        public int OrderDetailId { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public ProductDTO Product { get; set; }
    }
}
