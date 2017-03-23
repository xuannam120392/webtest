using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BIDVSmartContent.Models.AnswerModels
{
    public class AnswerModel
    {
           public int AS_ID { get; set; }
           public string AS_CONTENT {get; set;}
           public string QS_CONTENT {get; set;}
           public bool AS_STATUS { get; set; }
           public string CREATED_USER { get; set; }
           public DateTime CREATED_DATE { get; set; }
    }
}