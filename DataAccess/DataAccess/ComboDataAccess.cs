using DataAccess.DataAccess.Base;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Services.Contracts.DataAccess;
using Services.Helper;
using System.Collections.Generic;
using System.ComponentModel;

namespace DataAccess.DataAccess
{
    public class ComboDataAccess : GeneralDataAccess<Combo>, IComboDataAccess
    {
        public ComboDataAccess(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<Combo> AddCombo(Combo combo)
        {

            return await AddAsync(combo);
        }

        public bool DeleteCombo(Combo combo)
        {
            Delete(combo);
            return true;
        }

        public async Task<IEnumerable<Combo>> GetAllCombos(string name)
        {
            IEnumerable<Combo> combos = await _context.Combos.Include(x => x.ProductCombos)
            .ThenInclude(x => x.Product).
            Where(x => !string.IsNullOrEmpty(name) ||
            HelperNomalize.RemoveDiacritics(x.Name).ToLower().Contains(name.ToLower())).ToListAsync();
            return combos;
        }
        public async Task<IEnumerable<Combo>> GetAllCombos( )
        {
            IEnumerable<Combo> combos = await _context.Combos.Include(x => x.ProductCombos)
            .ThenInclude(x => x.Product)
            .ToListAsync();
            return combos;
        }


        public async Task<Combo> GetComboById(int id)
        {
            return await _context.Combos.Include(x => x.ProductCombos)
            .ThenInclude(x => x.Product).SingleOrDefaultAsync(x => x.Id == id);
        }

        /*public async Task<Combo> GetComboByName(string name)
        {
            return
        }*/

        public Combo UpdateCombo(Combo combo)
        {
            return Update(combo);
        }
    }
}
