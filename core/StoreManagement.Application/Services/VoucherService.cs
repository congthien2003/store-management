using AutoMapper;
using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response;
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

        public async Task<PaginationResult<List<VoucherDTO>>> GetAllByIdStore(int idStore, string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortColumn = "", string asc = "true")
        {
            int _currentPage = int.Parse(currentPage);
            int _pageSize = int.Parse(pageSize);
            bool _asc = bool.Parse(asc);
            var listVouchers = await _voucherRepository.GetAllByIdStoreAsync(idStore, _currentPage, _pageSize, searchTerm, sortColumn, _asc);
            var totalRecords = await _voucherRepository.GetCountAsync(idStore);
            var list = _mapper.Map<List<VoucherDTO>>(listVouchers);
            return PaginationResult<List<VoucherDTO>>.Create(list, _currentPage,_pageSize,totalRecords);
        }

        public Task<PaginationResult<List<VoucherResponse>>> GetAllByIdStore(int idStore, string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortColumn = "", bool ascSort = true)
        {
            throw new NotImplementedException();
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

        Task<VoucherResponse> IVoucherService.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<List<VoucherResponse>> IVoucherService.GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
