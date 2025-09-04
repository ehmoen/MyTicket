using Microsoft.EntityFrameworkCore;
using MyTicket.WebApp.Data;
using MyTicket.WebApp.Shared.Mappers;
using MyTicket.WebApp.Shared.ViewModels;

namespace MyTicket.WebApp.Features.CreateEvent;

public class CreateEventService(IDbContextFactory<ApplicationDbContext> contextFactory)
{
    public async Task CreateEventAsync(EventViewModel eventViewModel)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
       
        var newEvent = EventMapper.MapToEntity(eventViewModel);

        if (newEvent != null)
        {
            context.Events?.Add(newEvent);
            await context.SaveChangesAsync();
        }
        else
        {
            // TODO: Handle the case where mapping fails (optional)
            throw new InvalidOperationException("Failed to map EventViewModel to Event entity.");
        }
        
    }
}