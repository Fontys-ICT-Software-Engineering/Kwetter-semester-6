using AutoMapper;
using Kweet.Models;
using KweetService.DTOs.KweetDTO;
using KweetService.DTOs.LikeDTO;
using KweetService.DTOs.ReactionDTO;
using KweetService.Models;
using static System.Net.Mime.MediaTypeNames;

namespace KweetService
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ReturnUpdateKweetDTO, KweetModel>();
            CreateMap<KweetModel, ReturnUpdateKweetDTO>();

            CreateMap<PostLikeKweetDTO, Like>();
            CreateMap<Like , PostLikeKweetDTO>();

            CreateMap<PostReactionKweetDTO, ReactionKweet>();
            CreateMap<ReactionKweet, PostReactionKweetDTO>();

            CreateMap<ReactionKweet, GetReactionDTO>();
            CreateMap<GetReactionDTO, ReactionKweet>();


        }



    }
}
