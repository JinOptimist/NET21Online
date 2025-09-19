using WebPortal.Enum;

namespace WebPortal.DbStuff.Models.Notifications
{
    public class Notification : BaseModel
    {
        public string Message { get; set; }
        
        public DateTime CreateAt { get; set; }

        public virtual List<User> UserWhoViewedIt {  get; set; }

        public virtual User Author { get; set; }

        public Role? LevelNotification { get; set; } // Если RoleIntification = null, то сообщение отправляется всем пользователям 
    }
}
