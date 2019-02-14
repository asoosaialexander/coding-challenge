using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LoanWebApi.Repositories
{
    public class LoanRepository
    {
        internal LoansDbEntities _context;
        internal DbSet<Loan> _dbSet;

        public LoanRepository(LoansDbEntities context)
        {
            this._context = context;
            this._dbSet = _context.Set<Loan>();
        }

        public virtual List<Loan> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual Loan GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual Loan GetByAccountNo(int accountNo)
        {
            return _dbSet.FirstOrDefault(acct => acct.AccountNo == accountNo);
        }

        public virtual void Insert(Loan loan)
        {
            _dbSet.Add(loan);
        }

        public virtual void Delete(int id)
        {
            Loan loanToDelete = _dbSet.Find(id);
            Delete(loanToDelete);
        }

        public virtual void Delete(Loan loanToDelete)
        {
            if (_context.Entry(loanToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(loanToDelete);
            }
            _dbSet.Remove(loanToDelete);
        }

        public virtual void Update(Loan loanToUpdate)
        {
            _dbSet.Attach(loanToUpdate);
            _context.Entry(loanToUpdate).State = EntityState.Modified;
        }
    }
}