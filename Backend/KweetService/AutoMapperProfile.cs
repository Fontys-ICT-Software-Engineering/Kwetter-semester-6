using AutoMapper;
using KweetService.DTOs;
using KweetService.Models;
using static System.Net.Mime.MediaTypeNames;

namespace KweetService
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<LikeKweetDTO, Like>();
            CreateMap<Like , LikeKweetDTO>();

            CreateMap<ReactionKweetDTO, ReactionKweet>();
            CreateMap<ReactionKweet, ReactionKweetDTO>();
        }



    }
}
