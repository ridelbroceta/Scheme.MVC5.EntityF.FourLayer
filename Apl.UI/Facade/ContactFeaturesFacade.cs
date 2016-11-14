using System.Collections.Generic;
using System.Xml;

namespace Apl.UI.Facade
{

    public class SupportFacade
    {
        public SupportFacade()
        {
            Email = string.Empty;
            Phone = string.Empty;
        }

        public string Email { get; set; }
        public string Phone { get; set; }

    }

    public class SupportWithAddressFacade : SupportFacade
    {
        public SupportWithAddressFacade()
        {
            Address = string.Empty;
        }

        public string Address { get; set; }

    }

    public class ContactFacade
    {
        public ContactFacade()
        {
            Description = string.Empty;
            Emails = new List<string>();
        }

        public string Description { get; set; }
        public IList<string> Emails { get; set; }

    }

    public class ContactInfoFacade
    {

        public ContactInfoFacade()
        {
            Phones = new List<string>();
            AddressLarge = string.Empty;
            AddressShort = string.Empty;
            X = string.Empty;
            Y = string.Empty;
            Contactos = new List<ContactFacade>();
        }

        public IList<string> Phones { get; set; }
        public string AddressLarge { get; set; }
        public string AddressShort { get; set; }
        public string X { get; set; }
        public string Y { get; set; }
        public IList<ContactFacade> Contactos { get; set; }

    }


    public static class ContactFeaturesFacade
    {

        public static SupportFacade GetSupport(string fileName)
        {
            var tmp = new SupportFacade();
            if (System.IO.File.Exists(fileName))
            {
                //Creamos un documento y lo cargamos con los datos del XML.
                var documento = new XmlDocument();
                documento.Load(fileName);

                //Obtenemos una colección con todos los empleados.
                var soporte = documento.SelectNodes("empresa/support");

                //Creamos un único empleado.

                if (soporte != null)
                {
                    var unSoporte = soporte.Item(0);
                    if (unSoporte != null)
                    {
                        var selectSingleNode = unSoporte.SelectSingleNode("email");
                        if (selectSingleNode != null)
                            tmp.Email = selectSingleNode.InnerText.Trim();

                        var singleNode = unSoporte.SelectSingleNode("phone");
                        if (singleNode != null)
                            tmp.Phone = singleNode.InnerText.Trim();
                    }

                }
            }
            return tmp;
        }

        public static string GetRSocial(string fileName)
        {
            if (System.IO.File.Exists(fileName))
            {
                //Creamos un documento y lo cargamos con los datos del XML.
                var documento = new XmlDocument();
                documento.Load(fileName);

                //Obtenemos una colección con todos los empleados.
                var empresa = documento.SelectNodes("empresa");

                //Creamos un único empleado.

                if (empresa != null)
                {
                    var item = empresa.Item(0);
                    if (item != null)
                    {
                        var selectSingleNode = item.SelectSingleNode("rsocial");
                        if (selectSingleNode != null)
                            return selectSingleNode.InnerText.Trim();
                    }
                }
            }
            return null;
        }

        public static string GetSystem(string fileName)
        {
            if (System.IO.File.Exists(fileName))
            {
                //Creamos un documento y lo cargamos con los datos del XML.
                var documento = new XmlDocument();
                documento.Load(fileName);

                //Obtenemos una colección con todos los empleados.
                var empresa = documento.SelectNodes("empresa");

                //Creamos un único empleado.

                if (empresa != null)
                {
                    var item = empresa.Item(0);
                    if (item != null)
                    {
                        var selectSingleNode = item.SelectSingleNode("system");
                        if (selectSingleNode != null)
                            return selectSingleNode.InnerText.Trim();
                    }
                }
            }
            return null;
        }

        public static string GetSite(string fileName)
        {
            if (System.IO.File.Exists(fileName))
            {
                //Creamos un documento y lo cargamos con los datos del XML.
                var documento = new XmlDocument();
                documento.Load(fileName);

                //Obtenemos una colección con todos los empleados.
                var empresa = documento.SelectNodes("empresa");

                //Creamos un único empleado.

                if (empresa != null)
                {
                    var item = empresa.Item(0);
                    if (item != null)
                    {
                        var selectSingleNode = item.SelectSingleNode("site");
                        if (selectSingleNode != null)
                            return selectSingleNode.InnerText.Trim();
                    }
                }
            }
            return null;
        }


        public static SupportWithAddressFacade GetSupportWithAddress(string fileName)
        {
            var tmp = new SupportWithAddressFacade();
            if (System.IO.File.Exists(fileName))
            {
                //Creamos un documento y lo cargamos con los datos del XML.
                var documento = new XmlDocument();
                documento.Load(fileName);

                //Obtenemos una colección con todos los empleados.
                var soporte = documento.SelectNodes("empresa/support");
                var soporte2 = documento.SelectNodes("empresa/direccion");

                //Creamos un único empleado.

                if (soporte != null)
                {
                    var unSoporte = soporte.Item(0);
                    if (unSoporte != null)
                    {
                        var selectSingleNode = unSoporte.SelectSingleNode("email");
                        if (selectSingleNode != null)
                            tmp.Email = selectSingleNode.InnerText.Trim();

                        var singleNode = unSoporte.SelectSingleNode("phone");
                        if (singleNode != null)
                            tmp.Phone = singleNode.InnerText.Trim();
                    }
                }
                if (soporte2 != null)
                {
                    var unSoporte = soporte2.Item(0);
                    if (unSoporte != null)
                    {
                        var selectSingleNode = unSoporte.SelectSingleNode("large");
                        if (selectSingleNode != null)
                            tmp.Address = selectSingleNode.InnerText.Trim();

                    }
                }
            }
            return tmp;
        }

        public static ContactInfoFacade GetContactInfo(string fileName)
        {
            var result = new ContactInfoFacade();
            if (System.IO.File.Exists(fileName))
            {
                //Creamos un documento y lo cargamos con los datos del XML.
                var documento = new XmlDocument();
                documento.Load(fileName);

                var xPhones = documento.SelectNodes("empresa/phones");
                if (xPhones != null)
                {
                    var xLista = ((XmlElement)xPhones[0]).GetElementsByTagName("phone");

                    foreach (XmlElement nodo in xLista)
                    {
                        var tmp = nodo.InnerText.Trim();
                        result.Phones.Add(tmp);
                    }
                }

                var soporte2 = documento.SelectNodes("empresa/direccion");
                if (soporte2 != null)
                {
                    var unSoporte = soporte2.Item(0);
                    if (unSoporte != null)
                    {
                        var selectSingleNode = unSoporte.SelectSingleNode("large");
                        if (selectSingleNode != null)
                            result.AddressLarge = selectSingleNode.InnerText.Trim();

                        selectSingleNode = unSoporte.SelectSingleNode("short");
                        if (selectSingleNode != null)
                            result.AddressShort = selectSingleNode.InnerText.Trim();

                        selectSingleNode = unSoporte.SelectSingleNode("x");
                        if (selectSingleNode != null)
                            result.X = selectSingleNode.InnerText.Trim();

                        selectSingleNode = unSoporte.SelectSingleNode("y");
                        if (selectSingleNode != null)
                            result.Y = selectSingleNode.InnerText.Trim();
                    }
                }

                var xContacts = documento.SelectNodes("empresa/contactos");
                if (xContacts != null)
                {
                    var xLista = ((XmlElement)xContacts[0]).GetElementsByTagName("registro");

                    foreach (XmlElement nodo in xLista)
                    {
                        var tmp = new ContactFacade();
                        var selectSingleNode = nodo.SelectSingleNode("desc");
                        if (selectSingleNode != null)
                            tmp.Description = selectSingleNode.InnerText.Trim();

                        var emails = nodo.GetElementsByTagName("emails");
                        var xEmails = ((XmlElement)emails[0]).GetElementsByTagName("email");
                        foreach (XmlElement item in xEmails)
                        {
                            tmp.Emails.Add(item.InnerText.Trim());
                        }
                        result.Contactos.Add(tmp);
                    }
                }
            }
            return result;
        }


    }
}