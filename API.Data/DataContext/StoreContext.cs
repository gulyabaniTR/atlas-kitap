using API.Data.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Data.DataContext
{
    public class StoreContext : DbContext
    {
        //class oluşturulken ctor oluşturulur ve context olduğu belirtmek için DbContext olarak options belirtiliril.
        public StoreContext(DbContextOptions options) : base(options)
        {
        }

        //contextin databasede eşleşmesi için DbSet olarak belirtilmelidir
        public DbSet<Product> Products { get; set; }
    }
}
