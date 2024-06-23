using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ocean_Home.Models.Domain
{
    public class Project : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual ProjectsDepartment Department { get; set; }
        public int Sort { get; set; }
        public string ScopeOfWork { get; set; }
        public string ConsultantAndDesigner { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public virtual ICollection<ProjectImage> Images { get; set; }
    }
}
