using AutoMapper;
using StoreManagement.Application.Common;
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
        private readonly IProductSellRepository<ProductSell> _productSellRepository;
        public FoodService(IMapper mapper, IFoodRepository<Food> foodRepository, ICategoryRepository<Category> categoryRepository, IProductSellRepository<ProductSell> productSellRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _foodRepository = foodRepository;
            _productSellRepository = productSellRepository;
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

        public async Task<PaginationResult<List<FoodResponse>>> GetAllByIdStoreAsync(int id, string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortColumn = "", string asc = "true", bool incluDeleted = false)
        {

            int _currentPage = int.Parse(currentPage);
            int _pageSize = int.Parse(pageSize);
            bool _asc = bool.Parse(asc);
            var list = await _foodRepository.GetAllByIdStoreAsync(id, _currentPage, _pageSize, searchTerm, sortColumn, _asc, incluDeleted);
            var listFood = new List<FoodResponse>();
            foreach (var food in list)
            {
                var foodResponse = _mapper.Map<FoodResponse>(food);
                var category = await _categoryRepository.GetByIdAsync(food.IdCategory);
                if (category != null)
                {
                    foodResponse.CategoryDTO = _mapper.Map<CategoryDTO>(category);
                }
                listFood.Add(foodResponse);
            }
            var totalRecords = await _foodRepository.GetCountAsync(id);

            return PaginationResult<List<FoodResponse>>.Create(listFood,_currentPage,_pageSize,totalRecords);
        }

        public async Task<FoodResponse> GetByIdAsync(int id)
        {
            var food = await _foodRepository.GetByIdAsync(id);
            var foodResponse = _mapper.Map<FoodResponse>(food);

            if (food.Category != null)
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
            var count = await _foodRepository.GetCountAsync(idStore, searchTerm, incluDeleted);
            return count;
        }

        public async Task<FoodTopDTO> GetTopFood(int idCategory, int currentPage, int pageSize)
        {
            var allFoods = await _foodRepository.GetAllAsync(idCategory, currentPage, pageSize);
            var top3product = await _productSellRepository.GetTopProductsByQuantityAsync(idCategory);

            var allFoodDto = _mapper.Map<List<FoodDTO>>(allFoods);
            var top3productDTO = _mapper.Map<List<ProductSellDTO>>(top3product);

            List<FoodDTO> filteredFoods;

            if (currentPage == 1)
            {
                filteredFoods = allFoodDto
                    .Where(f => !top3productDTO.Any(p => p.FoodId == f.Id))
                    .Take(6)
                    .ToList();
            }
            else
            {
                filteredFoods = allFoodDto
                    .Where(f => !top3productDTO.Any(p => p.FoodId == f.Id))
                    .Take(10)
                    .ToList();
            }

            return new FoodTopDTO
            {
                Top3ProductsByQuantity = currentPage == 1 ? top3productDTO : new List<ProductSellDTO>(), 
                AllFoods = filteredFoods
            };
        }

        public async Task<FoodDTO> UpdateAsync(int id, FoodDTO foodDTO)
        {
            var foodUpdate = _mapper.Map<Food>(foodDTO);
            var update = await _foodRepository.UpdateAsync(id, foodUpdate);
            return _mapper.Map<FoodDTO>(update);
        }


    }
}
