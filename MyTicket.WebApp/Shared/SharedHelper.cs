namespace MyTicket.WebApp.Shared;

public class SharedHelper
{
        
    // Return a list of categories based on the enum in Shared/EventCategoriesEnum.cs
    public List<string> GetEventCategories()
    {
        return Enum.GetNames<EventCategoriesEnum>().ToList();
    }
}