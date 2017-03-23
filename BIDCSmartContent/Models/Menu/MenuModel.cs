using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BIDVSmartContent.Models.Menu
{
    public class MenuModel
    {
          public int Id { get; set; }  
          public string Title {get; set;}
	      public string Link {get; set;}
	      public string Desc {get; set;}
	      public bool Status {get; set;}
	      public string Link_Home {get; set;}
	      public  int Order {get; set;}
    }
}