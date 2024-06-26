using Ocean_Home.Models.Domain;
using System.Collections.Generic;

namespace Ocean_Home.Models.ViewModel
{
    public class IndexVm
    {
        public List<Specialty> Specialties { get; set; } = new List<Specialty>();
        public List<Project> Projects { get; set; } = new List<Project>();
        public List<JobsDepartment> JobsDepartments { get; set; } = new List<JobsDepartment>();
        public List<ProjectsDepartment> ProjectsDepartments { get; set; } = new List<ProjectsDepartment>();
        public List<ProjectImage> ProjectImages { get; set; } = new List<ProjectImage>();
        public List<Slider> Sliders { get; set; } = new List<Slider>();
        public List<Manager> Managers { get; set; } = new List<Manager>();
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public List<OurAffiliate> OurAffiliates { get; set; } = new List<OurAffiliate>();
        public AboutUS aboutUS { get; set; } = new AboutUS();
        public ContactUS contact { get; set; } = new ContactUS();
    }
}
