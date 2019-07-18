using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.Tags.Models;

namespace OrchardCore.Tags.ViewModels
{
    public class TagsPartViewModel
    {
        public IEnumerable<string> TagsCollection { get { return ParseTags(Tags); } protected set { } }

        private IEnumerable<string> ParseTags(string tags)
        {
            return (tags ?? "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public string Tags { get; set; }

        [BindNever]
        public TagsPart TagsPart { get; set; }

        [BindNever]
        public TagsPartSettings Settings { get; set; }
    }
}
