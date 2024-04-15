namespace ApiCatalogo.Parameters;

public class ProductsFilterPrice : QueryStringParameter
{
    public decimal? Price { get; set; }
    public string? PriceCriteria { get; set; }
}