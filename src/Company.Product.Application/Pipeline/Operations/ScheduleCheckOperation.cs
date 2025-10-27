using Company.Product.Application.DTOs.Config;
using Company.Product.Application.Pipeline.Abstarction;
using Company.Product.Application.Pipeline.Models;
using Company.Product.Domain.Contracts;
using Microsoft.Extensions.Options;

namespace Company.Product.Application.Pipeline.Operations;

public class ScheduleCheckOperation(ISchedulerService schedulerService, IOptions<ApplicationSettings> options) 
    : IOperation<KontextData>

{
    public void Invoke(KontextData results)
    {
        results.ExecutedOperations.Add(nameof(ScheduleCheckOperation));
        
        if (results.CancellationToken.IsCancellationRequested)
        {
            results.ShouldContinue = false;
            return;
        }
 
        // Check if it's time to start the execution
        if (!schedulerService.ShouldStart(
                options.Value.StartMinute, 
                options.Value.ExecutionDelayInMinutes, 
                DateTime.Now.Minute))
        {
            var sleepTime = schedulerService.GetSleepTime(
                options.Value.StartMinute,
                options.Value.ExecutionDelayInMinutes,
                DateTime.Now.Minute);
            
            results.SleepTimeInMSec = sleepTime;
            results.ShouldContinue = false;
        }
    }
}