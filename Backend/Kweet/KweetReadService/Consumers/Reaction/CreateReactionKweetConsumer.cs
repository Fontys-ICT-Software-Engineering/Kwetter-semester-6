using KweetReadService.DTOs.ReactionDTO;
using KweetReadService.Services.Reaction;
using MassTransit;
using SharedClasses;
using SharedClasses.Reaction;

namespace KweetReadService.Consumers.Reaction
{
    public class CreateReactionKweetConsumer : IConsumer<WriteCreateReactionKweet>
    {
        private readonly IReactionService _reactionService;

        public CreateReactionKweetConsumer(IReactionService reactionService)
        {
            _reactionService = reactionService;
        }

        public async Task Consume(ConsumeContext<WriteCreateReactionKweet> context)
        {
            MassTransitResponse status = new MassTransitResponse();

            WriteCreateReactionKweet dto = new WriteCreateReactionKweet
            {
                Id = context.Message.Id,
                KweetId = context.Message.KweetId,
                UserId = context.Message.UserId,
                Message = context.Message.Message
            };

            status.Succes = await _reactionService.CreateReactionKweet(dto);

            await context.RespondAsync(status);
        }
    }
}
