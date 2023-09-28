    using Norwind.DTO;

    namespace Norwind.Services
    {
        public interface ICustomerService
        {
            public IEnumerable<CustomerDTO> GetAllCustomers();
            public CustomerDTO GetCustomerById(int id);
            public bool CreateCustomer(CustomerDTO customer);
            public bool UpdateCustomer(CustomerDTO customer);
            public bool DeleteCustomer(int id);

        }
    }
