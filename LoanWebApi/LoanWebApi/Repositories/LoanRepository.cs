using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

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

        public virtual Loan GetByAccountNo(int accountNo)
        {
            return _dbSet.FirstOrDefault(acct => acct.AccountNo == accountNo);
        }
    }
}