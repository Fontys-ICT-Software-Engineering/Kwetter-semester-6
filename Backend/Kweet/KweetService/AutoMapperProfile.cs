using AutoMapper;
using Kweet.Models;
using KweetService.DTOs.KweetDTO;
using KweetService.DTOs.LikeDTO;
using KweetService.DTOs.ReactionDTO;
using KweetService.Models;
using SharedClasses.Kweet;
using static System.Net.Mime.MediaTypeNames;

namespace KweetService
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ReturnUpdateKweetDTO, KweetModel>();
            CreateMap<KweetModel, ReturnUpdateKweetDTO>();

            CreateMap<PostLikeKweetDTO, LikeModel>();
            CreateMap<LikeModel , PostLikeKweetDTO>();

            CreateMap<PostReactionKweetDTO, ReactionKweetModel>();
            CreateMap<ReactionKweetModel, PostReactionKweetDTO>();

            CreateMap<ReactionKweetModel, GetReactionDTO>();
            CreateMap<GetReactionDTO, ReactionKweetModel>();

            //rabbitMq automappers

            CreateMap<KweetModel, WriteKweetDTO>();
            CreateMap<KweetModel, WriteKweetUpdateDTO>();

        }



    }
}
