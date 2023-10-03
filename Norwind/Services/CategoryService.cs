using Norwind.DTO;
using Norwind.Helpers;
using Norwind.Models;
using Norwind.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Norwind.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenerictRepository<Category> _generictRepository;
        private readonly ILoggerManager _logger;

        public CategoryService(IGenerictRepository<Category> generictRepository, ILoggerManager logger)
        {
            _generictRepository = generictRepository;
            _logger = logger;
        }

        public bool CreateCategory(CategoryDTO category)
        {
            try
            {
                _logger.LogInfo("CategoryService: CreateCategory");

                var newCategory = new Category
                {
                    CategoryName = category.CategoryName,
                    Description = category.Description
                };

                _generictRepository.Create(newCategory);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("CategoryService: CreateCategory" + ex);
                return false;
            }
        }

        public bool DeleteCategory(int id)
        {
            try
            {
                _logger.LogInfo("CategoryService: DeleteCategory");
                _generictRepository.DeleteById(id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("CategoryService: DeleteCategory" + ex);
                return false;
            }
        }

        public IEnumerable<CategoryDTO> GetAllCategories()
        {
            var categories = _generictRepository.GetAll().Select(category => new CategoryDTO
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Description = category.Description
            }).ToList();

            return categories;
        }

        public CategoryDTO GetCategoryById(int id)
        {
            var category = _generictRepository.GetById(id);
            return new CategoryDTO
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Description = category.Description
            };
        }

        public bool UpdateCategory(CategoryDTO category)
        {
            try
            {
                _logger.LogInfo("CategoryService: UpdateCategory");

                var updateCategory = new Category
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName,
                    Description = category.Description
                };

                _generictRepository.Update(updateCategory);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("CategoryService: UpdateCategory" + ex);
                return false;
            }
        }
    }
}
