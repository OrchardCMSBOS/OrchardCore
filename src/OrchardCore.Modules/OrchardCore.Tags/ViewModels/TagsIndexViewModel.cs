using System.Collections.Generic;
using OrchardCore.Tags.Models;

namespace OrchardCore.Tags.ViewModels
{
    public class TagsIndexViewModel
    {
        public IList<TagsEntry> Tags { get; set; }
        public TagsIndexOptions Options { get; set; }
        public dynamic Pager { get; set; }
        public IEnumerable<string> TagsSourceNames { get; set; }
    }

    public class TagsEntry
    {
        public TagsPart Tag { get; set; }
        public bool IsChecked { get; set; }
        public dynamic Shape { get; set; }
    }

    public class TagsIndexOptions
    {
        public string Search { get; set; }
    }
}
