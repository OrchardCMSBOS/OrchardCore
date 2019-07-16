using System;
using System.Collections.Generic;
using OrchardCore.ContentManagement;

namespace OrchardCore.Tags.Models
{
    public class TagsPart : ContentPart
    {
        public IEnumerable<string> Tags { get; set; } = Array.Empty<string>();
        private IEnumerable<string> ParseTags(string tags)
        {
            return (tags ?? "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
