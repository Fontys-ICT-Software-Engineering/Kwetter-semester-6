using AutoMapper;
using Kweet.Data;
using Kweet.Models;
using KweetService.DTOs;
using KweetService.DTOs.KweetDTO;
using KweetService.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SharedClasses;
using SharedClasses.Kweet;

namespace Kweet.Services.Kweet
{
    public class KweetService : IKweetService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IRequestClient<WriteKweetDTO> _writeKweetClient;
        private readonly IRequestClient<WriteDeleteKweetDTO> _writeDeleteKweetClient;
        private readonly IRequestClient<WriteKweetUpdateDTO> _writeUpdateKweetClient;


        public KweetService(DataContext context, IMapper mapper, IRequestClient<WriteKweetDTO> writeKweetClient, IRequestClient<WriteDeleteKweetDTO> writeDeleteKweetClient, IRequestClient<WriteKweetUpdateDTO> writeUpdateKweetClient) 
        {
            _dataContext = context;
            _mapper = mapper;
            _writeDeleteKweetClient = writeDeleteKweetClient;
            _writeKweetClient = writeKweetClient;
            _writeUpdateKweetClient = writeUpdateKweetClient;   
        }

        //for testing
        public KweetService(DataContext context)
        {
            _dataContext = context;
        }

        public async Task<List<ReturnKweetDTO>> getAllKweets(string userId)
        {
            List<ReturnKweetDTO> response = new List<ReturnKweetDTO>();

            try
            {
                List<KweetModel> kweets = await _dataContext.Kweets.ToListAsync();

                foreach (KweetModel model in kweets)
                {
                    bool liked = await isLikedByUser(userId, model.Id.ToString());
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
                response = response.OrderByDescending(d => d.Date).ToList();
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
                if (model.User != dto.User) throw new Exception("users do not match");

                model.Message = dto.Message;
                model.IsEdited = true;

                _dataContext.Kweets.Update(model);
                _dataContext.SaveChanges();

                WriteKweetUpdateDTO rabbit = _mapper.Map<WriteKweetUpdateDTO>(model);

                var status = await _writeUpdateKweetClient.GetResponse<MassTransitResponse>(rabbit);

                if (!status.Message.Succes) throw new Exception("Failed to synchronize databases");

               return response = _mapper.Map<ReturnUpdateKweetDTO>(model);
            }
            catch(InvalidOperationException ex)
            {
                throw new Exception("Kweet ID not found");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<PostKweetDTO> postKweet(PostKweetDTO kweetDTO)
        {
            KweetModel post = new KweetModel(kweetDTO.Message, kweetDTO.User);

            try
            {
                _dataContext.Kweets.Add(post);
                _dataContext.SaveChanges();

                WriteKweetDTO rabbit = _mapper.Map<WriteKweetDTO>(post);
                var status = await _writeKweetClient.GetResponse<MassTransitResponse>(rabbit);

                if(!status.Message.Succes)
                {
                    _dataContext.Remove(_dataContext.Kweets.Single(k => k.Id == post.Id));
                    throw new Exception("Failed To Synchronize databases");
                }
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
                var status = await _writeDeleteKweetClient.GetResponse<MassTransitResponse>(new WriteDeleteKweetDTO { Id = id });
                if (!status.Message.Succes) throw new Exception("failed to synchronize databases");

                //remove Kweet
                _dataContext.Remove(_dataContext.Kweets.Single(k => k.Id == id));

                //remove likes
                await _dataContext.Likes.Where(k => k.KweetID == id.ToString()).ExecuteDeleteAsync();

                //remove comments            
                await _dataContext.Reactions.Where(k => k.KweetId == id.ToString()).ExecuteDeleteAsync();

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

        private async Task<bool> isLikedByUser(string UserID, string KweetID)
        {
            try
            {
                if(await _dataContext.Likes.AnyAsync(k => k.UserID == UserID && k.KweetID == KweetID)) return true;
            }
            catch (Exception ex)
            {
                throw;
            }
            return false;
        }
     

    }

}
