namespace Nekretnine.Events
{
    public class NavigateToOffersEvent
    {
        public string SearchQuery { get; }

        public NavigateToOffersEvent(string searchQuery)
        {
            SearchQuery = searchQuery;
        }
    }

}
