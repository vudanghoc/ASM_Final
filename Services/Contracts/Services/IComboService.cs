using Domain.Entities;
using Services.Models.Combo;

namespace Services.Contracts.Services
{
    public interface IComboService
    {
        Task<ComboForView> GetAllCombos(string name);
        Task<ComboForView> GetAllCombos();
        Task<bool> UpdateCombo(ComboForUpdate comboDto, int id);
        Task<ComboForViewItems> GetComboById(int id);
        Task<Combo> AddCombo(ComboForCreate comboDto);
        Task<bool> DeleteCombo(int id);
    }
}
