using CurrencyExchangeRates.Application.Commands;
using MediatR;

namespace CurrencyExchangeRates.Application
{
    public interface IBackgroundJob
    {
        Task RunJobAsync();
    }

    public class BackgroundJob : IBackgroundJob
    {
        private readonly IMediator _mediator;

        public BackgroundJob(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task RunJobAsync()
        {
            await _mediator.Send(new UpdateExchangeRateCommand());
        }
    }
}
