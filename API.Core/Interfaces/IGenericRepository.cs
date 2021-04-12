using API.Core.DbModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.Interfaces
{
    //Repository Pattern Generic Hale Getirilirken <T> (TYPE) parametersi verilir ve içine verilebilecek değerleri de 'where' anahtar kelimesi ile belirtilir.
    //Bu interface sadece BaseEntityden türeyen değeler veya new() lebilen sınıflarla çalışır
    public interface IGenericRepository<T> where T:BaseEntity
    {
        Task<T> GetByIdAsync(int id);

        //IReadOnlyList sadece okuma işlemi yapılması için kullanılacak
        Task<IReadOnlyList<T>> ListAllAsync();
    }
}
