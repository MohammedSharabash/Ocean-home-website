using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ocean_Home.Models.Dtos
{
    public class VerifyUserDto
    {
        [Required]
        public string Phone { get; set; }

        [Required]
        public int VerificationCode { get; set; }
    }
}
