using CoreWCF;

namespace HelloWorldWcf;

[ServiceContract]
public interface IHelloWorldService
{
    [OperationContract]
    string SayHello(string name);
}
