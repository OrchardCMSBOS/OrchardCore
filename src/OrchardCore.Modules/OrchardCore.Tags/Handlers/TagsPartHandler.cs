using OrchardCore.Tags.Models;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Environment.Cache;
using OrchardCore.Liquid;
using OrchardCore.Settings;
using YesSql;

namespace OrchardCore.Tags.Handlers
{
    public class TagsPartHandler : ContentPartHandler<TagsPart>
    {
        private readonly ILiquidTemplateManager _liquidTemplateManager;
        private readonly IContentDefinitionManager _contentDefinitionManager;
        private readonly ISiteService _siteService;
        private readonly ITagCache _tagCache;
        private readonly YesSql.ISession _session;

        public TagsPartHandler(
            ILiquidTemplateManager liquidTemplateManager,
            IContentDefinitionManager contentDefinitionManager,
            ISiteService siteService,
            ITagCache tagCache,
            YesSql.ISession session)
        {
            _contentDefinitionManager = contentDefinitionManager;
            _liquidTemplateManager = liquidTemplateManager;
            _siteService = siteService;
            _tagCache = tagCache;
            _session = session;
        }

        /*
        public override async Task PublishedAsync(PublishContentContext context, TagsPart part)
        {
            return Task.CompletedTask;
        }

        public override Task UnpublishedAsync(PublishContentContext context, TagsPart part)
        {


            return Task.CompletedTask;
        }

        public override Task RemovedAsync(RemoveContentContext context, TagsPart part)
        {

            return Task.CompletedTask;
        }

        public override async Task UpdatedAsync(UpdateContentContext context, TagsPart part)
        {
            return Task.CompletedTask;
        }
        */
    }
}
