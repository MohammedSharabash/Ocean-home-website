using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ocean_Home.Models.ViewModel
{
    public class ChangePasswordVm
    {
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100,MinimumLength =6)]
        public string OldPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("OldPassword")]
        public string ConfirmPassword { get; set; }
        public string UserId { get; set; }
    }
}
