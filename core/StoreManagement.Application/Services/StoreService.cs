using AutoMapper;
using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response;
using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Domain.IRepositories;
using StoreManagement.Domain.Models;

namespace StoreManagement.Services
{
    public class StoreService : IStoreService
    {
        private readonly IMapper _mapper;
        private readonly IStoreRepository<Store> _storeRepository;
        private readonly IUserRepository<User> _userRepository;
        public StoreService(IMapper mapper, IStoreRepository<Store> storeRepository, IUserRepository<User> userRepository)
        {
            _mapper = mapper;
            _storeRepository = storeRepository;
            _userRepository = userRepository;
        }

        public async Task<StoreDTO> CreateAsync(StoreDTO storeDTO)
        {
            var store = _mapper.Map<Store>(storeDTO);
            var storeCreated = await _storeRepository.CreateAsync(store);
            return _mapper.Map<StoreDTO>(storeCreated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _storeRepository.DeleteAsync(id);
            return true;
        }

        public async Task<PaginationResult<List<StoreResponse>>> GetAllAsync(string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortColumn = "", string asc = "true")
        {
            int _currentPage = int.Parse(currentPage);
            int _pageSize = int.Parse(pageSize);
            bool _asc = bool.Parse(asc);
            var totalRecords = await _storeRepository.CountAsync(searchTerm);
            var list = await _storeRepository.GetAllAsync(_currentPage, _pageSize, searchTerm, sortColumn, _asc);
            var count = list.Count();

            //var listStore = _mapper.Map<List<StoreResponse>>(list);
            var listStore = new List<StoreResponse>();
            foreach (var store in list)
            {
                var storeResponse = _mapper.Map<StoreResponse>(store);
                var user = await _userRepository.GetById(store.IdUser);
                if (user != null)
                {
                    storeResponse.UserDTO = _mapper.Map<UserDTO>(user);
                }
                listStore.Add(storeResponse);
            }


            return PaginationResult<List<StoreResponse>>.Create(listStore, _currentPage, _pageSize, totalRecords);
        }

        public async Task<StoreResponse> GetByIdAsync(int id)
        {
            var store = await _storeRepository.GetByIdAsync(id);


            var storeResponse = _mapper.Map<StoreResponse>(store);

            if (store.User != null)
            {
                storeResponse.UserDTO = new UserDTO
                {
                    Id = store.User.Id,
                    Email = store.User.Email,
                    Username = store.User.Username,
                    Phones = store.User.Phones,
                    Role = store.User.Role
                };
            }
            return storeResponse;
        }


        public async Task<List<StoreResponse>> GetByNameAsync(string name)
        {
            var store = await _storeRepository.GetByNameAsync(name);
            var storeResponse = _mapper.Map<List<StoreResponse>>(store);
            for (int i = 0; i < store.Count; i++)
            {
                if (store[i].User != null)
                {
                    storeResponse[i].UserDTO = new UserDTO
                    {
                        Id = store[i].User.Id,
                        Email = store[i].User.Email,
                        Username = store[i].User.Username,
                        Phones = store[i].User.Phones,
                        Role = store[i].User.Role
                    };
                }
            }
            return storeResponse;
        }

        public async Task<int> GetCountList(string searchTerm = "", bool incluDeleted = false)
        {
            var count = await _storeRepository.GetCount(searchTerm, incluDeleted);
            return count;
        }

        public async Task<StoreDTO> UpdateAsync(StoreDTO storeDTO)
        {
            var storeUpdate = _mapper.Map<Store>(storeDTO);
            var update = await _storeRepository.UpdateAsync(storeUpdate);
            return _mapper.Map<StoreDTO>(update);
        }
    }
}
