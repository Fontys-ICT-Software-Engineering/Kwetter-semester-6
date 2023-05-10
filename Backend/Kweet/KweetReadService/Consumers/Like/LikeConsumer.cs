using KweetReadService.DTOs.SharedClasses.Likes;
using KweetReadService.Services.Kweet;
using KweetReadService.Services.Like;
using MassTransit;
using SharedClasses;
using SharedClasses.Likes;

namespace KweetReadService.Consumers.Like
{
    public class LikeConsumer : IConsumer<WriteLikeDTO>
    {

        private readonly Ilikeservice _likeService;

        public LikeConsumer(Ilikeservice likeservice)
        {
            _likeService = likeservice;
        }

        public async Task Consume(ConsumeContext<WriteLikeDTO> context)
        {
            MassTransitResponse status = new MassTransitResponse();

            WriteLikeDTO dto = new WriteLikeDTO
            {
                Id = context.Message.Id,
                KweetId = context.Message.KweetId,
                UserId = context.Message.UserId,
                status = context.Message.status
            };

            status.Succes = await _likeService.LikeKweet(dto);

            await context.RespondAsync(status);
        }
    }
}
