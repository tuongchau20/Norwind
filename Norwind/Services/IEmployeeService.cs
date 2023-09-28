using Norwind.DTO;

namespace Norwind.Services
{
    public interface IEmployeeService
    {
        public IEnumerable<EmployeeDTO> GetAllEmployee();
        public EmployeeDTO GetEmployeeById(int id);
        public bool CreateEmployee (EmployeeDTO employee);
        public bool DeleteEmployee(int id);
        public bool UpdateEmployee(EmployeeDTO employee);
    }
}
