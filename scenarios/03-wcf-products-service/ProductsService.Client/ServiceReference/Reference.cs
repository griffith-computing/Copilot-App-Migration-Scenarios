using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace ProductsService.Client.ServiceReference
{
    /// <summary>
    /// svcutil-style generated data contract (mirrors the service's
    /// ProductDto, duplicated here since the client only knows the wire
    /// shape via WSDL/XSD metadata, not the service's actual type).
    /// </summary>
    [DataContract]
    public class ProductDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public bool InStock { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }
    }

    /// <summary>
    /// svcutil-style generated client proxy. Real tooling emits a class
    /// like this deriving from <see cref="ClientBase{TChannel}"/> and
    /// implementing the contract by forwarding each call through
    /// <c>Channel</c>. The matching client endpoint/binding configuration
    /// lives in App.config under &lt;system.serviceModel&gt;&lt;client&gt;,
    /// keyed by the endpoint name passed (or matched by contract) here.
    /// </summary>
    public class ProductServiceClient : ClientBase<IProductService>, IProductService
    {
        public ProductServiceClient() : base("ProductServiceEndpoint")
        {
        }

        public System.Collections.Generic.List<ProductDto> GetProducts()
        {
            return Channel.GetProducts();
        }

        public ProductDto GetProduct(int id)
        {
            return Channel.GetProduct(id);
        }

        public ProductDto CreateProduct(ProductDto product)
        {
            return Channel.CreateProduct(product);
        }

        public ProductDto UpdateProduct(ProductDto product)
        {
            return Channel.UpdateProduct(product);
        }

        public bool DeleteProduct(int id)
        {
            return Channel.DeleteProduct(id);
        }
    }
}
