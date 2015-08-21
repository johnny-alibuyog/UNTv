using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace UNTv.WP81.DataProviders
{
    public class Item { }

    public class CommonViewModel : ReactiveObject, IRoutableViewModel
    {
        private readonly RemoteHttpService _remoteClient;

        private ReactiveList<Item> _items;
        public ReactiveList<Item> ItemsFromServer
        {
            get { return _items; }
            set { this.RaiseAndSetIfChanged(ref _items, value); }
        }

        public ReactiveCommand<List<Item>> LoadItems { get; protected set; }
        public ReactiveCommand<object> SendItemsToServerCommand { get; protected set; }

        public string UrlPathSegment
        {
            get { return "CommonViewModel View"; }
        }

        public IScreen HostScreen { get; private set; }

        public CommonViewModel(IScreen screen)
        {
            HostScreen = screen;
            ItemsFromServer = new ReactiveList<Item>();
            _remoteClient = new RemoteHttpService(); //using refit and polly behind the scenes.

            SendItemsToServerCommand = ReactiveCommand.CreateAsyncTask((_, ctx) => InternalSendItemsToServer());
            SendItemsToServerCommand.ThrownExceptions.Subscribe(ex => UserError.Throw("Could not send data to server", ex));

            LoadItems = ReactiveCommand.CreateAsyncObservable(_ => this.InternalGetItemsFromCacheAndServer());
            LoadItems.Subscribe(list =>
            {
                _items.Clear();
                foreach (var item in list)
                {
                    //using AddRange throw Presentation Exception
                    _items.Add(item);
                }
            });
            LoadItems.ThrownExceptions.Subscribe(ex => UserError.Throw("Could not get data from server"));
            LoadItems.ExecuteAsyncTask();
        }

        private IObservable<List<Item>> InternalGetItemsFromCacheAndServer()
        {
            return BlobCache.LocalMachine.GetAndFetchLatest("items",
            InternalGetRemoteItems,
            dt => true,
            RxApp.MainThreadScheduler.Now + TimeSpan.FromMinutes(5));
        }

        private async Task<List<Item>> InternalGetRemoteItems()
        {
            var conferences = await _remoteClient.GetItems().ConfigureAwait(false);
            return conferences;
        }

        private async Task<object> InternalSendItemsToServer()
        {
            var itemsToSendToServer = ItemsFromServer.SomeLinq().Maybe();
            return await _remoteClient.SendDataToServer(itemsToSendToServer);
        }
    }
}
