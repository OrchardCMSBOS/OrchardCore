using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.Tags.Models;

namespace OrchardCore.Tags.ViewModels
{
    public class TagsPartViewModel
    {
        public string Tags { get; set; }

        public string GlobalTags { get; set; }

        public IEnumerable<string> GlobalTagsCollection { get { return ParseTags(GlobalTags); } protected set { } }

        public IEnumerable<string> TagsCollection { get { return ParseTags(Tags); } protected set { } }

        [BindNever]
        public TagsPart TagsPart { get; set; }

        [BindNever]
        public TagsPartSettings Settings { get; set; }

        private IEnumerable<string> ParseTags(string tags)
        {
            return (tags ?? "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

    }
}
