using Norwind.DTO;

namespace Norwind.Services
{
    public interface IShipperService
    {
        public IEnumerable<ShipperDTO> GetAllShipper();
        public ShipperDTO GetShipperById(int id);
        public bool CreateShipper(ShipperDTO order);
        public bool DeleteShipper(int id);
        public bool UpdateShipper(ShipperDTO orderD);
    }
}
