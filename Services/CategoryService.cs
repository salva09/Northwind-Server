using System;
using System.Linq;
using System.Collections.Generic;
using sample.DataAccess;

class CategoryService : NorthwindService
{
    public Category GetCategoryById(int id)
    {
        if (!Exists(id)) return null;

        var category = DbContext.Categories.First(e => e.CategoryId == id);

        return new Category
        {
            CategoryId = category.CategoryId,
            CategoryName = category.CategoryName,
            Description = category.Description,
            Picture = category.Picture
        };
    }

    public void UpdateCategory(int id, UpdateCategory updateCategory)
    {
        if (!Exists(id)) return;

        var category = DbContext.Categories.First(e => e.CategoryId == id);

        category.Description = updateCategory.Description;
        DbContext.SaveChanges();
    }

    public void AddCategory(NewCategory category)
    {
        DbContext.Categories.Add(new sample.DataAccess.Category
        {
            CategoryName = category.CategoryName,
            Description = category.Description,
            Picture = category.Picture
        });
        DbContext.SaveChanges();
    }

    public void DeleteCategory(int id)
    {
        var category = DbContext.Categories.First(e => e.CategoryId == id);

        var products = DbContext.Products
            .Where(product => product.CategoryId == category.CategoryId).ToList();

        var productService = new ProductService();
        products.ForEach(product => productService.DeleteProduct(product.ProductId));

        DbContext.Categories.Remove(category);
        DbContext.SaveChanges();
    }

    public bool Exists(int id)
    {
        try
        {
            DbContext.Categories.First(e => e.CategoryId == id);
        }
        catch
        {
            return false;
        }
        return true;
    }
}
