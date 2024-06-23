using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ocean_Home.Models.Domain
{
    public class Employee : BaseModel
    {
        public string Name { get; set; }
        public string JobTitle { get; set; }
        public string ImageUrl { get; set; }
        public long DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual JobsDepartment Department { get; set; }

    }
}
