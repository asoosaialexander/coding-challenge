using AutoMapper;
using LoanWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoanWebApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<List<Loan>, List<LoanModel>>();
            CreateMap<Loan, LoanModel>();
        }
    }
}