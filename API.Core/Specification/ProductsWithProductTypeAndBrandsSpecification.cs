using API.Core.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Core.Specification
{
    public class ProductsWithProductTypeAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithProductTypeAndBrandsSpecification(ProductSpecParams productSpecParams)
            :base(x=>
            (string.IsNullOrWhiteSpace(productSpecParams.Search)|| x.Name.ToLower().Contains(productSpecParams.Search))
            &&
            (!productSpecParams.BrandId.HasValue || x.ProductBrandId == productSpecParams.BrandId)
            &&
            (!productSpecParams.TypeId.HasValue || x.ProductTypeId == productSpecParams.TypeId)
            )
        {
            //lazy loadin
            base.AddInclude(x => x.ProductBrand);
            base.AddInclude(x => x.ProductType);
            //base.AddOrderBy(x => x.Name);

            ApplyPaging(productSpecParams.PageSize * (productSpecParams.PageIndex - 1),productSpecParams.PageSize);

            if (!string.IsNullOrWhiteSpace(productSpecParams.Sort))
            {
                switch (productSpecParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(x => x.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(x => x.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }
        }

        public ProductsWithProductTypeAndBrandsSpecification(int id):base(x=>x.Id == id)
        {
            base.AddInclude(x => x.ProductBrand);
            base.AddInclude(x => x.ProductType);
        }
    }
}
