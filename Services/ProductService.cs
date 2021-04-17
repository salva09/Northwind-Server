using System;
using System.Linq;
using sample.DataAccess;

class ProductService : NorthwindService
{
    public Product GetProductbyId(int id)
    {
        if (!Exists(id)) return null;

        var product = DbContext.Products.First(e => e.ProductId == id);

        return new Product
        {
            id = product.ProductId,
            ProductName = product.ProductName,
            QuantityPerUnit = product.QuantityPerUnit,
            Discontinued = product.Discontinued
        };
    }

    public void UpdateProduct(int id, UpdateProduct updateProduct)
    {
        if (!Exists(id)) return;

        var product = DbContext.Products.First(e => e.ProductId == id);

        product.Discontinued = updateProduct.Discontinued;
        DbContext.SaveChanges();
    }

    public void AddProduct(NewProduct product)
    {
        DbContext.Products.Add(new sample.DataAccess.Product
        {
            ProductName = product.ProductName,
            QuantityPerUnit = product.QuantityPerUnit,
            Discontinued = product.Discontinued
        });
        DbContext.SaveChanges();
    }

    public void DeleteProduct(int id)
    {
        if (!Exists(id)) return;

        var product = DbContext.Products.First(e => e.ProductId == id);
        var orderDetails = DbContext.OrderDetails.Where(p => p.ProductId == product.ProductId).ToList();

        orderDetails.ForEach(orderDetail => DbContext.Remove(orderDetail));

        DbContext.Products.Remove(product);
        DbContext.SaveChanges();
    }

    public bool Exists(int id)
    {
        try
        {
            DbContext.Products.First(e => e.ProductId == id);
        }
        catch
        {
            return false;
        }
        return true;
    }
}
