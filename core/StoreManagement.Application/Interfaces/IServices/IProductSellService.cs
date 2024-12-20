﻿using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response;
using StoreManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.Interfaces.IServices
{
    public interface IProductSellService
    {
        Task<ProductSellDTO> CreateAsync(ProductSellDTO productSellDTO);
        Task<ProductSellDTO> UpdateAsync(int id, ProductSellDTO productSellDTO);
        Task<bool> DeleteAsync(int id);
        Task<ProductSellDTO> GetByIdAsync(int id);
        Task<PaginationResult<List<ProductSellResponse>>> GetAllAsync(string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortColumn = "", string asc = "true");

    }
}
