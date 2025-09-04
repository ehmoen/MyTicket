using Microsoft.EntityFrameworkCore;
using MyTicket.WebApp.Data;
using MyTicket.WebApp.Data.Entities;
using MyTicket.WebApp.Shared.Mappers;
using MyTicket.WebApp.Shared.ViewModels;

namespace MyTicket.WebApp.Features.EditEvent;

public class EditEventService(IDbContextFactory<ApplicationDbContext> contextFactory)
{
    public async Task EditEventAsync(int eventId)
    {
        await using var context = await contextFactory.CreateDbContextAsync();

        if (context.Events != null)
        {
            var existingEvent = await context.Events.FirstOrDefaultAsync(x => x.EventId == eventId);
            if (existingEvent == null)
            {
                throw new InvalidOperationException($"Event with ID {eventId} not found.");
            }
            // Here you would typically update the properties of existingEvent
            // with new values from a provided EventViewModel or similar.
            // For example:
            // existingEvent.Title = updatedEventViewModel.Title;
            // existingEvent.Description = updatedEventViewModel.Description;
            // ... update other properties as needed ...
            EventViewModel? updatedEventViewModel = new();
            existingEvent = EventMapper.MapToEntity(updatedEventViewModel);
              
            context.Events.Update(existingEvent);
            await context.SaveChangesAsync();
        }

        
    }
    
    public async Task<EventViewModel?> GetEventByIdAsync(int eventId)
    {
        await using var context = await contextFactory.CreateDbContextAsync();

        if (context.Events == null) return null;
        var existingEvent = await context.Events.FirstOrDefaultAsync(x => x.EventId == eventId);
        
        if (existingEvent == null)
        {
            throw new InvalidOperationException($"Event with ID {eventId} not found.");
        }
         
        return EventMapper.MapToViewModel(existingEvent);
    }
    
    public async Task UpdateEventAsync(EventViewModel updatedEvent)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        if (context.Events == null)
        {
            throw new InvalidOperationException("Events DbSet is not available.");
        }

        var eventEntity = await context.Events.FirstOrDefaultAsync(e => e.EventId == updatedEvent.EventId);
        if (eventEntity == null)
        {
            throw new InvalidOperationException($"Event with ID {updatedEvent.EventId} not found.");
        }

        // Update properties
        eventEntity.Title = updatedEvent.Title;
        eventEntity.Description = updatedEvent.Description;
        eventEntity.BeginDate = updatedEvent.BeginDate;
        eventEntity.BeginTime = updatedEvent.BeginTime;
        eventEntity.EndDate = updatedEvent.EndDate;
        eventEntity.EndTime = updatedEvent.EndTime;
        eventEntity.Location = updatedEvent.Location;
        eventEntity.EventLink = updatedEvent.EventLink;
        eventEntity.Category = updatedEvent.Category;
        eventEntity.Capacity = updatedEvent.Capacity;
        eventEntity.ImageUrl = updatedEvent.ImageUrl;
        eventEntity.OrganizerId = updatedEvent.OrganizerId;

        //eventEntity = EventMapper.MapToEntity(updatedEvent);
        
        context.Events.Update(eventEntity);
        await context.SaveChangesAsync();
    }
}