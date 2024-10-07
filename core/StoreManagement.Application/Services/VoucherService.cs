using AutoMapper;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Domain.IRepositories;
using StoreManagement.Domain.Models;

namespace StoreManagement.Services
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherRepository<Voucher> _voucherRepository;
        private readonly IMapper _mapper;

        public VoucherService(IVoucherRepository<Voucher> voucherRepository, IMapper mapper) 
        {
            _voucherRepository = voucherRepository;
            _mapper = mapper;   
        }
        public async Task<VoucherDTO> CreateAsync(VoucherDTO voucherDTO)
        {
            var voucher = _mapper.Map<Voucher>(voucherDTO);
            var voucherCreated = await _voucherRepository.CreateAsync(voucher);
            return _mapper.Map<VoucherDTO>(voucherCreated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _voucherRepository.DeleteAsync(id);
            return true;
        }

        public async Task<List<VoucherDTO>> GetAllByIdStore(int idStore, int currentPage = 1, int pageSize = 5, string searchTerm = "", string sortColumn = "", bool ascSort = true)
        {
            var listVouchers = await _voucherRepository.GetAllByIdStoreAsync(idStore, currentPage, pageSize, searchTerm, sortColumn, ascSort);
            return _mapper.Map<List<VoucherDTO>>(listVouchers);
        }

        public async Task<VoucherDTO> GetByIdAsync(int id)
        {
            var voucher = await _voucherRepository.GetByIdAsync(id);
            return _mapper.Map<VoucherDTO>(voucher);
        }

        public async Task<List<VoucherDTO>> GetByNameAsync(string name)
        {
            var listVouchers = await _voucherRepository.GetByNameAsync(name);
            return _mapper.Map<List<VoucherDTO>>(listVouchers);
        }

        public async Task<int> GetCountList(int idStore, string searchTerm = "")
        {
            var count = await _voucherRepository.GetCountAsync(idStore,searchTerm);
            return count;
        }

        public async Task<VoucherDTO> UpdateAsync(int id, VoucherDTO voucherDTO)
        {
            var voucherUpdate = _mapper.Map<Voucher>(voucherDTO);
            var update = await _voucherRepository.UpdateAsync(id, voucherUpdate);
            return _mapper.Map<VoucherDTO>(update);
        }
    }
}
