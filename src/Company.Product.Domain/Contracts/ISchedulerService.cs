namespace Company.Product.Domain.Contracts;

public interface ISchedulerService
{
    public bool ShouldStart(int startMinute, int intervalMinute, int currentMinute);
    public int GetSleepTime(int startMinute, int intervalMinute, int currentMinute);
}