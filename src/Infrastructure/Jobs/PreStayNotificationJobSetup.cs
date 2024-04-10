using Microsoft.Extensions.Options;
using Quartz;

namespace HotelManagement.Infrastructure.Jobs;
public class PreStayNotificationJobSetup : IConfigureOptions<QuartzOptions>
{
    public void Configure(QuartzOptions options)
    {
        var jobKey = JobKey.Create(nameof(PreStayNotificationJob));

        options.AddJob<PreStayNotificationJob>(job =>
        {
            job.WithIdentity(jobKey)
                .WithDescription("Sends an e-mail to the users to notify them about their booking");
        });

        // Run the job every day at 8:00 AM
        options.AddTrigger(trigger =>
        {
            trigger
                .WithCronSchedule("0 8 * * *")
                .ForJob(jobKey);
        });
    }
}
