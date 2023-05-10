using KweetReadService.Services.Kweet;
using MassTransit;
using SharedClasses.Kweet;
using SharedClasses;

namespace KweetReadService.Consumers
{
    public class CreateKweetConsumer : IConsumer<WriteKweetDTO>
    {
        private readonly IKweetReadService _kweetReadService;

        public CreateKweetConsumer(IKweetReadService kweetReadService)
        {
            _kweetReadService = kweetReadService;
        }

        public async Task Consume(ConsumeContext<WriteKweetDTO> context)
        {
            MassTransitResponse status = new MassTransitResponse();

            WriteKweetDTO dto = new WriteKweetDTO
            {
                Id = context.Message.Id,
                Message = context.Message.Message,
                User = context.Message.User,
                Date = context.Message.Date,
                IsEdited = context.Message.IsEdited,                             
            };

            status.Succes = await _kweetReadService.PostKweet(dto);


            await context.RespondAsync(status);         
        }

    }
}
