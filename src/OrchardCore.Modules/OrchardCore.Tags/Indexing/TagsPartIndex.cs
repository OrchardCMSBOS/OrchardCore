using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data;
using OrchardCore.Tags.Models;
using YesSql.Indexes;

namespace OrchardCore.Tags.Indexing
{
    // Remark: 

    public class TagsPartIndex : MapIndex
    {
        public string ContentItemId { get; set; }
        public string Tags { get; set; }
        public bool Published { get; set; }
    }

    public class TagsPartIndexProvider : IndexProvider<ContentItem>
    {
        public override void Describe(DescribeContext<ContentItem> context)
        {
            context.For<TagsPartIndex>()
                .Map(contentItem =>
                {
                    var tagsArray = contentItem.As<TagsPart>()?.Tags;
                    var tags = string.Join(", ", tagsArray);
                    if (!String.IsNullOrEmpty(tags) && (contentItem.Published || contentItem.Latest))
                    {
                        return new TagsPartIndex
                        {
                            ContentItemId = contentItem.ContentItemId,
                            Tags = tags,
                            Published = contentItem.Published
                        };
                    }

                    return null;
                });
        }
    }
}