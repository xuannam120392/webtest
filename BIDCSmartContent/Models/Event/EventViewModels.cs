using BIDVSmartContent.Models.Contact;
using BIDVSmartContent.Models.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BIDVSmartContent.Models.Event
{
    public class EventViewModels : ContactModel
    {
        public List<NewModel> ListNewEvent { get; set; }
        public NewModel ListNewDetail { get; set; }
    }
}