using System;
using System.Collections.Generic;

namespace OrchardCore.Tags.ViewModels
{
    public class TagsSettingsViewModel
    {
        public IEnumerable<string> TagsCollection { get { return ParseTags(Tags); } protected set { } }

        public string Tags { get; set; }

        private IEnumerable<string> ParseTags(string tags)
        {
            return (tags ?? "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
