using PZePUAP.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLSerializable.Models.Faults
{
    [XmlRoot("Fault", Namespace = "")]
    public class Fault : IServiceResponse
    {
        [XmlElement("faultcode", Namespace = "")]
        public string Faultcode { get; set; }

        [XmlElement("faultstring", Namespace = "")]
        public string Faultstring { get; set; }

        [XmlElement("detail", Namespace = "")]
        public FaultDetail Detail { get; set; }

    }

    public class FaultDetail
    {
        [XmlElement("TpSigning2Exception", Namespace = "http://signing2.zp.epuap.gov.pl")]
        public FaultException Message { get; set; }
    }

    public class FaultException
    {
        [XmlElement("message", Namespace = "")]
        public string Message { get; set; }
    }
}
