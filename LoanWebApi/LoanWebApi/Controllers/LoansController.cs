using LoanWebApi.Models;
using LoanWebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace LoanWebApi.Controllers
{
    [EnableCors("*","*","*")]
    public class LoansController : ApiController
    {
        private readonly ILoanService _loanService;

        public LoansController()
        {
            _loanService = new LoanService();
        }

        public LoansController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        // GET: api/Loans
        public HttpResponseMessage Get()
        {
            var loans = _loanService.GetAllLoans();
            if (loans != null)
            {
                var loanList = loans as List<LoanModel> ?? loans.ToList();
                if (loanList.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, loanList);
                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Loans not found");
        }

        // GET: api/Loans/5
        public HttpResponseMessage Get(int accountNo)
        {
            var loan = _loanService.GetLoanByAccountNo(accountNo);
            if (loan != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, loan);
            }
            throw new Exception("No loan found for this Account No"); 
        }
    }
}
