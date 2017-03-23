using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BIDVSmartContent.Models.Image
{
    public class ImageModel
    {
        public int ID { get; set; }
        public string URL { get; set; }
        public string TITLE { get; set; }
        public string TYPE { get; set; }
        public bool STATUS { get; set; }
        public string SECTION { get; set; }
        public string FILE_TYPE { get; set; }
        public HttpPostedFileBase File { get; set; } 
    }
}