using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response.BankInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.Interfaces.IServices
{
    public interface IBankInfoService
    {
        Task<BankInfoDTO> Create(BankInfoDTO req);
        Task<BankInfoResponse> Update(BankInfoDTO req);
        Task<bool> Delete(int id);
        Task<BankInfoResponse> Get(int id);
        Task<List<BankInfoResponse>> GetListByIdStore(int IdStore);

    }
}
