using MySqlConnector;

namespace ProductApi.Repository;

public class ProductRepository:IProductRepository{
    private readonly IConfiguration _config;

    public ProductRepository(IConfiguration config){
        _config=config;
    }

   
    public async Task<IEnumerable<Product>>GetAll(){
        var connectionString = _config["ConnectionStrings:ProductDB"];
        var connection = new MySqlConnection(connectionString);
        List<Product> products = new List<Product>();
        try{
            await connection.OpenAsync();
            using(var command = new MySqlCommand("Select * from Product;", connection)){
                using(var reader = await command.ExecuteReaderAsync()){
                    while(await reader.ReadAsync()){
                        Product product = new Product();
                        product.productId = reader.GetFieldValue<Guid>(reader.GetOrdinal("productId"));
                        product.Title = reader.GetFieldValue<string>(reader.GetOrdinal("Title"));
                        product.Price = reader.GetFieldValue<int>(reader.GetOrdinal("price"));
                        // product.DateCreated = reader.GetFieldValue<DateTime?>(reader.GetOrdinal("dateCreated"));
                        product.Details = reader.GetFieldValue<string?>(reader.GetOrdinal("details"));
                        products.Add(product);
                    }
                }
            }
        }catch(Exception ex){
            await connection.CloseAsync();
            throw ex;
        }
        await connection.CloseAsync();
        return products;
    }

    public async Task<Product>GetById(Guid id){
        var connectionString = _config["ConnectionStrings:ProductDB"];
        var connection = new MySqlConnection(connectionString);
        Product product = new Product();
        try{
        await connection.OpenAsync();
        using(var command = new MySqlCommand($"SELECT * FROM Product p WHERE p.productId = @productId;", connection)){
            command.Parameters.AddWithValue("@productId", id);
            using(var reader = await command.ExecuteReaderAsync()){
                // while(reader.Read()){
                    // try{
                        await reader.ReadAsync();
                        product.productId = reader.GetFieldValue<Guid>(reader.GetOrdinal("productId"));
                        product.Title = reader.GetFieldValue<string>(reader.GetOrdinal("Title"));
                        product.Price = reader.GetFieldValue<int>(reader.GetOrdinal("price"));
                        // product.DateCreated = reader.GetFieldValue<DateTime?>(reader.GetOrdinal("dateCreated"));
                        product.Details = reader.GetFieldValue<string?>(reader.GetOrdinal("details"));
                    
                // }
            }
        }}catch(Exception ex){
            await connection.CloseAsync();
            throw ex;
        }
        await connection.CloseAsync();
        return product;
    }

    public async Task<Product>Create(Product _product){
        var connectionString = _config["ConnectionStrings:ProductDB"];
        var connection = new MySqlConnection(connectionString);
        try{
            await connection.OpenAsync();
            var query = @$"INSERT INTO Product(productId, title, price, details) VALUES(@productId,@Title,@Price,@Details);"; 
            var command = new MySqlCommand(query, connection);
            // Product product = new Product();
            using(command){
                command.Parameters.AddWithValue("@productId", _product.productId);
                command.Parameters.AddWithValue("@Title", _product.Title);
                command.Parameters.AddWithValue("@Price", _product.Price);
                command.Parameters.AddWithValue("@Details", _product.Details);
                // try{
                    await command.ExecuteNonQueryAsync();   
            } 
        }catch(Exception ex){
            await connection.CloseAsync();
            throw ex;
        }
        await connection.CloseAsync();
        return _product;
    }

    public async Task<Product>Update(Product _product){
        var connectionString = _config["ConnectionStrings:ProductDB"];
        var connection = new MySqlConnection(connectionString);
        try{
            await connection.OpenAsync();
            var query = @$"UPDATE Product p SET title=@Title, price=@Price, details=@Details WHERE p.productId = @productId"; 
            var command = new MySqlCommand(query, connection);
            // Product product = new Product();
            using(command){
                command.Parameters.AddWithValue("@Title", _product.Title);
                command.Parameters.AddWithValue("@Price", _product.Price);
                command.Parameters.AddWithValue("@Details", _product.Details);
                command.Parameters.AddWithValue("@productId", _product.productId);
                // try{
                    await command.ExecuteNonQueryAsync();
            }
        }catch(Exception ex){
            await connection.CloseAsync();
            throw ex;
        }
        await connection.CloseAsync();
        return _product;
    }

    public async Task Delete(Guid id){
        var connectionString = _config["ConnectionStrings:ProductDB"];
        var connection = new MySqlConnection(connectionString);
        try{
            await connection.OpenAsync();
            var query = @$"DELETE FROM Product p WHERE p.productId = @productId;"; 
            var command = new MySqlCommand(query, connection);
            // Product product = new Product();
            using(command){
                command.Parameters.AddWithValue("@productId", id);
                await command.ExecuteNonQueryAsync();
            }
        }catch(Exception ex){
            await connection.CloseAsync();
            throw ex;
        }
        await connection.CloseAsync();
    }    
}