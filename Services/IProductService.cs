namespace ProductApi.Services;

public interface IProductService {
    // private List<Product>_products;

    public Task<IEnumerable<Product>>GetAll();

    public Task<Product>GetById(Guid id);

    public Task<Product>Create(Product _product);

    public Task<Product>Update(Product _product);

    public Task Delete(Guid id);
}