using System;
using System.Runtime.Serialization;

namespace ProductsService
{
    /// <summary>
    /// Data contract exchanged over the wire (SOAP/XML by default with
    /// basicHttpBinding). Deliberately separate from the EF6 entity, which
    /// is classic WCF practice: the entity shape and the wire contract are
    /// allowed to diverge, and EF6 change-tracking proxies should never be
    /// serialized directly.
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
}
