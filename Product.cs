namespace ProductApi;

public class Product
{
    public Guid productId {get; set;}

    public string? Title{get; set;}

    public DateTime? DateCreated { get; set; }

    public int Price{ get; set; }

    public string? Details { get; set; }
}
