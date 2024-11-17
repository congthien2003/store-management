using StoreManagement.Domain.IRepositories;
using StoreManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.Interfaces.IUnitOfWork
{
    public interface DeleteFood : IDisposable
    {
        IFoodRepository<Food> FoodRepository { get; }
        Task<int> CommitAsync();
    }
}
