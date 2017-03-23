using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BIDVSmartContent.Models.Support
{
    public class SupportModel
    {
        public int ID {get; set;}
        public string TITLE  {get; set;}
        public string CONTENT  {get; set;}
        public bool STATUS  {get; set;}
        public string FROM_EMAIL {get; set;}
        public string EMAIL { get; set; }
        public string SDT { get; set; }
    }
}