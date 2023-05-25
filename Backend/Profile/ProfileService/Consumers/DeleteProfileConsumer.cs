using MassTransit;
using ProfileService.Services;
using Sharedclasses.RabbitMq;

namespace ProfileService.Consumers
{
    public class DeleteProfileConsumer : IConsumer<GDPRDelete>
    {

        private readonly IProfileService _profileService;

    public DeleteProfileConsumer(IProfileService profileService)
    {
        _profileService = profileService;
    }

    public async Task Consume(ConsumeContext<GDPRDelete> context)
    {
         await _profileService.GDPRDelete(context.Message.Id);
        //await Console.Out.WriteLineAsync(context.Message.Id);
    }   
  }
}
