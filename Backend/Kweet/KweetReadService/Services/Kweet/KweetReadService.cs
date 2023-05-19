using AutoMapper;
using KweetReadService.Data.Kweet;
using KweetReadService.DTOs.KweetDTO;
using KweetReadService.Models;
using KweetReadService.Services.Like;
using KweetReadService.Services.Reaction;
using SharedClasses.Kweet;

namespace KweetReadService.Services.Kweet
{
    public class KweetReadService : IKweetReadService
    {
        private readonly IKweetMongoRepository<KweetModel> _kweetRepository;
        private readonly IMapper _mapper;
        private readonly IReactionService _reactionService;
        private readonly Ilikeservice _likeService;


        public KweetReadService(IKweetMongoRepository<KweetModel> mongoRepository, IMapper mapper, Ilikeservice likeservice, IReactionService reactionService)
        {
            _kweetRepository = mongoRepository;
            _mapper = mapper;
            _likeService = likeservice;
            _reactionService = reactionService;
        }

        public async Task<List<ReturnKweetDTO>> GetAllKweets(string userId)
        {
            try
            {
                IEnumerable<KweetModel> kweets = _kweetRepository.FilterBy(filter => filter.User == userId);

                List<ReturnKweetDTO> response = new List<ReturnKweetDTO>();

                
                foreach (KweetModel kweet in kweets) 
                {
                    response.Add(
                        new ReturnKweetDTO
                        {
                            Id = kweet.Id,
                            Message = kweet.Message,
                            User = kweet.User,
                            Date = kweet.Date,
                            IsEdited = kweet.IsEdited,
                            Liked = await IsLikedByUser(kweet.Id, userId),
                            Likes = (int)await GetLikesByKweet(kweet.Id)
                        }
                    );   
                }
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> PostKweet(WriteKweetDTO kweetDTO)
        {
            try
            {
                KweetModel post = new KweetModel
                {
                    Id = kweetDTO.Id.ToString(),
                    Message = kweetDTO.Message,
                    IsEdited = kweetDTO.IsEdited,
                    Date = kweetDTO.Date,
                    User = kweetDTO.User
                };
                
                await _kweetRepository.InsertOneAsync(post);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        public async Task<bool> UpdateKweet(WriteKweetUpdateDTO dto)
        {
            ReturnUpdateKweetDTO response = new();
            try
            {
                KweetModel oldKweet = await _kweetRepository.FindByIdAsync(dto.Id.ToString());

                //check of het meegegeven userId match met de originele tweeter
                if (oldKweet.User != dto.User) throw new Exception("users do not match");

                oldKweet.Message = dto.Message;
                oldKweet.IsEdited = true;

                await _kweetRepository.ReplaceOneAsync(oldKweet);

                return true;
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Kweet ID not found");
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteKweet(WriteDeleteKweetDTO dto)
        {
            try
            {
                //remove Kweet
                await _kweetRepository.DeleteByIdAsync(dto.Id.ToString());
           
                //remove likes
                //await _dataContext.Likes.Where(k => k.KweetID == id.ToString()).ExecuteDeleteAsync();

                //remove comments            
                //await _dataContext.Reactions.Where(k => k.KweetId == id.ToString()).ExecuteDeleteAsync();

                //_dataContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<ReturnKweetDTO> GetKweetById(Guid id)
        {
            try
            {
                KweetModel res = _kweetRepository.FindById(id.ToString());
                bool liked = false;
                //int likes = await getLikesByKweet(id.ToString());
                int likes = 1;

                return new ReturnKweetDTO(res.Id, res.Message, res.User, res.Date, res.IsEdited, likes, liked);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<long> GetLikesByKweet(string kweetID)
        {
            try
            {
                return await _likeService.ReturnLikes(kweetID);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private async Task<bool> IsLikedByUser(string UserID, string KweetID)
        {
            try
            {
                return await _likeService.IsLikedByUser(KweetID, UserID);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
