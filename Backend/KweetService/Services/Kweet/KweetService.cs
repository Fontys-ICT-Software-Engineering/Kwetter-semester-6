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

        public async Task<List<ReturnKweetDTO>> getAllKweets()
        {
            List<ReturnKweetDTO> response = new List<ReturnKweetDTO>();

            try
            {
                List<KweetModel> kweets = await _dataContext.Kweets.ToListAsync();

                foreach (KweetModel model in kweets)
                {
                    bool liked = await isLikedByUser(model.User);
                    int likes = await getLikesByKweet(model.Id.ToString());

                    response.Add(new ReturnKweetDTO(
                        model.Id,
                        model.Message,
                        model.User,
                        model.Date,
                        model.IsEdited,
                        likes,
                        liked
                        ));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
        }

        public async Task<ReturnUpdateKweetDTO> UpdateKweet(PostUpdateKweetDTO dto)
        {
            ReturnUpdateKweetDTO response = new();
            try
            {
                KweetModel model = await _dataContext.Kweets.SingleAsync(k => k.Id == dto.Id);

                //check of het meegegeven userId match met de originele tweeter
                if (model.User != dto.User) throw new Exception();

                model.Message = dto.Message;
                model.IsEdited = true;

                _dataContext.Kweets.Update(model);
                _dataContext.SaveChanges();

               return response = _mapper.Map<ReturnUpdateKweetDTO>(model);
            }
            catch (Exception ex)
            {
                return response;
            }
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

        public async Task<ReturnKweetDTO> getKweetById(Guid id)
        {
            try
            {
                KweetModel res = _dataContext.Kweets.Single(k => k.Id == id);
                bool liked = false;
                int likes = await getLikesByKweet(id.ToString());




                return new ReturnKweetDTO(res.Id, res.Message, res.User, res.Date, res.IsEdited, likes, liked);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        private async Task<int> getLikesByKweet(string KweetID)
        {
            try
            {
                return await _dataContext.Likes.Where(i => i.KweetID == KweetID).CountAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private async Task<bool> isLikedByUser(string UserID)
        {
            try
            {
                if(await _dataContext.Likes.AnyAsync(k => k.UserID == UserID)) return true;
            }
            catch (Exception ex)
            {
                throw;
            }
            return false;
        }

        



    }


}
