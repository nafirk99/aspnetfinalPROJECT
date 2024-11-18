using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Services
{
    public class ProductManagementServices : IProductManagementServices
    {
        private readonly IInventoryUnitOfWork _productUnitOfWork;

        public ProductManagementServices(IInventoryUnitOfWork productUnitOfWork)
        {
            _productUnitOfWork = productUnitOfWork;
        }
        

        public void CreateProduct(Product product)
        {
           _productUnitOfWork.ProductRepository.Add(product);
            _productUnitOfWork.Save();
        }
    }
}
