using AutoMapper;
using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response;
using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Domain.IRepositories;
using StoreManagement.Domain.Models;

namespace StoreManagement.Application.Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository<Staff> _staffRepository;
        private readonly IMapper _mapper;

        public StaffService(IStaffRepository<Staff> staffRepository, IMapper mapper)
        {
            _staffRepository = staffRepository;
            _mapper = mapper;
        }

        public async Task<StaffDTO> CreateAsync(StaffDTO staffDTO)
        {
            var staff = _mapper.Map<Staff>(staffDTO);
            var staffCreated = await _staffRepository.CreateAsync(staff);
            return _mapper.Map<StaffDTO>(staffCreated);

        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _staffRepository.DeleteAsync(id);
            return true;
        }

        public async Task<PaginationResult<List<StaffResponse>>> GetAllByIdStoreAsync(int idStore, string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortColumn = "", bool asc = false, bool filter = false, int? categoryId = null)
        {
            int _currentPage = int.Parse(currentPage);
            int _pageSize = int.Parse(pageSize);
            var list = await _staffRepository.GetAllByIdStore(idStore, false, searchTerm);
            var count = list.Count;
            list = list.Skip(_currentPage * _pageSize - _pageSize).Take(_pageSize).ToList();
            var listStaff = _mapper.Map<List<StaffResponse>>(list);
            return PaginationResult<List<StaffResponse>>.Create(listStaff, _currentPage, _pageSize, count);
        }

        public async Task<StaffResponse> GetByIdAsync(int id)
        {
            var staff = await _staffRepository.GetByIdAsync(id);
            return _mapper.Map<StaffResponse>(staff);
        }

        public async Task<List<StaffResponse>> GetByNameAsync(string name, int idStore)
        {
            var listStaffs = await _staffRepository.GetStaffByNameAsync(name, idStore);
            return _mapper.Map<List<StaffResponse>>(listStaffs);
        }

        public async Task<StaffDTO> UpdateAsync(int id, StaffDTO staffDTO)
        {
            var staffUpdate = _mapper.Map<Staff>(staffDTO);
            var update = await _staffRepository.UpdateASync(id, staffUpdate);
            return _mapper.Map<StaffDTO>(update);
        }
    }
}
