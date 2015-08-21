using ReactiveUI;

namespace UNTv.WP81.Features
{
    public class ReactiveBase : ReactiveObject
    {
        protected void OnPropertyChanged(string propertyName)
        {
            this.RaisePropertyChanged(propertyName);
        }

        protected void OnPropertyChanging(string propertyName)
        {
            this.RaisePropertyChanging(propertyName);
        }
    }
}
