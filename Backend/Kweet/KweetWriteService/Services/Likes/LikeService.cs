using AutoMapper;
using Kweet.Data;
using KweetWriteService.DTOs.LikeDTO;
using KweetWriteService.Models;
using MassTransit;
using MassTransit.Clients;
using SharedClasses;
using SharedClasses.Likes;

namespace KweetWriteService.Services.Likes
{
    public class LikeService : ILikeService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IRequestClient<WriteLikeDTO> _client;


        public LikeService(DataContext context, IMapper mapper, IRequestClient<WriteLikeDTO> writeLikeDTO)
        {
            _dataContext = context;
            _mapper = mapper;
            _client = writeLikeDTO;
        }

        public async Task<bool> LikeKweet(PostLikeKweetDTO dto)
        {
            PostLikeKweetDTO res = new PostLikeKweetDTO();
            LikeStatus likeStatus;
            bool status = false;
            try
            {
                if (!_dataContext.Kweets.Any(k => k.Id.ToString() == dto.KweetId)) throw new Exception("Kweet id does not exist");

                if (_dataContext.Likes.Any(k => k.KweetID == dto.KweetId && k.UserID == dto.UserId))
                {
                    _dataContext.Remove(_dataContext.Likes.Single(k => k.KweetID == dto.KweetId && k.UserID == dto.UserId));
                    _dataContext.SaveChanges();
                    likeStatus = LikeStatus.DELETE;
                }
                else
                {
                    LikeModel like = _mapper.Map<LikeModel>(dto);
                    _dataContext.Likes.Add(like);
                    _dataContext.SaveChanges();
                    likeStatus = LikeStatus.CREATE;
                    status = true;
                }

                WriteLikeDTO rabbit = new WriteLikeDTO
                {
                    KweetId = dto.KweetId,
                    UserId = dto.UserId,
                    Status = likeStatus
                };

                var response = await _client.GetResponse<MassTransitResponse>(rabbit);

                if (!response.Message.Succes) throw new Exception();

                return status;
            }
            catch (Exception)
            {
                return false;
                throw;
            }     
        }

        public List<LikeModel> GetLikesPerKweet()
        {
            return _dataContext.Likes.ToList(); 
        }

        public void DeleteLike(Guid id)
        {
            _dataContext.Likes.Remove(_dataContext.Likes.Single(x => x.Id == id));
            _dataContext.SaveChanges();
        }


    }
}
