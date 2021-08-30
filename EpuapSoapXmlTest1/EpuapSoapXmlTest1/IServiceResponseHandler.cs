using XMLSerializable.Models.Faults;

namespace PZePUAP.ServiceInterface
{
    /// <summary>
    /// Response handler, creates the response out of the SOAP string
    /// </summary>
    public interface IServiceResponseHandler<TResult>
        where TResult : class, IServiceResponse
    {
        TResult FromSOAP(string soapResponse, out Fault fault);
    }
}
