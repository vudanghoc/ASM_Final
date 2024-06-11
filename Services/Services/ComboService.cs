using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Services.Contracts.DataAccess;
using Services.Contracts.Services;
using Services.Helper;
using Services.Models.Combo;

namespace Services.Services
{
    public class ComboService : IComboService
    {
        private readonly IComboDataAccess _comboDataAccess;
        private readonly ILogger<ComboService> _logger;
        private readonly IMapper _mapper;

        public ComboService(IComboDataAccess comboDataAccess, ILogger<ComboService> logger, IMapper mapper)
        {
            _comboDataAccess = comboDataAccess;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Combo> AddCombo(ComboForCreate comboDto)
        {
            try
            {
                Combo combo = _mapper.Map<Combo>(comboDto);
                combo.ProductCombos=_mapper.Map<ICollection<ProductCombo>>(comboDto.ProductCombos);
                await _comboDataAccess.AddCombo(combo);
                return combo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null!;
        }
        public async Task<bool> DeleteCombo(int id)
        {
            try
            {
                Combo combo = await _comboDataAccess.GetComboById(id);
                if (combo != null)
                {
                    _comboDataAccess.DeleteCombo(combo);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return false!;
        }

        public async Task<ComboForView> GetAllCombos(string name)
        {
            try
            {
               // name = HelperNomalize.RemoveDiacritics(name);
                IEnumerable<Combo> combos = await _comboDataAccess.GetAllCombos(name);
                IList<ComboForViewItems> items = _mapper.Map<IEnumerable<ComboForViewItems>>(combos).ToList();
                ComboForView response = new ComboForView();
                response.Combos = items;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null!;
        }
        public async Task<ComboForView> GetAllCombos( )
        {
            try
            {
                // name = HelperNomalize.RemoveDiacritics(name);
                IEnumerable<Combo> combos = await _comboDataAccess.GetAllCombos();
                IList<ComboForViewItems> items = _mapper.Map<IEnumerable<ComboForViewItems>>(combos).ToList();
                ComboForView response = new ComboForView();
                response.Combos = items;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null!;
        }

        public async Task<ComboForViewItems> GetComboById(int id)
        {
            try
            {
                Combo combo = await _comboDataAccess.GetComboById(id);
                ComboForViewItems items = _mapper.Map<ComboForViewItems>(combo);
                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null!;
        }
        /*public async Task<ComboForViewItems> GetComboByName(string name)
        {
            try
            {
                Combo combo = await _comboDataAccess.GetComboByName(name);
                ComboForViewItems items = _mapper.Map<ComboForViewItems>(combo);
                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null!;
        }*/
        public async Task<bool> UpdateCombo(ComboForUpdate comboDto, int id)
        {
            try
            {
                Combo combo = await _comboDataAccess.GetComboById(id);
                if (combo != null)
                {
                    combo.ProductCombos =_mapper.Map<ICollection<ProductCombo>>(comboDto.ProductCombos);
                    _mapper.Map(comboDto, combo);
                    _comboDataAccess.UpdateCombo(combo);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return false!;
        }
    }
}
