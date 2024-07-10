using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ocean_Home.Models.Domain
{
    public class ContactUS : BaseModel
    {
        public string Location { get; set; }
        public string MobilePhones { get; set; }
        public string CallDirectly_Phone { get; set; }
        public string WorkHours { get; set; }
        public string Logo { get; set; }
        public string Instagram { get; set; }
        public string Tiktok { get; set; }
        public string WhatsApp { get; set; }
        public string PDF { get; set; }

    }
}
