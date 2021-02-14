using AutoMapper;
using Blog.WebApi.Entities;
using Blog.WebApi.Models.Posts;
using Blog.WebApi.Models.Users;
using System.Data;

namespace Blog.WebApi.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<IDataReader, User>();
            CreateMap<IDataReader, Post>();
            CreateMap<RegisterModel, User>();
            CreateMap<PostCreateModel, Post>();
            CreateMap<PostUpdateModel, Post>();
        }
    }
}
