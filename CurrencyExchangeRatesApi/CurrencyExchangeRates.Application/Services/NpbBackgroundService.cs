using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace CurrencyExchangeRates.Application.Services
{
    public class MyJob : IJob
    {
        private readonly IServiceProvider _serviceProvider;

        public MyJob(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var exchangeRatesService = scope.ServiceProvider.GetRequiredService<IExchangeRatesService>();

                await exchangeRatesService.AddIfNoExistsAsync();
            }
        }
    }

    public class JobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public JobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return new MyJob(_serviceProvider);

        }

        public void ReturnJob(IJob job)
        {
            var disposable = job as IDisposable;
            disposable?.Dispose();
        }
    }

    public class NpbBackgroundService : IHostedService
    {
        public IScheduler Scheduler { get; set; }
        private readonly IJobFactory _jobFactory;

        public NpbBackgroundService(IJobFactory jobFactory)
        {
            _jobFactory = jobFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await new StdSchedulerFactory().GetScheduler();
            Scheduler.JobFactory = _jobFactory;
            await Scheduler.Start();

            IJobDetail job = JobBuilder.Create<MyJob>()
                .WithIdentity("MyJob")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("JobTrigger")
                .StartNow()
                .WithCronSchedule("0 16 12 ? * 1-5")
                .Build();

            await Scheduler.ScheduleJob(job, trigger);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler?.Shutdown(cancellationToken);
        }
    }
}
