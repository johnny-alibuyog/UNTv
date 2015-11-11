using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UNTv.WP81.Data.Entities
{
    public class Category 
    {
        [JsonProperty("id")]
        public virtual long Id { get; set; }

        [JsonProperty("slug")]
        public virtual string Slug { get; set; }

        [JsonProperty("title")]
        public virtual string Title { get; set; }

        public virtual string Header { get; set; }

        [JsonProperty("description")]
        public virtual string Description { get; set; }

        [JsonProperty("parent")]
        public virtual long Parent { get; set; }

        [JsonProperty("post_count")]
        public virtual long PostCount { get; set; }

        public Category() { }

        public Category(long id = 0, string slug = "", string title = "", string header = "", string description = "", long parent = 0, long postCount = 0)
        {
            this.Id = id;
            this.Slug = slug;
            this.Title = title;
            this.Header = header;
            this.Description = description;
            this.Parent = parent;
            this.PostCount = postCount;
        }

        public override string ToString()
        {
            return this.Slug;
        }

        #region Equality Comparer

        private Nullable<int> _hashCode;

        public override bool Equals(object obj)
        {
            var other = obj as Category;
            if (other == null)
                return false;

            return other.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            if (_hashCode.HasValue)
            {
                return _hashCode.Value;
            }

            if (this.Id == default(int))
            {
                _hashCode = base.GetHashCode();
                return _hashCode.Value;
            }

            return Id.GetHashCode();
        }

        public static bool operator ==(Category x, Category y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(Category x, Category y)
        {
            return !(x == y);
        }

        #endregion

        public static readonly Category World = new Category(id: 7, slug: "world", title: "World", header: "world");
        public static readonly Category Sports = new Category(id: 8, slug: "sports", title: "Sports", header: "sports");
        public static readonly Category Health = new Category(id: 327, slug: "health", title: "Health", header: "health");
        public static readonly Category Headlines = new Category(id: 18, slug: "headlines", title: "Headlines", header: "headlines");
        public static readonly Category Political = new Category(id: 25, slug: "political", title: "Political", header: "political");
        public static readonly Category Education = new Category(id: 22, slug: "education", title: "Education", header: "education");
        public static readonly Category Technology = new Category(id: 15, slug: "technology", title: "Technology", header: "technology");
        public static readonly Category Government = new Category(id: 24, slug: "government", title: "Government", header: "government");
        public static readonly Category ProvincialNews = new Category(id: 6532, slug: "provincial-news", title: "Provincial News", header: "provincial");
        public static readonly Category ScienceAndEnvironment = new Category(id: 23, slug: "science-and-environment", title: "Science and Environment", header: "science");

        public static readonly IEnumerable<Category> All = new Category[]
        {
            Category.World,
            Category.Sports,
            Category.Health,
            Category.Headlines,
            Category.Political,
            Category.Education,
            Category.Technology,
            Category.Government,
            Category.ProvincialNews,
            Category.ScienceAndEnvironment
        };

        public static Category GetByHeader(string header)
        {
            return Category.All.FirstOrDefault(x => x.Header == header);
        }
    }
}
