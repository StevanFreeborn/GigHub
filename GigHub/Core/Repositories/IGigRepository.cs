using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IGigRepository
    {
        void Add(Gig gig);
        Gig GetGigById(int gigId);
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        Gig GetGigWithAttendees(int gigId);
        IEnumerable<Gig> GetUsersUpcomingGigs(string userId);
        IEnumerable<Gig> GetUpcomingGigs(string userId);
        IEnumerable<Gig> QueryUpcomingGigs(string userId, string query);
    }
}