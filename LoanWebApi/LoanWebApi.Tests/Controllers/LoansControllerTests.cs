using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using AutoMapper;
using LoanWebApi.Controllers;
using LoanWebApi.Repositories;
using LoanWebApi.Services;
using Newtonsoft.Json;
using NUnit.Framework;
using Moq;

namespace LoanWebApi.Tests.Controllers
{
    [TestFixture]
    class LoansControllerTests
    {
        private ILoanService _loanService;
        private UnitOfWork.IUnitOfWork _unitOfWork;
        private List<Loan> _loans;
        private LoanRepository _loanRepository;
        private LoansDbEntities _dbEntities;
        private HttpClient _client;

        private HttpResponseMessage _response;
        private const string ServiceBaseURL = "http://localhost:58755/api";

        [OneTimeSetUp]
        public void Initialize()
        {
            Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperProfile>());
        }

        [SetUp]
        public void Setup()
        {
            _loans = SetUpLoans();
            _dbEntities = new Mock<LoansDbEntities>().Object;
            _loanRepository = SetUpLoanRepository();
            var unitOfWork = new Mock<UnitOfWork.IUnitOfWork>();
            unitOfWork.SetupGet(s => s.LoanRepository).Returns(_loanRepository);
            _unitOfWork = unitOfWork.Object;
            _loanService = new LoanService(_unitOfWork);
            _client = new HttpClient { BaseAddress = new Uri(ServiceBaseURL) };
        }

        private static List<Loan> SetUpLoans()
        {
            var loanId = new int();
            var loans = DataInitializer.GetAllLoans();
            foreach (Loan loan in loans)
                loan.Id = ++loanId;
            return loans;
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

        [TestCase]
        public void GetAllLoansTest()
        {
            var loansController = new LoansController(_loanService)
            {
                Request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(ServiceBaseURL + "loans")
                }
            };
            loansController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            _response = loansController.Get();

            var responseResult = JsonConvert.DeserializeObject<List<Loan>>(_response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(responseResult.Any(), true);
        }
        
        [TestCase(415593955, 415593955)]
        [TestCase(549442240, 549442240)]
        public void GetLoanByAccountNo(int input, int expected)
        {
            var loansController = new LoansController(_loanService)
            {
                Request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(ServiceBaseURL + "loans/" + input)
                }
            };
            loansController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            _response = loansController.Get(input);

            var responseResult = JsonConvert.DeserializeObject<Loan>(_response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
            Assert.AreEqual(responseResult.AccountName, _loans.Find(a => a.AccountNo == expected).AccountName);
        }

        [TestCase(111111111)]
        [TestCase(222222222)]
        public void LoanByAccountNoIsNotFound(int input)
        {
            var loansController = new LoansController(_loanService)
            {
                Request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(ServiceBaseURL + "loans/" + input)
                }
            };
            loansController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            _response = loansController.Get(input);

            var responseResult = JsonConvert.DeserializeObject<Loan>(_response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(HttpStatusCode.NotFound, _response.StatusCode);
            Assert.False(_loans.Any(a => a.AccountNo == input));
        }

        [OneTimeTearDown]
        public void DisposeAllObjects()
        {
            _loanService = null;
            _unitOfWork = null;
            _loanRepository = null;
            _loans = null;
            if (_response != null)
                _response.Dispose();
            if (_client != null)
                _client.Dispose();
        }
    }
}
