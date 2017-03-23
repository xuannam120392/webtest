using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BIDVSmartContent.Models.Block
{
    public class BlockModel
    {
        public int ID { get; set; }
        public string TITLE { get; set; }
        public string CONTENT { get; set; }
        public string TAB { get; set; }
        public string POSITION { get; set; }
        public bool STATUS { get; set; }
        public string SECTION { get; set; }
        public string IMAGE { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}