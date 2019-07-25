using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LGCoreApp.Application.ViewModels.Common;

namespace LGCoreApp.Models
{
    public class ContactPageViewModel
    {
        public ContactViewModel Contact { set; get; }

        public FeedbackViewModel Feedback { set; get; }
    }
}
