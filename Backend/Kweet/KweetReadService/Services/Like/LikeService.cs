using AutoMapper;
using KweetReadService.Data.Kweet;
using KweetReadService.Data.Likes;
using KweetReadService.DTOs.LikeDTO;
using KweetReadService.DTOs.SharedClasses;
using KweetReadService.DTOs.SharedClasses.Likes;
using KweetReadService.Models;
using SharedClasses.Likes;

namespace KweetReadService.Services.Like
{
    public class LikeService : Ilikeservice
    {
        private readonly ILikeMongoRepository<LikeModel> _likeRepository;
        private readonly IMapper _mapper;

        public LikeService(ILikeMongoRepository<LikeModel> context, IMapper mapper)
        {
            _likeRepository = context;
            _mapper = mapper;
        }

        public async Task<bool> LikeKweet(WriteLikeDTO dto)
        {
            try
            {
                if (dto.status == LikeStatus.CREATE)
                {
                    LikeModel model = new LikeModel
                    {
                        KweetID = dto.KweetId,
                        UserID = dto.UserId
                    };

                    await _likeRepository.InsertOneAsync(model);
                    return true;
                }
                else
                {
                   _likeRepository.FindOneAndDeleteAsync(filter => filter.UserID == dto.UserId && filter.KweetID == dto.KweetId);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            



        }

    }
}
