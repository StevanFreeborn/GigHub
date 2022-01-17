using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IUserNotificationRepository
    {
        List<UserNotification> GetUnreadUserNotifications(string userId);
    }
}