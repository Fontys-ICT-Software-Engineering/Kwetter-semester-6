using AutoMapper;
using KweetReadService.DTOs.KweetDTO;
using KweetReadService.DTOs.LikeDTO;
using KweetReadService.DTOs.ReactionDTO;
using KweetReadService.Models;

namespace KweetReadService
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ReturnUpdateKweetDTO, KweetModel>();
            CreateMap<KweetModel, ReturnUpdateKweetDTO>();

            CreateMap<ReturnKweetDTO, KweetModel>();
            CreateMap<IEnumerable<KweetModel>, ReturnKweetDTO>();

            CreateMap<PostLikeKweetDTO, LikeModel>();
            CreateMap<LikeModel, PostLikeKweetDTO>();

            CreateMap<PostReactionKweetDTO, ReactionKweetModel>();
            CreateMap<ReactionKweetModel, PostReactionKweetDTO>();

            CreateMap<ReactionKweetModel, GetReactionDTO>();
            CreateMap<GetReactionDTO, ReactionKweetModel>();


        }



    }
}
