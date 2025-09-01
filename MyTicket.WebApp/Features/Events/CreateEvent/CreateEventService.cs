using Microsoft.EntityFrameworkCore;
using MyTicket.WebApp.Data;
using MyTicket.WebApp.Shared.Mappers;
using MyTicket.WebApp.Shared.ViewModels;

namespace MyTicket.WebApp.Features.Events.CreateEvent;

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
    
    
    
    
    public string? ValidateEvent(EventViewModel? eventViewModel)
    {
        if (eventViewModel == null)
        {
            return "Event data is required.";
        }

        string? errorMessage = string.Empty;


        errorMessage = eventViewModel.ValidateDates();
        if (!string.IsNullOrWhiteSpace(errorMessage))
        {
            return errorMessage;
        }

        errorMessage = eventViewModel.ValidateLocation();
        if (!string.IsNullOrWhiteSpace(errorMessage))
        {
            return errorMessage;
        }

        errorMessage = eventViewModel.ValidateEventLink();
        if (!string.IsNullOrWhiteSpace(errorMessage))
        {
            return errorMessage;
        }

        return string.Empty;
    }
}