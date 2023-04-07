using AutoMapper;
using Kweet.Data;
using KweetService.DTOs.LikeDTO;
using KweetService.Models;

namespace KweetService.Services.Likes
{
    public class LikeService : ILikeService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public LikeService(DataContext context, IMapper mapper)
        {
            _dataContext = context;
            _mapper = mapper;
        }

        public async Task<bool> LikeKweet(PostLikeKweetDTO dto)
        {
            PostLikeKweetDTO res = new PostLikeKweetDTO();

            try
            {
                if (!_dataContext.Kweets.Any(k => k.Id.ToString() == dto.KweetId)) throw new Exception("Kweet id does not exist");

                if (_dataContext.Likes.Any(k => k.KweetID == dto.KweetId && k.UserID == dto.UserId))
                {
                    _dataContext.Remove(_dataContext.Likes.Single(k => k.KweetID == dto.KweetId && k.UserID == dto.UserId));
                    _dataContext.SaveChanges();
                    return false;
                }

                Like like = _mapper.Map<Like>(dto);

                _dataContext.Likes.Add(like);
                _dataContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
