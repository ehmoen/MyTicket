using Microsoft.EntityFrameworkCore;
using MyTicket.WebApp.Data;
using MyTicket.WebApp.Data.Entities;
using MyTicket.WebApp.Shared.Mappers;
using MyTicket.WebApp.Shared.ViewModels;

namespace MyTicket.WebApp.Features.DiscoverEvents;

public class DiscoverEventService(IDbContextFactory<ApplicationDbContext> contextFactory)
{
    public async Task<List<EventViewModel?>> GetEventsAsync(string? filter = "")
    {
        await using var context = await contextFactory.CreateDbContextAsync();

        if (context.Events == null)
        {
            throw new InvalidOperationException("Events DbSet is not available.");
        }

        List<Event> events = await SearchEvents(filter, context);

        if (string.IsNullOrWhiteSpace(filter) || events.Count > 0)
        {
            return events.Select(EventMapper.MapToViewModel).ToList();
        }
        
        // If no events found with the filter, return all upcoming events
        filter = null;
        events = await SearchEvents(filter, context);

        return events.Select(EventMapper.MapToViewModel).ToList();
    }

    private async Task<List<Event>> SearchEvents(string filter, ApplicationDbContext context)
    {
        return await (context.Events?.Where(e => e.Title.Contains(filter) ||
                                                 e.Description.Contains(filter) ||
                                                 e.Location.Contains(filter) &&
                                                 e.BeginDate >= DateOnly.FromDateTime(DateTime.Now) ||
                                                 (e.BeginDate == DateOnly.FromDateTime(DateTime.Now) &&
                                                  e.BeginTime > TimeOnly.FromDateTime(DateTime.Now))
        ).OrderBy(e => e.BeginDate).ThenBy(e => e.BeginTime)).ToListAsync();
    }
}