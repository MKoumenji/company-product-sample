using System.Net.Http;
using System.Threading.Tasks;
using Company.Product.Domain.Entities;

namespace Company.Product.Application.Contracts.Infrastructure;

public interface IOrderHandler
{
    public bool Start();
    public bool Stop();
    public Task<HttpResponseMessage> Send(RequestOrder orders, string url);
}