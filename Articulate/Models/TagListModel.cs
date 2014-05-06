using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;

namespace Articulate.Models
{
    public class TagListModel : IMasterModel
    {
        public TagListModel(IMasterModel masterModel, string name, IEnumerable<PostsByTagModel> tags)
        {
            Theme = masterModel.Theme;
            RootBlogNode = masterModel.RootBlogNode;
            BlogListNode = masterModel.BlogListNode;
            Name = name;
            BlogTitle = masterModel.BlogTitle;
            BlogDescription = masterModel.BlogDescription;
            RootUrl = masterModel.RootUrl;
            Tags = tags;
        }

        public string Theme { get; private set; }
        public IPublishedContent RootBlogNode { get; private set; }
        public IPublishedContent BlogListNode { get; private set; }
        public string Name { get; private set; }
        public string BlogTitle { get; private set; }
        public string BlogDescription { get; private set; }
        public string RootUrl { get; private set; }
        public string ArchiveUrl { get; private set; }
        public IEnumerable<PostsByTagModel> Tags { get; private set; }

        private int? _maxCount;

        /// <summary>
        /// Returns a tag weight based on the current tag collection out of 10
        /// </summary>
        /// <param name="postsByTag"></param>
        /// <returns></returns>
        public int GetTagWeight(PostsByTagModel postsByTag)
        {
            if (_maxCount.HasValue == false)
            {
                _maxCount = Tags.Max(x => x.PostCount);    
            }

            return Convert.ToInt32(Math.Ceiling(postsByTag.PostCount * 10.0 / _maxCount.Value));
        }
    }
}