using Microsoft.EntityFrameworkCore;
using MyTicket.WebApp.Data;
using MyTicket.WebApp.Data.Entities;
using MyTicket.WebApp.Shared.Mappers;
using MyTicket.WebApp.Shared.ViewModels;

namespace MyTicket.WebApp.Features.ViewCreatedEvents;

public class ViewCreatedEventsService(IDbContextFactory<ApplicationDbContext> contextFactory)
{
    public async Task<List<EventViewModel>> GetEventsAsync()
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        List<Event> events = await context.Events!.ToListAsync();
        return events.Select(EventMapper.MapToViewModel).Where(e => e != null).ToList()!;
    }
}