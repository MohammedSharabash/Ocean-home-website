using Ocean_Home.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ocean_Home.Models.Domain
{
    public class AppUser: IdentityUser
    {
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
