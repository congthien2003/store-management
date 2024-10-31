using Microsoft.EntityFrameworkCore;
using StoreManagement.Domain.IRepositories;
using StoreManagement.Domain.Models;
using StoreManagement.Infrastructure.Data;
using System.Linq.Expressions;

namespace StoreManagement.Infrastructure.Repositories
{
    internal class ProductSellRepository : IProductSellRepository<ProductSell>
    {
        private readonly DataContext _dataContext;

        public ProductSellRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<ProductSell> CreateAsync(ProductSell productSell)
        {
            var food = await _dataContext.Foods.FirstOrDefaultAsync(x => x.Id == productSell.FoodId && x.IsDeleted == false);
            if (food == null)
            {
                throw new NullReferenceException("Đồ ăn không tồn tại");
            }
            var newProductSell = await _dataContext.ProductSells.AddAsync(productSell);
            await _dataContext.SaveChangesAsync();
            return newProductSell.Entity;
        }

        public async Task<ProductSell> DeleteAsync(int id, bool incluDeleted = false)
        {
            var productSell = await _dataContext.ProductSells.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == incluDeleted);
            if(productSell == null)
            {
                throw new NullReferenceException("Không tìm thấy sản phẩm");
            }
            _dataContext.ProductSells.Remove(productSell);
            await _dataContext.SaveChangesAsync();
            return productSell;
        }

        public async Task<List<ProductSell>> GetAll(int currentPage = 1, int pageSize = 5, string searchTerm = "", string sortCol = "", bool ascSort = true, bool incluDeleted = false)
        {
            var product = _dataContext.ProductSells.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                product = product.Where(t => t.Food.Name.Contains(searchTerm));
            }
            if (!incluDeleted)
            {
                product = product.Where(t => t.IsDeleted == incluDeleted);
            }
            if (!string.IsNullOrEmpty(sortCol))
            {
                if (ascSort)
                {
                    product = product.OrderByDescending(GetSortColumnExpression(sortCol.ToLower()));
                }
                else
                {
                    product = product.OrderBy(GetSortColumnExpression(sortCol.ToLower()));

                }
            }
            var list = await product.Skip(currentPage * pageSize - pageSize).Take(pageSize).ToListAsync();
            return list;
        }

        public async Task<ProductSell> GetByFoodIdAsync(int foodId)
        {
            return await _dataContext.ProductSells.FirstOrDefaultAsync(ps => ps.FoodId == foodId);
        }

        public async Task<ProductSell> GetByIdAsync(int id, bool incluDeleted = false)
        {
            var productSell = await _dataContext.ProductSells.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == incluDeleted);
            if (productSell == null)
            {
                throw new NullReferenceException("Không tìm thấy sản phẩm");
            }
            return productSell;
        }

        public async Task<ProductSell> UpdateAsync(int id, ProductSell productSell, bool incluDeleted = false)
        {
            var existingProductSell = await _dataContext.ProductSells
       .Include(p => p.Food) 
       .FirstOrDefaultAsync(p => p.Id == id); 

            if (existingProductSell == null)
            {
                throw new KeyNotFoundException("Sản phẩm không tồn tại.");
            }
            existingProductSell.Quantity = productSell.Quantity;
            existingProductSell.UpdatedAt = DateTime.UtcNow; 

            _dataContext.ProductSells.Update(existingProductSell);
            await _dataContext.SaveChangesAsync();

            return existingProductSell; 
        }
        public Expression<Func<ProductSell, object>> GetSortColumnExpression(string sortColumn)
        {
            switch (sortColumn)
            {
                case "name":
                    return x => x.Food.Name;
                case "UpdateAt":
                    return x => x.UpdatedAt;
                default:
                    return x => x.Id;
            }
        }
        public async Task<int> CountAsync(string searchTerm)
        {
            var product = _dataContext.ProductSells.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                product = product.Where(t => t.Food.Name.Contains(searchTerm));
            }

            return await product.CountAsync();
        }
        public async Task<ProductSell> UpdateQuantityAsync(int id, int quantity)
        {
            var existingProductSell = await _dataContext.ProductSells
                .Include(p => p.Food)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (existingProductSell == null)
            {
                throw new KeyNotFoundException("Sản phẩm không tồn tại.");
            }

            existingProductSell.Quantity += quantity;
            existingProductSell.UpdatedAt = DateTime.UtcNow;

            _dataContext.ProductSells.Update(existingProductSell);
            await _dataContext.SaveChangesAsync();

            return existingProductSell;
        }

        public async Task UpdateProductSellQuantityAsync(int foodId,int orderId, int orderQuantity)
        {
            var orderDetails = await GetOrderDetailsByOrderIdAsync(orderId);

            var orderDetailForFood = orderDetails.FirstOrDefault(od => od.IdFood == foodId);

            if (orderDetailForFood != null)
            {
                var existingProductSell = await _dataContext.ProductSells
                    .FirstOrDefaultAsync(p => p.FoodId == foodId);

                if (existingProductSell != null)
                {
                    Console.WriteLine($"Số lượng hiện tại: {existingProductSell.Quantity}");

                    existingProductSell.Quantity += orderDetailForFood.Quantity; 
                    existingProductSell.UpdatedAt = DateTime.UtcNow;

                    Console.WriteLine($"Số lượng mới: {existingProductSell.Quantity}");
                    await _dataContext.SaveChangesAsync();  
                }
                else
                {
                    var newProductSell = new ProductSell
                    {
                        FoodId = foodId,
                        Quantity = orderDetailForFood.Quantity, 
                        UpdatedAt = DateTime.UtcNow
                    };

                    _dataContext.ProductSells.Add(newProductSell);
                    await _dataContext.SaveChangesAsync();  
                }
            }
        }

        private async Task<List<OrderDetail>> GetOrderDetailsByOrderIdAsync(int orderId)
        {
            return await _dataContext.OrderDetails
        .Where(od => od.IdOrder == orderId)
        .ToListAsync();
        }

        public async Task<List<ProductSell>> GetTopProductsByQuantityAsync(int categoryId)
        {
            return await _dataContext.ProductSells
                 .Where(p => p.Food.IdCategory == categoryId)
                .OrderByDescending(p => p.Quantity)
                .Take(4)
                .ToListAsync();
        }
    }
}
