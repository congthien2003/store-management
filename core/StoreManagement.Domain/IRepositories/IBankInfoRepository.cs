using StoreManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Domain.IRepositories
{
    public interface IBankInfoRepository<T> where T : BankInfo
    {
        Task<BankInfo> Create(BankInfo req);
        Task<BankInfo> Update(BankInfo req);
        Task<BankInfo> Delete(int id);
        Task<List<BankInfo>> GetAllByIdStore(int idStore);
        Task<BankInfo> GetById(int id);


    }
}
