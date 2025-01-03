﻿using StoreManagement.Domain.Models;

namespace StoreManagement.Domain.IRepositories
{
    public interface IFoodRepository<TFood> where TFood : Food
    {
        Task<TFood> CreateAsync(TFood food);
        Task<TFood> UpdateAsync(int id, TFood food, bool incluDeleted = false);
        Task<TFood> DeleteAsync(int id);
        Task<TFood> GetByIdAsync(int id, bool incluDeleted = false);
        Task<List<TFood>> GetByNameAsync(int idStore, string name, bool incluDeleted = false);
        Task<List<TFood>> GetByIdCategory(int id, bool incluDeleted = false);
        Task<int> GetCountAsync(int idStore, string searchTerm = "", bool incluDeleted = false);
        Task<List<TFood>> GetAllByIdStoreAsync(int id, string searchTerm = "", string sortCol = "", bool ascSort = true, int? categoryId = null, bool incluDeleted = false);
        Task<List<TFood>> GetAllByIdStoreNoPagin(int idStore);
        Task<List<TFood>> GetAllByStoreAndByCateAsync(int idStore, int idCategory, int currentPage, int pageSize);
        Task<List<TFood>> GetAllByStoreAsync(int idStore, int currentPage, int pageSize);
    }
}
