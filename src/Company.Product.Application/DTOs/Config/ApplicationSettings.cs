namespace Company.Product.Application.DTOs.Config;

public class ApplicationSettings
{
    public required int TimeSpanInMinutes { get; init; }
    public required int OrderProcessingTimeInSec { get; init; }
    public required int MarginInMinutes { get; init; }
    public required string OrderHandlerUrl { get; init; }
    public int ExecutionDelayInMinutes { get; init; }
    public int StartMinute { get; init; }
    
    
}