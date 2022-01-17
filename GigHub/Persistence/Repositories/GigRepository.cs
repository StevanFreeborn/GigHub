using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories
{
    public class GigRepository : IGigRepository
    {
        private readonly IApplicationDbContext _context;

        public GigRepository(IApplicationDbContext context)
        {
            _context = context;
        }


        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);

            var artistId = gig.ArtistId;

            var followers = _context.Followings
                .Where(f => f.FolloweeId == artistId)
                .Select(f => f.Follower)
                .ToList();

            var notification = Notification.GigCreated(gig);

            foreach(var follower in followers)
            {
                follower.Notify(notification);
            }
        }

        public Gig GetGigById(int gigId)
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .SingleOrDefault(g => g.Id == gigId);
        }

        public Gig GetGigWithAttendees(int gigId)
        {
            return _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .SingleOrDefault(g => g.Id == gigId);
        }

        public IEnumerable<Gig> QueryUpcomingGigs(string userId, string query)
        {
            var upcomingGigs = GetUsersUpcomingGigs(userId);

            return upcomingGigs
                .Where(g =>
                    g.Artist.Name.Contains(query) ||
                    g.Genre.Name.Contains(query) ||
                    g.Venue.Contains(query))
                .OrderBy(g => g.DateTime)
                .ToList();
        }

        public IEnumerable<Gig> GetUpcomingGigs(string userId)
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g =>
                    g.DateTime > DateTime.Now &&
                    g.IsCanceled == false &&
                    g.ArtistId != userId)
                .OrderBy(g => g.DateTime)
                .ToList();
        }

        public IEnumerable<Gig> GetUsersUpcomingGigs(string userId)
        {
            return _context.Gigs
                .Where(g =>
                    g.ArtistId == userId &&
                    g.DateTime > DateTime.Now &&
                    g.IsCanceled == false)
                .Include(g => g.Genre)
                .OrderBy(g => g.DateTime)
                .ToList();
        }

        public IEnumerable<Gig> GetGigsUserAttending(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .OrderBy(g => g.DateTime)
                .ToList();
        }

    }
}