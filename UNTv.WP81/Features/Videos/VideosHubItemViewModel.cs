using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using UNTv.WP81.Data.Contracts.Messages;
using UNTv.WP81.Features.Controls.ListItemControls;

namespace UNTv.WP81.Features.Videos
{
    public class VideosHubItemViewModel : ReactiveBase
    {
        public virtual SortFilter SortFilter { get; set; }
        public virtual ReactiveList<ItemViewModel> Items { get; set; }

        public VideosHubItemViewModel(SortFilter sortFilter = SortFilter.Latest, ReactiveList<ItemViewModel> items = null)
        {
            this.SortFilter = sortFilter;
            this.Items = items;
        }
    }
}