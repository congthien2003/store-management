using AutoMapper;
using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Request.Combo;
using StoreManagement.Application.DTOs.Response.Combo;
using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Domain.IRepositories;
using StoreManagement.Domain.Models;

namespace StoreManagement.Application.Services
{
    public class ComboService : IComboService
    {
        private readonly IMapper _mapper;
        private readonly IComboRepository _comboRepository;
        private readonly IComboItemRepository _comboItemRepository;
        public ComboService(IMapper mapper, IComboRepository comboRepository, IComboItemRepository comboItemRepository)
        {
            _mapper = mapper;
            _comboRepository = comboRepository;
            _comboItemRepository = comboItemRepository;
        }

        public async Task<ComboDTO> CreateAsync(CreateComboReq comboDTO)
        {
            var combo = _mapper.Map<Combo>(comboDTO);
            var createdCombo = await _comboRepository.AddComboAsync(combo);
            var createComboItem = await _comboItemRepository.AddComboItemByListId(comboDTO.IdFoods, createdCombo.Id);
            if (!createComboItem)
            {
                await _comboRepository.DeleteComboAsync(createdCombo.Id);
                return null;
            }
            return _mapper.Map<ComboDTO>(createdCombo);
        }

        public async Task<ComboDTO> UpdateAsync(int id, ComboDTO comboDTO)
        {
            var comboUpdate = _mapper.Map<Combo>(comboDTO);
            var updatedCombo = await _comboRepository.UpdateComboAsync(id, comboUpdate);
            return _mapper.Map<ComboDTO>(updatedCombo);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _comboRepository.DeleteComboAsync(id);
            return true;
        }

        public async Task<ComboDTO> GetByIdAsync(int id)
        {
            var combo = await _comboRepository.GetComboByIdAsync(id);
            return _mapper.Map<ComboDTO>(combo);
        }

        public async Task<PaginationResult<List<ComboWithFood>>> GetAllAsync(int idStore, string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortColumn = "", bool asc = false)
        {
            int _currentPage = int.Parse(currentPage);
            int _pageSize = int.Parse(pageSize);

            var list = await _comboRepository.GetAllByStoreAsync(idStore, searchTerm, sortColumn, asc);
            var count = list.Count();

            list = list.Skip(_currentPage * _pageSize - _pageSize).Take(_pageSize).ToList();

            // Map Combos to ComboWithFoodsResponse
            var response = list.Select(combo => new ComboWithFood
            {
                Id = combo.Id,
                Name = combo.Name,
                Price = combo.Price,
                Description = combo.Description,
                Foods = combo.ComboItems.Select(ci => _mapper.Map<FoodDTO>(ci.Food)).ToList()
            }).ToList();

            return PaginationResult<List<ComboWithFood>>.Create(response, _currentPage, _pageSize, count);
        }
    }
}
