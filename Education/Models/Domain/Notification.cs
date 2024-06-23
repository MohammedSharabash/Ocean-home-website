using Ocean_Home.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ocean_Home.Models.Domain
{
    public class Notification : BaseModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public bool IsSeen { get; set; }
        public NotifiactionType NotificationType { get; set; }
        public string NotificationLink { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }
    }
}
