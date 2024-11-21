using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Domain.IRepositories;
using StoreManagement.Domain.Models;
using StoreManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Infrastructure.Repositories
{
    public class BankInfoRepository : IBankInfoRepository<BankInfo>
    {
        private readonly DataContext _dataContext;

        public BankInfoRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<BankInfo> Create(BankInfo req)
        {
            BankInfo bankInfo = new BankInfo{
                AccountId = req.AccountId,
                BankId = req.BankId,
                AccountName = req.AccountName,
                IdStore = req.IdStore,

            };

            _dataContext.BankInfos.Add(bankInfo);
            await _dataContext.SaveChangesAsync();
            return bankInfo;
        }

        public async Task<BankInfo> Delete(int id)
        {
            
                var bankInfo = await _dataContext.BankInfos.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
                if (bankInfo == null)
                {
                    throw new NullReferenceException("Không tìm thấy thông tin ngân hàng cần xóa");
                }
                _dataContext.BankInfos.Remove(bankInfo);
                await _dataContext.SaveChangesAsync();
                return bankInfo;
           
        }

        public async Task<BankInfo> GetById(int id)
        {
            var bankInfo = await _dataContext.BankInfos.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (bankInfo == null)
            {
                throw new NullReferenceException("Không tìm thấy thông tin ngân hàng cần xóa");
            }
            return bankInfo;
        }

        public async Task<List<BankInfo>> GetAllByIdStore(int idStore)
        {
            var bankInfo = await _dataContext.BankInfos.Include("Store").Where(x => x.IdStore == idStore && x.IsDeleted == false).ToListAsync();
            if (bankInfo == null)
            {
                throw new NullReferenceException("Không tìm thấy thông tin ngân hàng cần xóa");
            }
            return bankInfo;
        }

        public async Task<BankInfo> Update(BankInfo req)
        {
            var bankInfo = await _dataContext.BankInfos.FirstOrDefaultAsync(x => x.Id == req.Id && x.IsDeleted == false);
            if (bankInfo == null)
            {
                throw new NullReferenceException("Không tìm thấy thông tin ngân hàng cần xóa");
            }
            bankInfo.BankId = req.BankId;
            bankInfo.AccountId = req.AccountId;
            bankInfo.AccountName = req.AccountName;

            await _dataContext.SaveChangesAsync();
            return bankInfo;
        }
    }
}
