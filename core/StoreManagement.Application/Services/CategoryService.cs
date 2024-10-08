using AutoMapper;
using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response;
using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Domain.IRepositories;
using StoreManagement.Domain.Models;

namespace StoreManagement.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository<Category> _categoryRepository;
        private readonly IStoreRepository<Store> _storeRepository;

        public CategoryService(IMapper mapper, ICategoryRepository<Category> categoryRepository, IStoreRepository<Store> storeRepository)
        {
            _mapper = mapper;
            _storeRepository = storeRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryDTO> CreateAsync(CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);
            var categoryCreated = await _categoryRepository.CreateAsync(category);
            return _mapper.Map<CategoryDTO>(categoryCreated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _categoryRepository.DeleteAsync(id);
            return true;
        }

        public async Task<PaginationResult<List<CategoryResponse>>> GetAllByIdStoreAsync(int id, string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortColumn = "", string ascSort = "true", bool incluDeleted = false)
        {
            int _currentPage = int.Parse(currentPage);
            int _pageSize = int.Parse(pageSize);
            bool _asc = bool.Parse(ascSort);
            var list = await _categoryRepository.GetAllByIdStoreAsync(id, _currentPage, _pageSize, searchTerm, sortColumn, _asc, incluDeleted);
            var listCategory = new List<CategoryResponse>();
            foreach (var category in list)
            {
                var categoryResponse = _mapper.Map<CategoryResponse>(category);
                var store = await _storeRepository.GetByIdAsync(category.IdStore);
                if (store != null)
                {
                    categoryResponse.StoreDTO = _mapper.Map<StoreDTO>(store);
                }
                listCategory.Add(categoryResponse);
            }
            var totalRecords = await _categoryRepository.GetCountAsync(id);
            return PaginationResult<List<CategoryResponse>>.Create(listCategory, _currentPage, _pageSize, totalRecords);
        }

        public async Task<CategoryResponse> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            var categoryResponse = _mapper.Map<CategoryResponse>(category);

            if (category.Store != null)
            {
                categoryResponse.StoreDTO = new StoreDTO
                {
                    Id = category.Store.Id,
                    Name = category.Store.Name,
                    Address = category.Store.Address,
                    Phone = category.Store.Phone,
                    IdUser = category.Store.IdUser
                };
            }
            return categoryResponse;
        }

        public async Task<List<CategoryResponse>> GetByNameAsync(int idStore, string name)
        {
            var category = await _categoryRepository.GetByNameAsync(idStore, name);
            var listCategoires = _mapper.Map<List<CategoryResponse>>(category);
            for (int i = 0; i < category.Count; i++)
            {
                if (category[i].Store != null)
                {
                    if (listCategoires[i].StoreDTO == null)
                    {
                        listCategoires[i].StoreDTO = new StoreDTO();
                    }

                    listCategoires[i].StoreDTO.Id = category[i].Store.Id;
                    listCategoires[i].StoreDTO.Address = category[i].Store.Address;
                    listCategoires[i].StoreDTO.Name = category[i].Store.Name;
                    listCategoires[i].StoreDTO.Phone = category[i].Store.Phone;
                    listCategoires[i].StoreDTO.IdUser = category[i].Store.IdUser;
                }
            }
            return listCategoires;
        }

        public async Task<int> GetCountList(int idStore, string searchTerm = "", bool incluDeleted = false)
        {
            var count = await _categoryRepository.GetCountAsync(idStore, searchTerm, incluDeleted);
            return count;
        }

        public async Task<CategoryDTO> UpdateAsync(int id, CategoryDTO categoryDTO)
        {
            var categoryUpdate = _mapper.Map<Category>(categoryDTO);
            var update = await _categoryRepository.UpdateAsync(id, categoryUpdate);
            return _mapper.Map<CategoryDTO>(update);
        }

       
    }
}
