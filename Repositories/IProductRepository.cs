namespace ProductApi.Repository;

public interface IProductRepository {
    // private List<Product>_products;

    public Task<IEnumerable<Product>>GetAll();

    public Task<Product>GetById(Guid id);

    public Task Create(Product _product);

    public Task Update(Product _product);

    public Task Delete(Guid id);
}