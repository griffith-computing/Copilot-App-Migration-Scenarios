using System.Collections.Generic;
using System.ServiceModel;

namespace ProductsService.Client.ServiceReference
{
    /// <summary>
    /// Duplicated (not shared/project-referenced) copy of the service's
    /// contract interface and DTO, exactly as "Add Service Reference" /
    /// svcutil.exe would generate it from the service's published WSDL.
    /// Real WCF clients never reference the service's assembly directly --
    /// they regenerate this from metadata, which is why the contract is
    /// duplicated here rather than shared.
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
