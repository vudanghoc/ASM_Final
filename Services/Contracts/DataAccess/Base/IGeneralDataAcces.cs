namespace Services.Contracts.DataAccess.Base
{
    public interface IGeneralDataAcces<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);
        TEntity Update(TEntity entity);
        bool Delete(TEntity entity);

    }
}
