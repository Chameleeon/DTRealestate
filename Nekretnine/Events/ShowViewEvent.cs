namespace Nekretnine.Events
{
    public class ShowViewEvent
    {
        public Type ViewModelType { get; }

        public ShowViewEvent(Type viewModelType)
        {
            ViewModelType = viewModelType;
        }
    }

}
