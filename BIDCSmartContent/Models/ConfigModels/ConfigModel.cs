using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BIDVSmartContent.Models.ConfigModels
{
    public class ConfigModel
    {
        public string Code { get; set; }
        public string Value { get; set; }
        public string Desc { get; set; }
        public bool Status { get; set; }
    }
}