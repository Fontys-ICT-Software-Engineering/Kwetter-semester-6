using KweetReadService.Services.Kweet;
using MassTransit;
using SharedClasses;
using SharedClasses.Kweet;

namespace KweetReadService.Consumers
{
    public class DeleteKweetConsumer : IConsumer<WriteDeleteKweetDTO>
    {
        private readonly IKweetReadService _kweetReadService;

        public DeleteKweetConsumer(IKweetReadService kweetReadService) 
        {
            _kweetReadService = kweetReadService;
        }

        public async Task Consume(ConsumeContext<WriteDeleteKweetDTO> context)
        {
            MassTransitResponse status = new MassTransitResponse();

            WriteDeleteKweetDTO dto = new WriteDeleteKweetDTO
            {
                Id = context.Message.Id
            };

            status.Succes = await _kweetReadService.DeleteKweet(dto);

            await context.RespondAsync(status);

        }
    }
}
