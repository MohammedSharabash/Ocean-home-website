using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ocean_Home.Models.Domain
{
    public class AboutUS : BaseModel
    {
        public string OurVision { get; set; }
        public string OurMessage { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string CoverImageUrl { get; set; }

    }
}
