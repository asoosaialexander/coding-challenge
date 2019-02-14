using LoanWebApi.Repositories;

namespace LoanWebApi.UnitOfWork
{
    public interface IUnitOfWork
    {
        LoanRepository LoanRepository { get; }
        void Save();
    }
}