using Kweet.Data;
using Kweet.DTOs;
using Kweet.Models;
using Microsoft.EntityFrameworkCore;

namespace Kweet.Services.Kweet
{
    public class KweetService : IKweetService
    {
        private readonly DataContext _dataContext;

        public KweetService(DataContext context) 
        {
            _dataContext = context;
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

        public async Task<KweetDTO> postKweet(KweetDTO kweetDTO)
        {
            KweetModel post = new KweetModel(kweetDTO.Message, kweetDTO.User, DateTime.Now);

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

        public async Task<bool> deleteKweet(int id)
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

        public async Task<KweetDTO> getKweetById(int id)
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

    }


}
