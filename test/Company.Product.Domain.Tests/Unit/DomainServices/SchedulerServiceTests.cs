using Company.Product.Domain.DomainServices;

namespace Company.Product.Domain.Tests.Unit.DomainServices;

public class SchedulerServiceTests
{

    private const int MinInMSec = 60000;
    
    [Theory]
    [InlineData(0,30, 13)]
    [InlineData(0,30, 10)]
    [InlineData(0,30, 11)]
    [InlineData(0,30, 16)]
    [InlineData(0,30, 1)]
    [InlineData(0,30, 31)]
    public void ShouldStart_ShouldNotStart_ShouldPass( int startMinute, int intervalMinute, int currentMinute)
    {
        // Arrange
        SchedulerService schedulerService = new SchedulerService();
        
        // Act
        var result = schedulerService.ShouldStart(startMinute, intervalMinute, currentMinute);
        
        // Assert
        Assert.False(result);
    }
    
    [Theory]
    [InlineData(0,30, 0)]
    [InlineData(0,30, 30)]
    [InlineData(1,31, 1)]
    [InlineData(1,31, 32)]
    public void ShouldStart_ShouldStartTheApplication_ShouldPass( int startMinute, int intervalMinute, int currentMinute)
    {
        // Arrange
        SchedulerService schedulerService = new SchedulerService();
        
        // Act
        var result = schedulerService.ShouldStart(startMinute, intervalMinute, currentMinute);
        
        // Assert
        Assert.True(result);
    }
    
    [Theory]
    [InlineData(0,30, 15,15)]
    [InlineData(0,30, 3,27)]
    [InlineData(0,30, 45,15)]
    [InlineData(0,30, 58,2)]
    [InlineData(0,30, 5,25)]
    public void GetSleepTime_ShouldReturnCorrectSleepTime_ShouldPass(int startMinute, int intervalMinute, int currentMinute, int sleepTime)
    {
        
        // Arrange
        SchedulerService schedulerService = new SchedulerService();
        
        // Act
        var result = schedulerService.GetSleepTime(startMinute, intervalMinute, currentMinute);
        
        // Assert
        Assert.Equal(result, (sleepTime * MinInMSec));
    }
   
    
}