using Ocean_Home.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ocean_Home.Models.Domain
{
    public class Manager : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public JobTitle jobTitle { get; set; }
    }
}
