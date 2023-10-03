using Norwind.DTO;

namespace Norwind.Services
{
    public interface ISupplierService
    {
        public IEnumerable<SupplierDTO> GetAllSuppliers();
        public SupplierDTO GetSupplierById(int id);
        public bool CreateSupplier(SupplierDTO supplier);
        public bool DeleteSupplier(int id);
        public bool UpdateSupplier(SupplierDTO supplier);
    }
}
