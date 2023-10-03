
using Norwind.DTO;
using Norwind.Helpers;
using Norwind.Models;
using Norwind.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Norwind.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenerictRepository<Product> _generictRepository;
        private readonly ILoggerManager _logger;

        public ProductService(IGenerictRepository<Product> generictRepository, ILoggerManager logger)
        {
            _generictRepository = generictRepository;
            _logger = logger;
        }

        public bool CreateProduct(ProductDTO product)
        {
            try
            {
                _logger.LogInfo("ProductService: CreateProduct");

                var newProduct = new Product
                {
                    ProductName = product.ProductName,
                    SupplierId = product.SupplierId,
                    CategoryId = product.CategoryId,
                    Unit = product.Unit,
                    Price = product.Price
                };

                _generictRepository.Create(newProduct);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("ProductService: CreateProduct" + ex);
                return false;
            }
        }

        public bool DeleteProduct(int id)
        {
            try
            {
                _logger.LogInfo("ProductService: DeleteProduct");
                _generictRepository.DeleteById(id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("ProductService: DeleteProduct" + ex);
                return false;
            }
        }

        public IEnumerable<ProductDTO> GetAllProducts()
        {
            var products = _generictRepository.GetAll().Select(product => new ProductDTO
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                SupplierId = product.SupplierId,
                CategoryId = product.CategoryId,
                Unit = product.Unit,
                Price = product.Price
            }).ToList();

            return products;
        }

        public ProductDTO GetProductById(int id)
        {
            var product = _generictRepository.GetById(id);
            return new ProductDTO
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                SupplierId = product.SupplierId,
                CategoryId = product.CategoryId,
                Unit = product.Unit,
                Price = product.Price
            };
        }

        public bool UpdateProduct(ProductDTO product)
        {
            try
            {
                _logger.LogInfo("ProductService: UpdateProduct");

                var updateProduct = new Product
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    SupplierId = product.SupplierId,
                    CategoryId = product.CategoryId,
                    Unit = product.Unit,
                    Price = product.Price
                };

                _generictRepository.Update(updateProduct);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("ProductService: UpdateProduct" + ex);
                return false;
            }
        }
    }
}
