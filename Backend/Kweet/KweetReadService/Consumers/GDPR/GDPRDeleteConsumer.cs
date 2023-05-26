using KweetReadService.Services.Kweet;
using KweetReadService.Services.Like;
using KweetReadService.Services.Reaction;
using MassTransit;
using Sharedclasses.RabbitMq;

namespace KweetReadService.Consumers.GDPR
{
    public class GDPRDeleteConsumer : IConsumer<GDPRDelete>
    {

        private readonly IKweetReadService _kweetReadService;
        private readonly Ilikeservice _likeservice;
        private readonly IReactionService _reactionService;

        public GDPRDeleteConsumer(IKweetReadService kweetReadService, Ilikeservice ilikeservice, IReactionService reactionService)
        {
            _kweetReadService = kweetReadService;
            _likeservice = ilikeservice;    
            _reactionService = reactionService;
        }

        public async Task Consume(ConsumeContext<GDPRDelete> context)
        {
            await _kweetReadService.GDPRDelete(context.Message.Id);
            //await Console.Out.WriteLineAsync(context.Message.Id);
        }
    }
}
