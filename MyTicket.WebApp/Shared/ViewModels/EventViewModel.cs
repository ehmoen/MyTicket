using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Forms;
using MyTicket.WebApp.Features.CreateEvent;

namespace MyTicket.WebApp.Shared.ViewModels;

public class EventViewModel
{
    public int EventId { get; set; }
    

    [Required]
    [StringLength(maximumLength: 100)]
    public string? Title { get; set; } 
    
    [StringLength(maximumLength: 500)]
    public string? Description { get; set; }
    
    [Required]
    public DateOnly BeginDate { get; set; } 

    [Required]
    public TimeOnly BeginTime { get; set; } 

    [Required]
    public DateOnly EndDate { get; set; } 

    [Required]
    public TimeOnly EndTime { get; set; } 

    public string? Location { get; set; }
    
    public string? EventLink { get; set; }
    
    [Required]
    public string? Category { get; set; } 
    
    [Range(0, int.MaxValue)]
    public int Capacity { get; set; }

    public IBrowserFile CoverImage { get; set; }
    
    public string? ImageUrl { get; set; } = $"/images/placeholder_300x169.png";
    
    public int OrganizerId { get; set; }


    public EventViewModel()
    {
        BeginDate = DateOnly.FromDateTime(DateTime.Now);
        EndDate = DateOnly.FromDateTime(DateTime.Now);
        BeginTime = TimeOnly.FromDateTime(DateTime.Now);
        EndTime = TimeOnly.FromDateTime(DateTime.Now);
        
    }
    
    public string? ValidateEvent()
    {
        string? errorMessage = string.Empty;


        errorMessage = ValidateDates();
        if (!string.IsNullOrWhiteSpace(errorMessage))
        {
            return errorMessage;
        }

        errorMessage = ValidateLocation();
        if (!string.IsNullOrWhiteSpace(errorMessage))
        {
            return errorMessage;
        }

        errorMessage = ValidateEventLink();
        if (!string.IsNullOrWhiteSpace(errorMessage))
        {
            return errorMessage;
        }
        
        errorMessage = ValidateCoverImage();
        if (!string.IsNullOrWhiteSpace(errorMessage))
        {
            return errorMessage;
        }

        return string.Empty;
    }
    
    private string? ValidateCoverImage()
    {
        if (CoverImage == null && EventId <= 0)
        {
            return "Cover image is required.";
        }

        return string.Empty;
    }
    
    private string? ValidateDates()
    {
        DateTime beginDateTime = BeginDate.ToDateTime(BeginTime);
        DateTime endDateTime = EndDate.ToDateTime(EndTime);
        
        if (beginDateTime >= endDateTime)
        {
            return "Begin date and time must be earlier than end date and time.";
        }

        return string.Empty;
    }
    
    private string? ValidateLocation()
    {
        if (Category == nameof(EventCategoriesEnum.InPerson) && string.IsNullOrWhiteSpace(Location))
        {
            return "Location is required for in-person events.";
        }

        return string.Empty;
    }
    
    private string? ValidateEventLink()
    {
        if (Category == nameof(EventCategoriesEnum.Online) && string.IsNullOrWhiteSpace(EventLink))
        {
            return "Event link is required for online events.";
        }

        return string.Empty;
    }

}