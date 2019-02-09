using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LoanWebApi.Models;

namespace LoanWebApi.Services
{
    public class LoanService : ILoanService
    {
        private readonly UnitOfWork.UnitOfWork _unitOfWork;
        public LoanService()
        {
            _unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public List<LoanModel> GetAllLoans()
        {
            var loans = _unitOfWork.LoanRepository.GetAll();
            if (loans.Any())
            {
                var loansModel = Mapper.Map<List<LoanModel>>(loans);
                return loansModel;
            }
            return null;
        }

        public LoanModel GetLoanByAccountNo(int accountNo)
        {
            var loan = _unitOfWork.LoanRepository.GetByAccountNo(accountNo);
            if (loan != null)
            {
                var loanModel = Mapper.Map<LoanModel>(loan);
                return loanModel;
            }
            return null;
        }
    }
}