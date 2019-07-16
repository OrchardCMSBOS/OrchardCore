using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Records;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Mvc.ModelBinding;
using OrchardCore.Settings;
using OrchardCore.Tags.Models;
using OrchardCore.Tags.ViewModels;
using YesSql;

namespace OrchardCore.Tags.Drivers
{
    public class TagsPartDisplay : ContentPartDisplayDriver<TagsPart>
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;
        private readonly ISiteService _siteService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly YesSql.ISession _session;
        private readonly IStringLocalizer<TagsPartDisplay> T;

        public TagsPartDisplay(
            IContentDefinitionManager contentDefinitionManager,
            ISiteService siteService,
            IAuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor,
            YesSql.ISession session,
            IStringLocalizer<TagsPartDisplay> localizer
            )
        {
            _contentDefinitionManager = contentDefinitionManager;
            _siteService = siteService;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
            _session = session;
            T = localizer;
        }

        public override IDisplayResult Edit(TagsPart tagsPart)
        {
            return Initialize<TagsPartViewModel>("TagsPart_Edit", async model =>
            {

                model.Tags = String.Join(", ", tagsPart.Tags.ToArray());
                model.TagsPart = tagsPart;

                var siteSettings = await _siteService.GetSiteSettingsAsync();
 
                model.Settings = GetSettings(tagsPart);
            });
        }

        private TagsPartSettings GetSettings(TagsPart tagsPart)
        {
            var contentTypeDefinition = _contentDefinitionManager.GetTypeDefinition(tagsPart.ContentItem.ContentType);
            var contentTypePartDefinition = contentTypeDefinition.Parts.FirstOrDefault(x => String.Equals(x.PartDefinition.Name, nameof(TagsPart), StringComparison.Ordinal));
            return contentTypePartDefinition.Settings.ToObject<TagsPartSettings>();
        }

        public override async Task<IDisplayResult> UpdateAsync(TagsPart model, IUpdateModel updater)
        {
            var viewModel = new TagsPartViewModel();

            await updater.TryUpdateModelAsync(viewModel, Prefix, t => t.Tags);

            var settings = GetSettings(model);

            var httpContext = _httpContextAccessor.HttpContext;

            model.Tags = viewModel.Tags.Split(new char[] { ',' });

            await updater.TryUpdateModelAsync(model, Prefix, t => t.Tags);

            //await ValidateAsync(model, updater);

            return Edit(model);
        }

        /*
        private async Task ValidateAsync(TagsPart tags, IUpdateModel updater)
        {
            //any validation we may need
        }
        */
    }
}
