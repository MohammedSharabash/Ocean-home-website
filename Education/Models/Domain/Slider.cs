using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ocean_Home.Models.Domain
{
    public class Slider : BaseModel
    {
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public int Sort { get; set; }

    }
}
