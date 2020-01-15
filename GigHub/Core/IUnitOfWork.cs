using GigHub.Core.Repositories;

namespace GigHub.Core
{
    public interface IUnitOfWork
    {
        IGigRepository Gigs { get; }
        IFollowingRepository Followings { get; }
        IAttendanceRepository Attendances { get; }
        IGenreRepository Genres { get; }
        INotificationRepository Notifications { get; }
        IUserNotificationRepository UserNotifications { get; }
        void Complete();
    }
}