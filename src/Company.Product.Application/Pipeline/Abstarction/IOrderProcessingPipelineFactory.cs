using Company.Product.Application.Pipeline.Models;

namespace Company.Product.Application.Pipeline.Abstarction;

public interface IOrderProcessingPipelineFactory
{
    Pipeline<KontextData> CreatePipeline();
}