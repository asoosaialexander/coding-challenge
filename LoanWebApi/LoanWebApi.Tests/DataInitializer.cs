using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanWebApi.Tests
{
    public class DataInitializer
    {
        public static List<Loan> GetAllLoans()
        {
            var loans = new List<Loan>()
            {
                new Loan(){AccountName="Placeat autem quas",AccountNo=415593955,
                Balance=1927,Interest=376,EarlyPaymentFee=76,Carryover=2379},
                new Loan(){AccountName="Quo voluplate", AccountNo=549442240,
                Balance=1138,Interest=674,EarlyPaymentFee=45,Carryover=1857}
            };

            return loans;
        }
    }
}
