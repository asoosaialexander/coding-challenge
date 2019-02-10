using LoanWebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace LoanWebApi.UnitOfWork
{
    public class UnitOfWork:IUnitOfWork,IDisposable
    {
        private LoansDbEntities _context = null;
        private LoanRepository _loanRepository;

        public UnitOfWork()
        {
            _context = new LoansDbEntities();
        }

        public LoanRepository LoanRepository
        {
            get
            {
                if (this._loanRepository == null)
                {
                    this._loanRepository = new LoanRepository(_context);
                }
                return _loanRepository;
            }
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                // TODO: LOg Exception
                throw;
            }
        }

        #region Implementing IDiosposable...

        #region private dispose variable declaration...
        private bool disposed = false;
        #endregion

        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}