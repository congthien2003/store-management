using AutoMapper;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response;
using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Domain.IRepositories;
using StoreManagement.Domain.Models;

namespace StoreManagement.Services
{
    public class FoodService : IFoodService
    {
        private readonly IMapper _mapper;
        private readonly IFoodRepository<Food> _foodRepository;
        private readonly ICategoryRepository<Category> _categoryRepository;

        public FoodService(IMapper mapper, IFoodRepository<Food> foodRepository, ICategoryRepository<Category> categoryRepository) 
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
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

        public async Task<List<FoodResponse>> GetAllByIdStoreAsync(int id,int currentPage = 1, int pageSize = 5, string searchTerm = "", string sortColumn = "", bool ascSort = true, bool incluDeleted = false)
        {
            var list = await _foodRepository.GetAllByIdStoreAsync(id,currentPage, pageSize, searchTerm, sortColumn, ascSort, incluDeleted);
            var listFood = new List<FoodResponse>();
            foreach(var food in list)
            {
                var foodResponse = _mapper.Map<FoodResponse>(food);
                var category = await _categoryRepository.GetByIdAsync(food.IdCategory);
                if(category != null)
                {
                    foodResponse.CategoryDTO = _mapper.Map<CategoryDTO>(category);
                }
                listFood.Add(foodResponse);
            }
            return listFood;
        }

        public async Task<FoodResponse> GetByIdAsync(int id)
        {
            var food = await _foodRepository.GetByIdAsync(id);
            var foodResponse = _mapper.Map<FoodResponse>(food);

            if(food.Category != null)
            {
                foodResponse.CategoryDTO = new CategoryDTO
                {
                    Id = food.Category.Id,
                    Name = food.Category.Name,
                    IdStore = food.Category.IdStore,
                };
            }
            return foodResponse;
        }

        public async Task<List<FoodResponse>> GetByIdCategoryAsync(int id)
        {
            var listFood = await _foodRepository.GetByIdCategory(id);
            var listFoodResponse = _mapper.Map<List<FoodResponse>>(listFood);

            for (int i = 0; i < listFood.Count; i++)
            {
                if (listFood[i].Category != null)
                {
                    listFoodResponse[i].CategoryDTO = new CategoryDTO
                    {
                        Id = listFood[i].Category.Id,
                        Name = listFood[i].Category.Name,
                        IdStore = listFood[i].Category.IdStore,
                    };
                }
            }

            return listFoodResponse;
        }

        public async Task<List<FoodResponse>> GetByNameAsync(int idStore, string name)
        {
            var listFood = await _foodRepository.GetByNameAsync(idStore, name);
            var listFoodResponse = _mapper.Map<List<FoodResponse>>(listFood);

            // Duyệt qua từng food và kiểm tra category
            for (int i = 0; i < listFood.Count; i++)
            {
                if (listFood[i].Category != null)
                {
                    listFoodResponse[i].CategoryDTO = new CategoryDTO
                    {
                        Id = listFood[i].Category.Id,
                        Name = listFood[i].Category.Name,
                        IdStore = listFood[i].Category.IdStore,
                    };
                }
            }

            return listFoodResponse;
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
