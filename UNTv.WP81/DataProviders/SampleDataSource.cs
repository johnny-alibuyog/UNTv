using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

// The data model defined by this file serves as a representative example of a strongly-typed
// model.  The property names chosen coincide with data bindings in the standard item templates.
//
// Applications may use this model as a starting point and build on it, or discard it entirely and
// replace it with something appropriate to their needs. If using this model, you might improve app 
// responsiveness by initiating the data loading task in the code behind for App.xaml when the app 
// is first launched.

namespace UNTv.WP81.DataProviders
{
    /// <summary>
    /// Generic item data model.
    /// </summary>
    public class SampleDataItem
    {
        public SampleDataItem(String uniqueId, String title, String subtitle, String imagePath, String description, String content)
        {
            this.UniqueId = uniqueId;
            this.Title = title;
            this.Subtitle = subtitle;
            this.Description = description;
            this.ImagePath = imagePath;
            this.Content = content;
        }

        public string UniqueId { get; private set; }
        public string Title { get; private set; }
        public string Subtitle { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public string Content { get; private set; }

        public override string ToString()
        {
            return this.Title;
        }
    }

    /// <summary>
    /// Generic group data model.
    /// </summary>
    public class SampleDataGroup
    {
        public SampleDataGroup(String uniqueId, String title, String subtitle, String imagePath, String description)
        {
            this.UniqueId = uniqueId;
            this.Title = title;
            this.Subtitle = subtitle;
            this.Description = description;
            this.ImagePath = imagePath;
            this.Items = new ObservableCollection<SampleDataItem>();
        }

        public string UniqueId { get; private set; }
        public string Title { get; private set; }
        public string Subtitle { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public ObservableCollection<SampleDataItem> Items { get; private set; }

        public override string ToString()
        {
            return this.Title;
        }
    }

    /// <summary>
    /// Creates a collection of groups and items with content read from a static json file.
    /// 
    /// SampleDataSource initializes with data read from a static json file included in the 
    /// project.  This provides sample data at both design-time and run-time.
    /// </summary>
    public sealed class SampleDataSource
    {
        private static SampleDataSource _sampleDataSource = new SampleDataSource();
        private ObservableCollection<SampleDataGroup> _groups = new ObservableCollection<SampleDataGroup>();

        public ObservableCollection<SampleDataGroup> Groups
        {
            get { return this._groups; }
        }

        public static async Task<IEnumerable<SampleDataGroup>> GetGroupsAsync()
        {
            await _sampleDataSource.GetSampleDataAsync();
            return _sampleDataSource.Groups;
        }

        public static async Task<SampleDataGroup> GetGroupAsync(string uniqueId)
        {
            await _sampleDataSource.GetSampleDataAsync();
            return _sampleDataSource.Groups.FirstOrDefault(x => x.UniqueId.Equals(uniqueId));
        }

        public static async Task<SampleDataItem> GetItemAsync(string uniqueId)
        {
            await _sampleDataSource.GetSampleDataAsync();
            return _sampleDataSource.Groups.SelectMany(x => x.Items).FirstOrDefault(x => x.UniqueId.Equals(uniqueId));
        }

        public static IEnumerable<SampleDataGroup> GetGroups()
        {
            _sampleDataSource.GetSampleDataAsync().Wait();
            return _sampleDataSource.Groups;
        }


        private async Task GetSampleDataAsync()
        {
            if (this._groups.Count != 0)
                return;

            var dataUri = new Uri("ms-appx:///DataProviders/SampleData.json");

            var file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
            var jsonText = await FileIO.ReadTextAsync(file);
            var jsonObject = JsonObject.Parse(jsonText);
            var jsonArray = jsonObject["Groups"].GetArray();

            foreach (var groupValue in jsonArray)
            {
                var groupObject = groupValue.GetObject();
                var group = new SampleDataGroup(
                    groupObject["UniqueId"].GetString(),
                    groupObject["Title"].GetString(),
                    groupObject["Subtitle"].GetString(),
                    groupObject["ImagePath"].GetString(),
                    groupObject["Description"].GetString()
                );

                foreach (var itemValue in groupObject["Items"].GetArray())
                {
                    var itemObject = itemValue.GetObject();
                    group.Items.Add(new SampleDataItem(
                        itemObject["UniqueId"].GetString(),
                        itemObject["Title"].GetString(),
                        itemObject["Subtitle"].GetString(),
                        itemObject["ImagePath"].GetString(),
                        itemObject["Description"].GetString(),
                        itemObject["Content"].GetString())
                    );
                }
                this.Groups.Add(group);
            }
        }
    }
}