using Company.Product.Application.UseCases.Models;

namespace Company.Product.Application.Pipeline.Models;

public class KontextData
{
    public InspectionResult? InspectionResult { get; set; } 
    public CancellationToken CancellationToken { get; set; }
    public bool ShouldContinue { get; set; } = true;
    public Exception Exception { get; set; } =new ();
    
    public bool ExceptionOccurred { get; set; } =false;

    public Dictionary<string, object> Properties { get; set; } = new();
    public List<string> ExecutedOperations { get; set; } = new();
    
    public int SleepTimeInMSec { get; set; } = 0;

}