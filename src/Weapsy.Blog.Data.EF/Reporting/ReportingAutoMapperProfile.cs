using AutoMapper;
using Weapsy.Blog.Data.EF.Entities;
using Weapsy.Blog.Reporting.Blogs;

namespace Weapsy.Blog.Data.EF.Reporting
{
    public class ReportingAutoMapperProfile : Profile
    {
        public ReportingAutoMapperProfile()
        {
            CreateMap<BlogEntity, BlogViewModel>();
        }
    }
}
