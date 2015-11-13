using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;
using UNTv.WP81.Common.IO;
using Windows.ApplicationModel;

namespace UNTv.WP81.Features.ContactUs
{
    public class ContactUsSectionViewModel : ReactiveBase
    {
        public virtual string Content { get; private set; }
        public virtual ReactiveCommand<object> PopulateCommand { get; set; }

        public ContactUsSectionViewModel()
        {
            this.PopulateCommand = ReactiveCommand.Create();
            this.PopulateCommand.Subscribe(x => Populate());
        }

        private void Populate()
        {
            if (string.IsNullOrEmpty(this.Content))
            {
                var path = Package.Current.InstalledLocation.Path + @"\Features\ContactUs";
                var reader = Locator.CurrentMutable.GetService<ITextReader>();
                reader.Read("ContactUsSectionContent.html", path).ContinueWith(
                    continuationAction: x => this.Content = x.Result,
                    scheduler: TaskScheduler.FromCurrentSynchronizationContext()
                );
            }
        }
    }
}
