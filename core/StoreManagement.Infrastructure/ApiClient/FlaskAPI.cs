using AutoMapper;
using Newtonsoft.Json;
using StoreManagement.Application.DTOs.ApiClient.FlaskAPI;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response.OrderDetail;
using StoreManagement.Application.DTOs.Response.PredictRevenue;
using StoreManagement.Application.Interfaces.CachingServices;
using StoreManagement.Application.Interfaces.IApiClientServices;
using StoreManagement.Domain.IRepositories;
using StoreManagement.Domain.Models;
using System.Net.Http.Json;

namespace StoreManagement.Infrastructure.ApiClient
{
    public class FlaskAPI : IFlaskAPI
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly IFoodRepository<Food> _foodRepository;
        private readonly ICachingServices _cachingServices;
        private readonly IMapper _mapper;

        public FlaskAPI(IFoodRepository<Food> foodRepository, ICachingServices cachingServices, IMapper mapper)
        {

            _foodRepository = foodRepository;
            _cachingServices = cachingServices;
            _mapper = mapper;
        }

        public async Task<List<object>> GetPopularComboAsync(List<DataByIdStoreRes> listData, int idStore)
        {
            string keyCacheListFood = "listfood";
            string url = "http://127.0.0.1:3253/get_popular_combos";
            var payload = new { data = listData };

            var response = await _httpClient.PostAsJsonAsync(url, payload);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error calling Flask API");
            }
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            var resultFlask = await response.Content.ReadAsStringAsync();

            List<PopularComboRes> combos = JsonConvert.DeserializeObject<List<PopularComboRes>>(resultFlask);

            var listFood = _cachingServices.GetData<IEnumerable<Food>>(keyCacheListFood);

            if (listFood == null)
            {
                listFood = await _foodRepository.GetAllByIdStoreAsync(idStore);
                var expriedTime = DateTime.Now.AddMinutes(2);
                _cachingServices.SetData<IEnumerable<Food>>(keyCacheListFood, listFood, expriedTime);
            }
            Food food = new Food();
            var result = new List<object>();
            foreach (PopularComboRes combo in combos)
            {
                List<FoodDTO> listFoodName = new List<FoodDTO>();

                foreach (var item in combo.combo)
                {
                    food = listFood.Where(x => x.Id == item).First();
                    listFoodName.Add(_mapper.Map<FoodDTO>(food));
                }

                var temp = new
                {
                    combo = listFoodName,
                    score = Math.Round(combo.confidence, 2)
            };
                result.Add(temp);
            }
            return result;
        }

        public async Task<List<object>> GetPredictRevenue(int idStore)
        {
            string keyCacheListFood = "predict";
            string url = "http://127.0.0.1:3253/predict";
            var payload = new { numdays = 5 };

            var response = await _httpClient.PostAsJsonAsync(url, payload);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error calling Flask API");
            }
            var resultFlask = await response.Content.ReadAsStringAsync();

            var predictions = JsonConvert.DeserializeObject<PredictResponse>(resultFlask)?.Prediction;


            // Xây dựng kết quả trả về
            var result = predictions.Select(p => new
            {
                Date = DateTime.Parse(p.Date).ToString("yyyy-MM-dd"),
                p.Revenue,
            }).ToList<object>();

            return result;
        }
    }
}
