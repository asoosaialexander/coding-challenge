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
        LoanModel GetLoanByAccountNo(int accountNo);
    }
}
