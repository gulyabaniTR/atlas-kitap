using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace API.Core.Specification
{
    //Navigtionj Propertyler için Specifitacion Class'ı
    public interface ISpecification<T>
    {
        Expression<Func<T,bool>> Criteria { get;}
        List<Expression<Func<T,object>>> Includes { get; }
    }
}
