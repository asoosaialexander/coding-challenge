using LoanWebApi.Models;
using AutoMapper;

namespace LoanWebApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Loan, LoanModel>();
        }
    }
}