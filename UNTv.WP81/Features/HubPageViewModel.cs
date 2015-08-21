using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;
using UNTv.WP81.Features;
using UNTv.WP81.Features.News;

namespace UNTv.WP81.Features
{
    public class HubPageViewModel : ReactiveRoutableBase
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public ReactiveList<UNTv.WP81.DataProviders.SampleDataGroup> Groups { get; set; }

        public ReactiveList<UNTv.WP81.DataProviders.SampleDataItem> Items { get; set; }

        public ReactiveCommand<Object> GoToBasicCommand { get; set; }

        public ReactiveCommand<IEnumerable<UNTv.WP81.DataProviders.SampleDataGroup>> GetDataCommand { get; set; }

        public HubPageViewModel(IScreen hostScreen = null)
            : base(hostScreen)
        {
            this.Name = "johnny";
            this.Address = "Antipolo";
            this.UrlPathSegment = "/hub";

            this.Groups = new ReactiveList<DataProviders.SampleDataGroup>(UNTv.WP81.DataProviders.SampleDataSource.GetGroups());
            this.Items = new ReactiveList<DataProviders.SampleDataItem>(this.Groups.FirstOrDefault().Items);

            GoToBasicCommand = ReactiveCommand.Create();
            GoToBasicCommand.Subscribe(x => GotoBasic());
            GetDataCommand = ReactiveCommand.CreateAsyncTask(async _ => await UNTv.WP81.DataProviders.SampleDataSource.GetGroupsAsync());
        }

        private void GotoBasic()
        {
            this.HostScreen.Router.Navigate.Execute(new NewsPageViewModel());
        }

        public async Task GetData()
        {
            this.Groups = new ReactiveList<DataProviders.SampleDataGroup>(await UNTv.WP81.DataProviders.SampleDataSource.GetGroupsAsync());
        }
    }
}
