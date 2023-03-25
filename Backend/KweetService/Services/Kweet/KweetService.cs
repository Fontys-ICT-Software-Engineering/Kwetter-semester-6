using AutoMapper;
using Kweet.Data;
using Kweet.Models;
using KweetService.DTOs;
using KweetService.DTOs.KweetDTO;
using KweetService.Models;
using Microsoft.EntityFrameworkCore;

namespace Kweet.Services.Kweet
{
    public class KweetService : IKweetService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public KweetService(DataContext context, IMapper mapper) 
        {
            _dataContext = context;
            _mapper = mapper;
        }

        public async Task<List<KweetDTO>> getAllKweets()
        {
            List<KweetDTO> response = new List<KweetDTO>();

            try
            {
                List<KweetModel> kweets = await _dataContext.Kweets.ToListAsync();

                foreach (KweetModel model in kweets)
                {
                    response.Add(new KweetDTO(
                        model.Id,
                        model.Message,
                        model.User,
                        model.Date
                        ));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
        }

        public async Task<PostKweetDTO> postKweet(PostKweetDTO kweetDTO)
        {
            KweetModel post = new KweetModel(kweetDTO.Message, kweetDTO.User);

            try
            {
                _dataContext.Kweets.Add(post);
                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
            return kweetDTO;
        }

        public async Task<bool> deleteKweet(Guid id)
        {
            try
            {
                _dataContext.Remove(_dataContext.Kweets.Single(k => k.Id == id));
                _dataContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<KweetDTO> getKweetById(Guid id)
        {
            try
            {
                KweetModel res = _dataContext.Kweets.Single(k => k.Id == id);

                return new KweetDTO(res.Id, res.Message, res.User, res.Date);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<LikeKweetDTO> LikeKweet(LikeKweetDTO dto)
        {
            LikeKweetDTO res = new LikeKweetDTO();

            try
            {
                if(_dataContext.Likes.Any(k => k.KweetID == dto.KweetId && k.UserID == dto.UserId))
                {
                    _dataContext.Remove(_dataContext.Likes.Single(k => k.KweetID == dto.KweetId && k.UserID == dto.UserId));
                    _dataContext.SaveChanges();
                    return res;
                }

                Like like = _mapper.Map<Like>(dto);

                _dataContext.Likes.Add(like);
                _dataContext.SaveChanges();

                return res;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<ReactionKweetDTO> ReactionKweet(ReactionKweetDTO dto)
        {
            try
            {
                ReactionKweet reaction = new ReactionKweet(dto.KweetId, dto.UserId, dto.Message);
                _dataContext.Reactions.Add(reaction);
                await _dataContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw;
            }

            return dto;
        }



    }


}
