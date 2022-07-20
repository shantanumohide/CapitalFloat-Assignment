using MySqlConnector;
using ProductApi.Repository;
using System;

namespace ProductApi.Services;

public class ProductService:IProductService{
    // private List<Product> _products = new List<Product>{
    //     new Product{
    //         productId=Guid.NewGuid(),
    //         Title="Adidas Sports Shoe",
    //         Price=25,
    //         Details=""
    //     },
    //     new Product{
    //         productId=Guid.NewGuid(),
    //         Title="Jockey T-shirt",
    //         Price=10,
    //         Details=""
    //     }
    // };

    private readonly IProductRepository _productRepository;
    private readonly IConfiguration _config;

    public ProductService(IProductRepository productRepository, IConfiguration config){
        _productRepository = productRepository;
        _config = config;
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        IEnumerable<Product> _products = new List<Product>();
        try{
            _products = await _productRepository.GetAll();
        }catch(Exception ex){
            Console.WriteLine(ex);
            throw ex;
        }
        return _products;
    }

    public async Task<Product>GetById(Guid productId)
    {
        var product = new Product();
        try{
            product = await _productRepository.GetById(productId);
        }catch(Exception ex){
            Console.WriteLine(ex);
            throw ex;
        }        
        return product;
    }

    public async Task Create(Product _product){
        var product = new Product();
        try{
            await _productRepository.Create(_product);
        }catch(Exception ex){
            Console.WriteLine(ex);
            throw ex;
        }
    }

    public async Task Update(Product _product){
        var product = new Product();        
        try{
            await _productRepository.Update(_product);
        }catch(Exception ex){
            Console.WriteLine(ex);
            throw ex;
        }
    }

    public async Task Delete(Guid productId){
        try{
            await _productRepository.Delete(productId);
        }catch(Exception ex){
            Console.WriteLine(ex);
            throw ex;
        }
    } 
}