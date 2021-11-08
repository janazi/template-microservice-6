using Flunt.Notifications;

namespace MicroserviceBase.Application
{
    public class Result : Notifiable<Notification>
    {
        protected Result() { }

        protected Result(ICollection<Notification> notifications)
        {
            AddNotifications(notifications);
        }

        public ErrorCode? Error { get; set; }
    }
}
