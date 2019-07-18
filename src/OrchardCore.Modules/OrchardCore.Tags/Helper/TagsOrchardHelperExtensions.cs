using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using OrchardCore;
using OrchardCore.ContentManagement;
using OrchardCore.Entities;
using OrchardCore.Settings;
using OrchardCore.Tags.Indexing;
using OrchardCore.Tags.Models;
using OrchardCore.Tags.ViewModels;
using YesSql;

public static class TagsOrchardHelperExtensions
{
    /// <summary>
    /// Query all system tags.
    /// </summary>
    public static async Task<IEnumerable<string>> QueryAllTagsAsync(this IOrchardHelper orchardHelper)
    {
        var siteService = orchardHelper.HttpContext.RequestServices.GetService<ISiteService>();

        var siteSettings = await siteService.GetSiteSettingsAsync();
        var globalTags = siteSettings.As<TagsSettings>().Tags;

        var vm = new TagsSettingsViewModel();
        vm.Tags = globalTags;

        return vm.TagsCollection;
    }

    /// <summary>
    /// Query all tagged content items.
    /// </summary>
    public static async Task<IEnumerable<ContentItem>> QueryTaggedContentItemsAsync(this IOrchardHelper orchardHelper, Func<IQuery<ContentItem, TagsPartIndex>, IQuery<ContentItem>> query)
    {
        var contentManager = orchardHelper.HttpContext.RequestServices.GetService<IContentManager>();
        var session = orchardHelper.HttpContext.RequestServices.GetService<ISession>();

        var contentItems = await query(session.Query<ContentItem, TagsPartIndex>()).ListAsync();

        return await contentManager.LoadAsync(contentItems);
    }

    /// <summary>
    /// Query all tagged content items by tag.
    /// </summary>
    public static async Task<IEnumerable<ContentItem>> QueryTaggedContentItemsByTagsAsync(this IOrchardHelper orchardHelper, IEnumerable<string> tags)
    {
        var contentManager = orchardHelper.HttpContext.RequestServices.GetService<IContentManager>();
        var session = orchardHelper.HttpContext.RequestServices.GetService<ISession>();

        var contentItems = await session.Query<ContentItem, TagsPartIndex>().ListAsync();
        var filteredItems = new List<ContentItem>();

        foreach (var ci in contentItems)
        {
            var tagsString = ci.Content.TagsPart.ToObject<TagsPart>().Tags;
            var vm = new TagsSettingsViewModel();
            vm.Tags = tagsString;

            if (vm.TagsCollection.Any(x => tags.Contains(x)))
                filteredItems.Add(ci);
        }

        return filteredItems;
    }
}
