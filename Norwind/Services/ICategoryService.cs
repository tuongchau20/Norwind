using Norwind.DTO;

namespace Norwind.Services
{
    public interface ICategoryService
    {
        public IEnumerable<CategoryDTO> GetAllCategories();
        public CategoryDTO GetCategoryById(int id);
        public bool CreateCategory(CategoryDTO categoryDTO);    
        public bool DeleteCategory(int id);
        public bool UpdateCategory (CategoryDTO categoryDTO);
    }
}
