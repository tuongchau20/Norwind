using Norwind.DTO;

namespace Norwind.Services
{
    public interface IProductService
    {
        public IEnumerable<ProductDTO> GetAllProducts();
        public ProductDTO GetProductById(int id);
        public bool CreateProduct(ProductDTO product);
        public bool DeleteProduct(int id);
        public bool UpdateProduct(ProductDTO product);
    }
}
