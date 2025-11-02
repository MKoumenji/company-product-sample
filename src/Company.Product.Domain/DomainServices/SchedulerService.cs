using Company.Product.Domain.Contracts;

namespace Company.Product.Domain.DomainServices;

public class SchedulerService:ISchedulerService
{
    public bool ShouldStart(int startMinute, int intervalMinute, int currentMinute)
    {
        var nextStart = (startMinute + intervalMinute) % 60;
        return currentMinute == startMinute || currentMinute == nextStart;
    }
    
    public int GetSleepTime(int startMinute, int intervalMinute, int currentMinute)
    {
        const int  minInMSec = 60000; 
        const int  minuteInSec = 60; 
        var nextStart = (startMinute + intervalMinute) % 60;
        var executionTimes = new[] { startMinute, nextStart }.OrderBy(x => x).ToArray();
       
        // next start?
        foreach (var executionTime in executionTimes)
            if (executionTime > currentMinute)
                return (executionTime - currentMinute) * minInMSec;
       
        // sonst berechne bis zur ersten Ausführungszeit der nächsten Stunde
        return ((minuteInSec - currentMinute) + executionTimes[0]) * minInMSec;

    }
}