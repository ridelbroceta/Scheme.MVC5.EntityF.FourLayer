using System.Collections.Generic;

namespace Apl.UI.Models
{



    public class InfoElement
    {
        public string Right { get; set; }
        public string Left { get; set; }

    }

    public class ListaInfoElement
    {
        private readonly List<InfoElement> _lista;

        public IList<InfoElement> List {
            get
            {
                return _lista;
            }
        }

        public ListaInfoElement()
        {
            _lista = new List<InfoElement>();
        }

        public void Add(string right, string left)
        {
            _lista.Add(new InfoElement{Right = right, Left = left});
        }



    }

    public class HeaderInfoViewPartial
    {
        public string Prefijo { get; set; }

        public string Header
        {
            get
            {
                return "";
            }
        }

    }

    public class InfoViewPartial : HeaderInfoViewPartial
    {

        public string UrlPartialView { get; set; }

        public ListaInfoElement Body { get; set; }

        public InfoViewPartial()
        {
            Body = new ListaInfoElement();
        }


    }


    public class ViewOptions
    {

        public ViewOptions()
        {
            IsPrincipal = true;
        }


        public string Legend { get; set; }

        public InfoViewPartial InfoViewPartial { get; set; }

        public string Message { get; set; }

        public bool IsPrincipal { get; set; }

        public bool IsFixed { get; set; }

        public bool IsSencondaryWhite { get; set; }

        public object RouteValues { get; set; }

        public string[] FileNamesToExport { get; set; }

    }

    public class QueryViewOptions : ViewOptions
    {

        public QueryViewOptions()
        {
            IsPrincipal = true;
            IsFixed = true;
            HaveSubmit = true;
            HaveReport = false;
        }

        public bool HaveReport { get; set; }

        public bool HaveSubmit { get; set; }

        public string AjaxAction { get; set; }
        
        public string Url { get; set; }

        public bool HavePartialView { get; set; }

        public string PartialView { get; set; }
    }


}