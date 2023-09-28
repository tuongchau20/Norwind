using Norwind.DTO;

namespace Norwind.Services
{
    public interface IOrderService
    {
        public IEnumerable<OrderDTO> GetAllOrders();
        public OrderDTO GetOrderById(int id);
        public bool CreateOrder(OrderDTO order);    
        public bool DeleteOrder(int id);
        public bool UpdateOrder (OrderDTO orderD);
    }
}
