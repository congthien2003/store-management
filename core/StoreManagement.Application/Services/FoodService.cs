using AutoMapper;
using StoreManagement.Application.DTOs;
using StoreManagement.Application.Interfaces.IRepositories;
using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Domain.Models;

namespace StoreManagement.Services
{
    public class FoodService : IFoodService
    {
        private readonly IMapper _mapper;
        private readonly IFoodRepository<Food> _foodRepository;

        public FoodService(IMapper mapper, IFoodRepository<Food> foodRepository) 
        {
            _mapper = mapper;
            _foodRepository = foodRepository;
        }
        public async Task<FoodDTO> CreateAsync(FoodDTO foodDTO)
        {
                var food = _mapper.Map<Food>(foodDTO);
                var foodCreated = await _foodRepository.CreateAsync(food);
                return _mapper.Map<FoodDTO>(foodCreated);       
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _foodRepository.DeleteAsync(id);
            return true;
        }

        public async Task<List<FoodDTO>> GetAllByIdStoreAsync(int id,int currentPage = 1, int pageSize = 5, string searchTerm = "", string sortColumn = "", bool ascSort = true, bool incluDeleted = false)
        {
            var listFoods = await _foodRepository.GetAllByIdStoreAsync(id,currentPage, pageSize, searchTerm, sortColumn, ascSort, incluDeleted);
            return _mapper.Map<List<FoodDTO>>(listFoods);
        }

        public async Task<FoodDTO> GetByIdAsync(int id)
        {
            var food = await _foodRepository.GetByIdAsync(id);
            return _mapper.Map<FoodDTO>(food);
        }

        public async Task<List<FoodDTO>> GetByIdCategoryAsync(int id)
        {
            var listFood = await _foodRepository.GetByIdCategory(id);
            return _mapper.Map<List<FoodDTO>>(listFood);
        }

        public async Task<List<FoodDTO>> GetByNameAsync(int idStore, string name)
        {
            var listFood = await _foodRepository.GetByNameAsync(idStore, name);
            return _mapper.Map<List<FoodDTO>>(listFood);
        }

        public async Task<int> GetCountList(int idStore, string searchTerm = "", bool incluDeleted = false)
        {
            var count = await _foodRepository.GetCountAsync(idStore,  searchTerm, incluDeleted);
            return count;
        }

        public async Task<FoodDTO> UpdateAsync(int id, FoodDTO foodDTO)
        {
            var foodUpdate = _mapper.Map<Food>(foodDTO);
            var update = await _foodRepository.UpdateAsync(id, foodUpdate);
            return _mapper.Map<FoodDTO>(update);
        }
    }
}
