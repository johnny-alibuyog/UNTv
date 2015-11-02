﻿using System;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;
using UNTv.WP81.Common.IO;

namespace UNTv.WP81.Features.About
{
    public class AboutSectionViewModel : ReactiveBase
    {
        public virtual string Content { get; private set; }
        public virtual ReactiveCommand<object> PopulateCommand { get; set; }

        public AboutSectionViewModel()
        {
            this.PopulateCommand = ReactiveCommand.Create();
            this.PopulateCommand.Subscribe(x => Populate());
        }

        private void Populate()
        {
            if (string.IsNullOrEmpty(this.Content))
            {
                var reader = Locator.CurrentMutable.GetService<ITextReader>();
                reader.Read("AboutSectionContent.html", @"\Features\About").ContinueWith(
                    continuationAction: x => this.Content = x.Result,
                    scheduler: TaskScheduler.FromCurrentSynchronizationContext()
                );
            }
        }
    }
}
