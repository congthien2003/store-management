using StoreManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Domain.IRepositories
{
    public interface IProductSellRepository<TProductSell> where TProductSell : ProductSell
    {
        Task<TProductSell> CreateAsync(TProductSell productSell);
        Task<TProductSell> UpdateAsync(int id, TProductSell productSell, bool incluDeleted = false);
        Task<TProductSell> GetByIdAsync(int id, bool incluDeleted = false);
        Task<TProductSell?> GetByIdFoodAsync(int idFood, bool incluDeleted = false);
        Task<TProductSell> DeleteAsync(int id, bool incluDeleted = false);

        Task<TProductSell> GetByFoodIdAsync(int foodId);
        Task<int> CountAsync(string searchTerm);
        Task<List<TProductSell>> GetAll(int currentPage = 1, int pageSize = 5, string searchTerm = "", string sortCol = "", bool ascSort = true, bool incluDeleted = false);
        Task<TProductSell> UpdateQuantityAsync(int id, int quantity);
        Task UpdateProductSellQuantityAsync(int foodId, int orderId, int orderQuantity);
        Task<List<TProductSell>> GetTopProductsByQuantityAsync(int idStore, int idCategory);
        Task<List<TProductSell>> GetTopProductsByQuantityByStoreAsync(int idStore);

        Task<int> GetTotalProductSellByMonth(int month, int year);
    }
}
