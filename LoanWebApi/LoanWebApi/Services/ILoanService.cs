using System.Collections.Generic;
using LoanWebApi.Models;

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
