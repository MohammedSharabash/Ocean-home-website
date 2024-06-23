using Ocean_Home.Helper;
using Ocean_Home.Models.Domain;
using Ocean_Home.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ocean_Home.Models.Dtos
{
    public class CourseDto
    {
        public long Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string InstructorName { get; set; }
        public double Price { get; set; }
        public double? OfferPrice { get; set; }
        public string ImageUrl { get; set; }
        public bool IsSubscribe { get; set; }
        public CourseDto(Project model, string lang)
        {
            this.Id = model.Id;
            this.CreatedOn = model.CreatedOn;
            this.Name =model.Name;
            this.Description =  model.Description ;
        }
    }
}
