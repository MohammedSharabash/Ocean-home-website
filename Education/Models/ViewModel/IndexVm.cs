using Ocean_Home.Models.Domain;
using System.Collections.Generic;

namespace Ocean_Home.Models.ViewModel
{
    public class IndexVm
    {
        public List<Project> Courses { get; set; } = new List<Project>();
        public ProjectImage ProjectImages { get; set; } = new ProjectImage();
        public List<Slider> Sliders { get; set; } = new List<Slider>();
        public AboutUS aboutUS { get; set; } = new AboutUS();
    }
}
