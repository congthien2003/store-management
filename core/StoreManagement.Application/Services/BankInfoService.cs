using AutoMapper;
using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Request.Store;
using StoreManagement.Application.DTOs.Response.BankInfo;
using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Domain.IRepositories;
using StoreManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.Services
{
    public class BankInfoService : IBankInfoService
    {
        private readonly IBankInfoRepository<BankInfo> _bankInfoRepository;
        private readonly IMapper _mapper;

        public BankInfoService(IBankInfoRepository<BankInfo> bankInfoRepository, IMapper mapper)
        {
            _bankInfoRepository = bankInfoRepository;
            _mapper = mapper;
        }

        public async Task<BankInfoDTO> Create(BankInfoDTO req)
        {
            var bankInfo = _mapper.Map<BankInfo>(req);
            var bankInfoCreated = await _bankInfoRepository.Create(bankInfo);
            return _mapper.Map<BankInfoDTO>(bankInfoCreated);
        }

        public async Task<bool> Delete(int id)
        {
            await _bankInfoRepository.Delete(id);
            return true;
        }

        public async Task<BankInfoResponse> Get(int id)
        {
            var bankInfo = await _bankInfoRepository.GetById(id);
            return _mapper.Map<BankInfoResponse>(bankInfo);
        }

        public async Task<List<BankInfoResponse>> GetListByIdStore(int IdStore)
        {
            var list = await _bankInfoRepository.GetAllByIdStore(IdStore);

            var listBankInfo = _mapper.Map<List<BankInfoResponse>>(list);

            for(var i = 0; i < list.Count; i++)
            {
                BankInfoResponse res = new BankInfoResponse();
                var storeDTO = _mapper.Map<StoreDTO>(list[i].Store);
                listBankInfo[i].StoreDTO = storeDTO;
            }
            return listBankInfo;
        }

        public async Task<BankInfoResponse> Update(BankInfoDTO req)
        {
            var bankInfoUpdate = _mapper.Map<BankInfo>(req);
            var update = await _bankInfoRepository.Update(bankInfoUpdate);
            return _mapper.Map<BankInfoResponse>(update);
        }
    }
}
