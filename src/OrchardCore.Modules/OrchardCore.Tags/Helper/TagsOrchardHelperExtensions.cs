using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using OrchardCore;
using OrchardCore.ContentManagement;
using OrchardCore.Tags.Indexing;
using YesSql;

public static class TagsOrchardHelperExtensions
{
    /// <summary>
    /// Query content items.
    /// </summary>
    public static async Task<IEnumerable<ContentItem>> QueryTaggedContentItemsAsync(this IOrchardHelper orchardHelper, Func<IQuery<ContentItem, TagsPartIndex>, IQuery<ContentItem>> query)
    {
        var contentManager = orchardHelper.HttpContext.RequestServices.GetService<IContentManager>();
        var session = orchardHelper.HttpContext.RequestServices.GetService<ISession>();

        var contentItems = await query(session.Query<ContentItem, TagsPartIndex>()).ListAsync();

        return await contentManager.LoadAsync(contentItems);
    }
}
