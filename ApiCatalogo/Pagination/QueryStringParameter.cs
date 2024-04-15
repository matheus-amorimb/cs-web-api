namespace ApiCatalogo.Parameters;

public abstract class QueryStringParameter
{
    const int MaxPageSize = 50;
    public int PageNumber { get; set; } = 1;

    private int _pageSize = MaxPageSize;
    public int PageSize
    {
        get => _pageSize;
            
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
}