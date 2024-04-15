namespace ApiCatalogo.Parameters;

    public class ProductsParameter
    { 
        const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        private int _pageSize;
        public int PageSize
        {
            get => _pageSize;
            
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        
    }