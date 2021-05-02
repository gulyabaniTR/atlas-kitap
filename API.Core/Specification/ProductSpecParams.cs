using System;
using System.Collections.Generic;
using System.Text;

namespace API.Core.Specification
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;

        private int _pageSize = 9;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string Sort { get; set; }
        public int? AuthorId { get; set; }


        //search encapsulation
        private string _search;

        public string Search {
            get { return _search; }
            set
            {
                _search = value.ToLower();
            } 
        }
    }
}
