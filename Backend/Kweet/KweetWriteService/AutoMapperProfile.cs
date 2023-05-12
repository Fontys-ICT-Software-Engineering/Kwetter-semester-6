using AutoMapper;
using Kweet.Models;
using KweetWriteService.DTOs.KweetDTO;
using KweetWriteService.DTOs.LikeDTO;
using KweetWriteService.DTOs.ReactionDTO;
using KweetWriteService.Models;
using SharedClasses.Kweet;
using SharedClasses.Reaction;
using static System.Net.Mime.MediaTypeNames;

namespace KweetWriteService
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

            CreateMap<ReactionKweetModel, WriteCreateReactionKweet>();
            CreateMap<ReactionKweetModel, WriteDeleteReactionKWeet>();




        }



    }
}
