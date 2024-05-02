namespace DMSMVC.Repository.Interface
{
    public interface IUnitOfWork
    {
        Task<int> SaveAsync();
    }
}
