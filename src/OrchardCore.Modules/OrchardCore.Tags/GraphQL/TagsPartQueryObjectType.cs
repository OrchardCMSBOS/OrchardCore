using System.Collections.Generic;
using GraphQL.Types;
using OrchardCore.Apis.GraphQL;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.GraphQL.Queries.Types;
using OrchardCore.Tags.Models;

namespace OrchardCore.Tags.GraphQL
{
    public class TagsPartQueryObjectType : ObjectGraphType<TagsPart>
    {
        public TagsPartQueryObjectType()
        {
            Name = "TagsPart";

            /*
            Field<ListGraphType<ContentItemInterface>, IEnumerable<ContentItem>>()
                .Name("contentItems")
                .Description("the content items")
                .PagingArguments()
                .Resolve(x => x.Page(x.Source.Tags));

    */
        }
    }
}
