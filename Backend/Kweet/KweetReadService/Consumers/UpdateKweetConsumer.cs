using KweetReadService.Services.Kweet;
using MassTransit;
using SharedClasses;
using SharedClasses.Kweet;
using SharedClasses.Reaction;

namespace KweetReadService.Consumers
{
    public class UpdateKweetConsumer : IConsumer<WriteKweetUpdateDTO>
    {
        private readonly IKweetReadService _kweetReadService;

        public UpdateKweetConsumer(IKweetReadService kweetReadService) 
        {
            _kweetReadService = kweetReadService;       
        }

        public async Task Consume(ConsumeContext<WriteKweetUpdateDTO> context)
        {
            MassTransitResponse status = new MassTransitResponse();

            WriteKweetUpdateDTO dto = new WriteKweetUpdateDTO
            {
                Id = context.Message.Id,
                User = context.Message.User,
                Message = context.Message.Message,
                IsEdited = context.Message.IsEdited,
            };

            status.Succes = await _kweetReadService.UpdateKweet(dto);
            await context.RespondAsync(status);
 
        }
    }
}
