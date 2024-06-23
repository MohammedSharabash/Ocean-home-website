using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ocean_Home.Models.Domain
{
    public class OurAffiliate : BaseModel
    {
        public string ImageUrl_150x150 { get; set; }
        public string ImageUrl_300x300 { get; set; }
        public string ImageUrl_370x370 { get; set; }
        public string ImageUrl_685x685 { get; set; }
        public string ImageUrl_768x768 { get; set; }
        public string ImageUrl_1024x1024 { get; set; }
        public string ImageUrl_1080 { get; set; }
    }
}
