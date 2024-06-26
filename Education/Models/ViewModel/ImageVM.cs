using Ocean_Home.Models.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ocean_Home.Models.ViewModel
{
    public class ImageVM
    {

        public long Id { get; set; }
        public int Sort { get; set; }
        public string ImageUrl { get; set; }
        public long ProjectId { get; set; }
    }
}