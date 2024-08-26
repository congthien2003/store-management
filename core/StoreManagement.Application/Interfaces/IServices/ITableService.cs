﻿using StoreManagement.Application.DTOs;

namespace StoreManagement.Application.Interfaces.IServices
{
    public interface ITableService
    {
        Task<TableDTO> CreateAsync(TableDTO tableDTO);
        Task<TableDTO> UpdateAsync(int id, TableDTO tableDTO);
        Task<bool> DeleteAsync(int id);
        Task<TableDTO> GetByIdAsync(int id);
        Task<List<TableDTO>> GetAllByIdStore(int id, int currentPage = 1, int pageSize = 5, string sortCol = "", bool ascSort = true);
        Task<int> GetCountAsync(int id);
    }
}
