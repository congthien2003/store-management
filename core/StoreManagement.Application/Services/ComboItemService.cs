using AutoMapper;
using StoreManagement.Application.DTOs.Request.ComboItem;
using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Domain.IRepositories;
using StoreManagement.Domain.Models;

namespace StoreManagement.Application.Services
{
    public class ComboItemService : IComboItemService
    {
        private readonly IMapper _mapper;
        private readonly IComboItemRepository _comboItemRepository;

        public ComboItemService(IMapper mapper, IComboItemRepository comboItemRepository)
        {
            _mapper = mapper;
            _comboItemRepository = comboItemRepository;
        }

        public async Task<ComboItemDTO> CreateAsync(ComboItemDTO comboItemDTO)
        {
            var comboItem = _mapper.Map<ComboItem>(comboItemDTO);
            var createdItem = await _comboItemRepository.AddComboItemAsync(comboItem);
            return _mapper.Map<ComboItemDTO>(createdItem);
        }

        public async Task<ComboItemDTO> UpdateAsync(int id, ComboItemDTO comboItemDTO)
        {
            var comboItemUpdate = _mapper.Map<ComboItem>(comboItemDTO);
            var updatedItem = await _comboItemRepository.UpdateComboItemAsync(id, comboItemUpdate);
            return _mapper.Map<ComboItemDTO>(updatedItem);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _comboItemRepository.DeleteComboItemAsync(id);
            return true;
        }

        public async Task<List<ComboItemDTO>> GetByComboIdAsync(int comboId)
        {
            var comboItems = await _comboItemRepository.GetItemsByComboIdAsync(comboId);
            return _mapper.Map<List<ComboItemDTO>>(comboItems);
        }

        public async Task<List<ComboItemDTO>> CreateByListIdFood(int idCombo, int[] listIdFood)
        {
            List<ComboItemDTO> result = new List<ComboItemDTO>();
            foreach (var id in listIdFood)
            {
                ComboItem temp = new ComboItem
                {
                    Id = 0,
                    IdCombo = idCombo,
                    IdFood = id
                };
                var createdItem = await _comboItemRepository.AddComboItemAsync(temp);
                result.Add(_mapper.Map<ComboItemDTO>(createdItem));
            }
            return result;
        }
    }
}
