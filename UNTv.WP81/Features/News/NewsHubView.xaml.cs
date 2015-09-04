﻿using System;
using System.Reactive.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ReactiveUI;
using UNTv.WP81.Features.Controls.ListItemControls;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace UNTv.WP81.Features.News
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewsHubView : Page, IViewFor<NewsHubViewModel>
    {
        private bool _isActivated;

        public NewsHubView()
        {
            this.InitializeComponent();
            this.InitializeBindings();
        }

        private void InitializeBindings()
        {
            this.WhenActivated(block =>
            {
                if (_isActivated)
                    return;

                Action<ListView> BindClickEvent = (listView) =>
                {
                    listView.Events().ItemClick
                        .Select(x => x.ClickedItem as ItemViewModel)
                        .Where(x => this.ViewModel.NavigateToNewsDetailCommand.CanExecute(null))
                        .Subscribe(x => this.ViewModel.NavigateToNewsDetailCommand.Execute(x));
                };

                block(this.OneWayBind(ViewModel, x => x.Headlines, x => x.HeadlinesListView.ItemsSource));
                block(this.OneWayBind(ViewModel, x => x.WorldNews, x => x.WorldNewsListView.ItemsSource));
                block(this.OneWayBind(ViewModel, x => x.SportsNews, x => x.SportsNewsListView.ItemsSource));
                block(this.OneWayBind(ViewModel, x => x.HealthNews, x => x.HealthNewsListView.ItemsSource));
                block(this.OneWayBind(ViewModel, x => x.PoliticalNews, x => x.PoliticalNewsListView.ItemsSource));
                block(this.OneWayBind(ViewModel, x => x.EducationNews, x => x.EducationNewsListView.ItemsSource));
                block(this.OneWayBind(ViewModel, x => x.TechnologyNews, x => x.TechnologyNewsListView.ItemsSource));
                block(this.OneWayBind(ViewModel, x => x.GovernmentNews, x => x.GovernmentNewsListView.ItemsSource));
                block(this.OneWayBind(ViewModel, x => x.ProvincialNews, x => x.ProvincialNewsListView.ItemsSource));
                block(this.OneWayBind(ViewModel, x => x.ScienceNews, x => x.ScienceNewsListView.ItemsSource));

                BindClickEvent(this.HeadlinesListView);
                BindClickEvent(this.WorldNewsListView);
                BindClickEvent(this.SportsNewsListView);
                BindClickEvent(this.HealthNewsListView);
                BindClickEvent(this.PoliticalNewsListView);
                BindClickEvent(this.EducationNewsListView);
                BindClickEvent(this.TechnologyNewsListView);
                BindClickEvent(this.GovernmentNewsListView);
                BindClickEvent(this.ProvincialNewsListView);
                BindClickEvent(this.ScienceNewsListView);

                this.ViewModel.PopulateCommand.Execute(null);

                _isActivated = true;
            });
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
            .Register("ViewModel", typeof(NewsHubViewModel), typeof(NewsHubView), new PropertyMetadata(null));

        public NewsHubViewModel ViewModel
        {
            get { return (NewsHubViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (NewsHubViewModel)value; }
        }
    }
}