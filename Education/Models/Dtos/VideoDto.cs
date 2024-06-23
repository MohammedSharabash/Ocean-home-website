using Ocean_Home.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ocean_Home.Models.Dtos
{
    public class VideoDto
    {
        public long Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long CourseId { get; set; }
        public string CourseName { get; set; }
        public bool Free { get; set; }
        public string URL { get; set; }
        //public string URL2 { get; set; }
        public VideoDto(ProjectImage model,string lang)
        {
            this.Id = model.Id;
            this.CreatedOn = model.CreatedOn;
         
        }
    }
}
