using Norwind.DTO;

namespace Norwind.Services
{
    public interface IOrderDetailService
    {
        public IEnumerable<OrderDetailDTO> GetAllOrderDetails();
        public OrderDetailDTO GetOrderDetailsById(int id);
        public bool CreateOrderDetails(OrderDetailDTO OrderDetails);
        public bool DeleteOrderDetails(int id);
        public bool UpdateOrderDetails(OrderDetailDTO orderDetails);
    }
}
