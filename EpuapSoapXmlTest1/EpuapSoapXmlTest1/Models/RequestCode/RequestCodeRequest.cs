using PZePUAP.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLSerializable.Models.RequestCode
{
    /// <summary>
    /// RequestCode Request
    /// </summary>
    [XmlRoot("requestCode", Namespace = "http://signing2.zp.epuap.gov.pl")]
    public class RequestCodeRequest : IServiceRequest
    {
        public RequestCodeRequest()
        {

        }

        [XmlElement(ElementName = "userTgsid", Namespace = "")]
        public string UserTgsid { get; set; }

        public string SOAPAction
        {
            get
            {
                return "requestCode";
            }
        }

        public HeaderAttribute[] HeaderAttributes
        {
            get
            {
                return null;
            }
        }
    }
}
