using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace API.Core.Specification
{
    //Navigtionj Propertyler için Specifitacion Class'ı
    public interface ISpecification<T>
    {

        //delegate tipleriyle fonksiyon tanımlama max 17 adet 17 den fazla kriter varsa array veya aggregate ile yapmamız lazım
        Expression<Func<T,bool>> Criteria { get;}
        List<Expression<Func<T,object>>> Includes { get; }
        Expression<Func<T,object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnable { get; }
    }
}
