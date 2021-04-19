using API.Core.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Core.Specification
{
    public class ProductsWithProductTypeAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithProductTypeAndBrandsSpecification()
        {
            //lazy loadin
            base.AddInclude(x => x.ProductBrand);
            base.AddInclude(x => x.ProductType);
        }

        public ProductsWithProductTypeAndBrandsSpecification(int id):base(x=>x.Id == id)
        {
            base.AddInclude(x => x.ProductBrand);
            base.AddInclude(x => x.ProductType);
        }
    }
}
