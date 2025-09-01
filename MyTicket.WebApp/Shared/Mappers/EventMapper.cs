using MyTicket.WebApp.Data.Entities;
using MyTicket.WebApp.Shared.ViewModels;

namespace MyTicket.WebApp.Shared.Mappers;

public static class EventMapper
{
    public static EventViewModel? MapToViewModel(Event? myEvent)
    {
        if (myEvent == null) return null;

        return new EventViewModel
        {
            Title = myEvent.Title,
            Description = myEvent.Description,
            BeginDate = myEvent.BeginDate,
            BeginTime = myEvent.BeginTime,
            EndDate = myEvent.EndDate,
            EndTime = myEvent.EndTime,
            Location = myEvent.Location,
            EventLink = myEvent.EventLink,
            Category = myEvent.Category,
            Capacity = myEvent.Capacity,
            OrganizerId = myEvent.OrganizerId
        };
    }

    public static Event? MapToEntity(EventViewModel? myEventViewModel)
    {
        if (myEventViewModel == null) return null;

        return new Event
        {
            Title = myEventViewModel.Title,
            Description = myEventViewModel.Description,
            BeginDate = myEventViewModel.BeginDate,
            BeginTime = myEventViewModel.BeginTime,
            EndDate = myEventViewModel.EndDate,
            EndTime = myEventViewModel.EndTime,
            Location = myEventViewModel.Location,
            EventLink = myEventViewModel.EventLink,
            Category = myEventViewModel.Category,
            Capacity = myEventViewModel.Capacity,
            OrganizerId = myEventViewModel.OrganizerId
        };
    }
}
