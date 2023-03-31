using MassTransit;
using AuthService.DTOs;

namespace ProfileService.Models.RabbitMq
{
    internal class ProfileConsumer : IConsumer<RegisterUserDTO>
    {
        public async Task Consume(ConsumeContext<RegisterUserDTO> context)
        {
            await Console.Out.WriteLineAsync(context.Message.Id);
        }
    }
}
