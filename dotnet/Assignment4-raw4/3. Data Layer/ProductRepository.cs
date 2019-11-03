using _0._Models;
using _2._Data_Layer_Abstraction;
using _3._Data_Layer.Database_Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3._Data_Layer
{
    public class ProductRepository : IProductRepository
    {
        private NorthwindContext databaseContext;

        public ProductRepository(NorthwindContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public Product Create(string name, string description)
        {
            var nextId = databaseContext.Products.Max(x => x.Id) + 1;

            var product = new Product
            {
                Id = nextId,
                Name = name,
            };

            databaseContext.Products.Add(product);

            databaseContext.SaveChanges();

            return product;
        }

        public bool Delete(int id)
        {
            if (databaseContext.Products.Find(id) != null)
            {
                databaseContext.Products.Remove(databaseContext.Products.Find(id));
                databaseContext.SaveChanges();

                return true;
            }
            return false;
        }

        public IEnumerable<Product> GetByCategoryId(int categoryId)
        {
            var productById = databaseContext.Products.Include("Category").Where(p => p.CategoryId == categoryId).ToList();
            foreach (var p in productById)
            {
                // Fixing swedish special characters encoding issue: "ö"
                // Inspiration from here: https://stackoverflow.com/a/13845414/9332260
                var iso = Encoding.GetEncoding("ISO-8859-1");
                var name = Encoding.UTF8.GetString(iso.GetBytes(p.Name));
                p.Name = name;
            }
            return productById;
        }

        public IEnumerable<Product> GetByContainedSubstringInName(string substring)
        {
            var productBySubstring = databaseContext.Products.Where(p => p.Name.Contains(substring));
            // Read more: https://github.com/dotnet/corefx/issues/17356#issuecomment-288237167
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            foreach (var p in productBySubstring)
            {
                // Fixing german special characters encoding issue: "ß"
                var iso = Encoding.GetEncoding(1252);
                var name = Encoding.UTF8.GetString(iso.GetBytes(p.Name));
                p.Name = name;
            }
            return productBySubstring;
        }

        public Product GetById(int id)
        {
            return databaseContext.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
        }

    }
}