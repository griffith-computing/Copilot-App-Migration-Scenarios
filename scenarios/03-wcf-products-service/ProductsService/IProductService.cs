using System.Collections.Generic;
using System.ServiceModel;

namespace ProductsService
{
    /// <summary>
    /// WCF service contract. [ServiceContract]/[OperationContract] define
    /// the SOAP operations and are compiled into the WSDL exposed at
    /// ProductService.svc?wsdl. This attribute-driven, contract-first
    /// style has no equivalent in a REST/minimal API or gRPC world --
    /// migrating away from WCF means redesigning the contract, not just
    /// moving the code.
    /// </summary>
    [ServiceContract]
    public interface IProductService
    {
        [OperationContract]
        List<ProductDto> GetProducts();

        [OperationContract]
        ProductDto GetProduct(int id);

        [OperationContract]
        ProductDto CreateProduct(ProductDto product);

        [OperationContract]
        ProductDto UpdateProduct(ProductDto product);

        [OperationContract]
        bool DeleteProduct(int id);
    }
}
