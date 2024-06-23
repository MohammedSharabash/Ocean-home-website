using Ocean_Home.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Ocean_Home.Models.Dtos
{
    public class LoginDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Token { get; set; }

        public LoginDto(AppUser model)
        {
            this.Id = model.Id;
            this.UserName = model.Name;
            this.Email = model.Email;
            this.Phone = model.PhoneNumber;
           
        }
    }
   
}
