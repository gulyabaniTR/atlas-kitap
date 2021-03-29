using API.Core.DbModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.Interfaces
{
    public interface IProductRepository
    {

        //dependency injection ile kavramları soyutlaştırıyoruz ve somut bağlılıklar bırakmıyoruz
        Task<Product> GetProductByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetProductsAsync();
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
    }
}
