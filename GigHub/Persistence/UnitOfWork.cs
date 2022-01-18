using GigHub.Core;
using GigHub.Core.Repositories;
using GigHub.Persistence.Repositories;

namespace GigHub.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigRepository(_context);
            Genres = new GenreRepository(_context);
            Followings = new FollowingRepository(_context);
            Attendances = new AttendanceRepository(_context);
            UserNotifications = new UserNotificationRepository(_context);
            Notifications = new NotificationRepository(_context);
        }

        public IGigRepository Gigs { get; }
        public IGenreRepository Genres { get; }
        public IFollowingRepository Followings { get; }
        public IAttendanceRepository Attendances { get; }
        public IUserNotificationRepository UserNotifications { get; }
        public INotificationRepository Notifications { get; }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}