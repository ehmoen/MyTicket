using Microsoft.EntityFrameworkCore;
using MyTicket.WebApp.Data;

namespace MyTicket.WebApp.Features.DeleteEvent;

public class DeleteEventService(IDbContextFactory<ApplicationDbContext> contextFactory)
{
    public async Task<bool> DeleteEventAsync(int? eventId)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
       
        if (context.Events == null)
        {
            throw new InvalidOperationException("Events DbSet is not available.");
        }
        
        if (eventId == null) return false;
        var existingEvent = await context.Events.FirstOrDefaultAsync(x => x.EventId == eventId);

        if (existingEvent == null) return false;
        context.Events.Remove(existingEvent);
        await context.SaveChangesAsync();

        return true;
    }
    
    public bool IsEventDeletable(int? eventId)
    {
        if (eventId == null) return false;
        
        using var context = contextFactory.CreateDbContext();
        var existingEvent = context.Events?.FirstOrDefault(x => x.EventId == eventId);
        
        if (existingEvent == null) return false;
        
        // check if the event is in the past
        return existingEvent.BeginDate >= DateOnly.FromDateTime(DateTime.Now) && 
               (existingEvent.BeginDate != DateOnly.FromDateTime(DateTime.Now) || 
                existingEvent.BeginTime >= TimeOnly.FromDateTime(DateTime.Now));
        
    }
}