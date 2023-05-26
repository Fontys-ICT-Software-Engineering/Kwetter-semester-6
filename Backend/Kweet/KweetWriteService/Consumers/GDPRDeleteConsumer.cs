using Kweet.Services.Kweet;
using MassTransit;
using Sharedclasses.RabbitMq;

namespace KweetWriteService.Consumers
{
    public class GDPRDeleteConsumer : IConsumer<GDPRDelete>
    {

        private readonly IKweetService kweetService;

        public GDPRDeleteConsumer(IKweetService kweet)
        {
            kweetService = kweet;
        }

        public async Task Consume(ConsumeContext<GDPRDelete> context)
        {
            await kweetService.GDPRDelete(context.Message.Id);
        }
    }
}
