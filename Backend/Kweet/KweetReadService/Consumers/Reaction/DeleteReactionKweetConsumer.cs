using KweetReadService.Services.Kweet;
using KweetReadService.Services.Reaction;
using MassTransit;
using SharedClasses;
using SharedClasses.Reaction;

namespace KweetReadService.Consumers.Reaction
{
    public class DeleteReactionKweetConsumer : IConsumer<WriteDeleteReactionKWeet>
    {
        private readonly IReactionService _reactionService;

        public DeleteReactionKweetConsumer(IReactionService reactionService)
        {
            _reactionService = reactionService;
        }

        public async Task Consume(ConsumeContext<WriteDeleteReactionKWeet> context)
        {
            MassTransitResponse status = new MassTransitResponse();

            status.Succes = _reactionService.DeleteReactionKweet(context.Message.Id);
            await context.RespondAsync(status);
        }
    }
}
