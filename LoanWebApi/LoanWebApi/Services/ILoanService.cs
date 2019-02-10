using LoanWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanWebApi.Services
{
    public interface ILoanService
    {
        List<LoanModel> GetAllLoans();
        LoanModel GetLoanById(int id);
        LoanModel GetLoanByAccountNo(int accountNo);
        int CreateLoan(LoanModel loanModel);
        bool UpdateLoan(int loanId, LoanModel loanModel);
        bool DeleteLoan(int id);
    }
}
