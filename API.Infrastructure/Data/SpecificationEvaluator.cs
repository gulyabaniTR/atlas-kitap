using API.Core.DbModels;
using API.Core.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.Infrastructure.Data
{

    //bu classın alacağı entity base entitden türemediler
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {

        //içerisine instance almadan erişmek için method ekliyoruz
        //IQuerable tipinde mathod olucak tipi entitiy olucak, parametleri ise öyle bir parametre olmalı ki specification işlemleri ile eşleşşin
        //çoklu olmalı
        //1. parametre query nin kendisi ikinci parametre ise uygulamamız gereken kriterler
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;

            //criterde where koşulu var mı diye kontrol ediyorum
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            //eğer where yok ise current -> TEntitiy e eşittir ve içindeki includeları dönen Queryable'ı aggragater ile gezerek işliyorum
            //aggreate sadece dizilerde geri gitmek için değil de tüm includeları yakalamak için kullandım
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            return query;
        }
    }
}
