using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BIDVSmartContent.Models.Category
{
    public class CategoryModel
    {
        public int ID { get; set; }
        public int PARENT_ID { get; set; }
        public string TITLE { get; set; }
        public string DESC { get; set; }
        public string TYPE { get; set; }
        public bool STATUS { get; set; }
        public string CREATED_USER { get; set; }
    }
}