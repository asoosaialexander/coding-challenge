using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using AutoMapper;
using Moq;
using LoanWebApi.Models;
using LoanWebApi.Repositories;
using LoanWebApi.Services;

namespace LoanWebApi.Tests.Services
{
    [TestFixture]
    class LoanServiceTests
    {
        private ILoanService _loanService;
        private UnitOfWork.IUnitOfWork _unitOfWork;
        private List<Loan> _loans;
        private LoanRepository _loanRepository;
        private LoansDbEntities _dbEntities;

        [OneTimeSetUp]
        public void Setup()
        {
            _loans = SetUpLoans();
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperProfile>());

        }

        private static List<Loan> SetUpLoans()
        {
            var loanId = new int();
            var loans = DataInitializer.GetAllLoans();
            foreach (Loan loan in loans)
                loan.Id = ++loanId;
            return loans;

        }

        [SetUp]
        public void ReInitializeTest()
        {
            _dbEntities = new Mock<LoansDbEntities>().Object;
            _loanRepository = SetUpLoanRepository();
            var unitOfWork = new Mock<UnitOfWork.IUnitOfWork>();
            unitOfWork.SetupGet(s => s.LoanRepository).Returns(_loanRepository);
            _unitOfWork = unitOfWork.Object;
            _loanService = new LoanService(_unitOfWork);
        }

        private LoanRepository SetUpLoanRepository()
        {
            // Initialise repository
            var mockRepo = new Mock<LoanRepository>(MockBehavior.Default, _dbEntities);

            // Setup mocking behavior
            mockRepo.Setup(l => l.GetAll()).Returns(_loans);

            mockRepo.Setup(p => p.GetByAccountNo(It.IsAny<int>()))
            .Returns(new Func<int, Loan>(
            id => _loans.Find(p => p.AccountNo.Equals(id))));

            mockRepo.Setup(p => p.GetById(It.IsAny<int>()))
            .Returns(new Func<int, Loan>(
            id => _loans.Find(p => p.Id.Equals(id))));

            mockRepo.Setup(p => p.Insert((It.IsAny<Loan>())))
            .Callback(new Action<Loan>(newLoan =>
            {
                dynamic maxLoanID = _loans.Last().Id;
                dynamic nextLoanID = maxLoanID + 1;
                newLoan.Id = nextLoanID;
                _loans.Add(newLoan);
            }));

            mockRepo.Setup(p => p.Update(It.IsAny<Loan>()))
            .Callback(new Action<Loan>(loan =>
            {
                var oldLoan = _loans.Find(a => a.Id == loan.Id);
                oldLoan = loan;
            }));

            mockRepo.Setup(p => p.Delete(It.IsAny<Loan>()))
            .Callback(new Action<Loan>(loan =>
            {
                var LoanToRemove = _loans.Find(a => a.Id == loan.Id);

                if (LoanToRemove != null)
                    _loans.Remove(LoanToRemove);
            }));

            // Return mock implementation object
            return mockRepo.Object;
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(20)]
        public void NullTestingForGetLoanById(int input)
        {
            var loan = _loanService.GetLoanById(input);
            Assert.Null(loan);
        }

        [TestCase("New Account", 1, "New Account")]
        [TestCase("Another New Account", 1, "Another New Account")]
        public void UpdateLoanTest(string inputAccountNm, int expectedLoanId,
            string expectedAccountNm)
        {
            var firstLoan = _loans.First();
            firstLoan.AccountName = inputAccountNm;
            var updatedLoan = new LoanModel()
            {
                Id = firstLoan.Id,
                AccountName = firstLoan.AccountName,
                AccountNo = firstLoan.AccountNo,
                Balance = firstLoan.Balance,
                Interest = firstLoan.Interest,
                EarlyPaymentFee = firstLoan.EarlyPaymentFee,
                Carryover = firstLoan.Carryover
            };
            _loanService.UpdateLoan(firstLoan.Id, updatedLoan);
            Assert.That(firstLoan.Id, Is.EqualTo(expectedLoanId)); // hasn't changed
            Assert.That(firstLoan.AccountName, Is.EqualTo(expectedAccountNm)); // Loan name changed
        }

        [Test]
        public void DeleteLoanTest()
        {
            int maxID = _loans.Max(a => a.Id); // Before removal
            var lastLoan = _loans.Last();

            // Remove last Loan
            _loanService.DeleteLoan(lastLoan.Id);
            Assert.That(maxID, Is.GreaterThan(_loans.Max(a => a.Id)));
        }


        [OneTimeTearDown]
        public void DisposeAllObjects()
        {
            _loans = null;
        }
    }
}
