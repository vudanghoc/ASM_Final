using Domain.Entities;
using Services.Contracts.DataAccess.Base;

namespace Services.Contracts.DataAccess
{
    public interface IComboDataAccess : IGeneralDataAcces<Combo>
    {
        Task<IEnumerable<Combo>> GetAllCombos(string name);
        Task<IEnumerable<Combo>> GetAllCombos( );
        Combo UpdateCombo(Combo combo);
        Task<Combo> GetComboById(int id);
/*        Task<Combo> GetComboByName(string name);*/
        Task<Combo> AddCombo(Combo combo);
        bool DeleteCombo(Combo combo);
    }
}
