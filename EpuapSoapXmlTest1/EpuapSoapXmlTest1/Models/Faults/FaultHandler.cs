using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace XMLSerializable.Models.Faults
{
    class FaultHandler
    {
        public Fault FromSOAP(string soapEnvelope)
        {
            if (string.IsNullOrEmpty(soapEnvelope))
            {
                throw new ArgumentNullException();
            }

            try
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(soapEnvelope);

                XmlSerializer serializer = new XmlSerializer(typeof(Fault));
                XmlNamespaceManager nsManager = new XmlNamespaceManager(xml.NameTable);
                nsManager.AddNamespace("soap","http://schemas.xmlsoap.org/soap/envelope/");

                if (xml.SelectSingleNode("//soap:Fault", nsManager) is XmlElement response)
                {
                    using (StringReader reader = new StringReader(response.OuterXml))
                    {
                        return serializer.Deserialize(reader) as Fault;
                    }
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
