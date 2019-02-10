using LoanWebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoanWebApi.UnitOfWork
{
    public interface IUnitOfWork
    {
        LoanRepository LoanRepository { get; }
        void Save();
    }
}