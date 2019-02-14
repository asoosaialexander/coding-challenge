using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using AutoMapper;
using LoanWebApi.Models;

namespace LoanWebApi.Services
{
    public class LoanService : ILoanService
    {
        private readonly UnitOfWork.IUnitOfWork _unitOfWork;
        public LoanService()
        {
            _unitOfWork = new UnitOfWork.UnitOfWork();
        }
        public LoanService(UnitOfWork.IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

        public LoanModel GetLoanById(int id)
        {
            var loan = _unitOfWork.LoanRepository.GetById(id);
            if (loan != null)
            {
                var loanModel = Mapper.Map<LoanModel>(loan);
                return loanModel;
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

        public int CreateLoan(LoanModel loanModel)
        {
            using (var scope = new TransactionScope())
            {
                var loan = new Loan
                {
                    AccountName = loanModel.AccountName,
                    AccountNo=loanModel.AccountNo,
                    Balance=loanModel.Balance,
                    Interest=loanModel.Interest,
                    EarlyPaymentFee=loanModel.EarlyPaymentFee,
                    Carryover=loanModel.Carryover
                };
                _unitOfWork.LoanRepository.Insert(loan);
                _unitOfWork.Save();
                scope.Complete();
                return loan.Id;
            }
        }

        public bool UpdateLoan(int loanId, LoanModel loanModel)
        {
            var success = false;
            if (loanModel != null)
            {
                using (var scope = new TransactionScope())
                {
                    var loan = _unitOfWork.LoanRepository.GetById(loanId);
                    if (loan != null)
                    {
                        loan.AccountName = loanModel.AccountName;
                        loan.AccountNo = loanModel.AccountNo;
                        loan.Balance = loanModel.Balance;
                        loan.Interest = loanModel.Interest;
                        loan.EarlyPaymentFee = loanModel.EarlyPaymentFee;
                        loan.Carryover = loanModel.Carryover;

                        _unitOfWork.LoanRepository.Update(loan);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public bool DeleteLoan(int id)
        {
            var success = false;
            if (id > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var loan = _unitOfWork.LoanRepository.GetById(id);
                    if (loan != null)
                    {

                        _unitOfWork.LoanRepository.Delete(loan);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }
    }
}