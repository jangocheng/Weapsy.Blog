using AutoMapper;
using Weapsy.Blog.Data.Entities;
using Weapsy.Blog.Reporting.Models;

namespace Weapsy.Blog.Data.Reporting
{
    public class ReportingAutoMapperProfile : Profile
    {
        public ReportingAutoMapperProfile()
        {
            CreateMap<BlogEntity, BlogSettings>();
        }
    }
}
