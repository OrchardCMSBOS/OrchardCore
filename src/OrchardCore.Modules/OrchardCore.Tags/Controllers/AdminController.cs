using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json.Linq;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Notify;
using OrchardCore.Navigation;
using OrchardCore.Settings;
using OrchardCore.Tags.Models;
using OrchardCore.Tags.ViewModels;
using YesSql;

namespace OrchardCore.Tags.Controllers
{
    public class AdminController : Controller, IUpdateModel
    {
        private readonly IContentManager _contentManager;
        private readonly IAuthorizationService _authorizationService;
        private readonly IContentItemDisplayManager _contentItemDisplayManager;
        private readonly IContentDefinitionManager _contentDefinitionManager;
        private readonly ISiteService _siteService;
        private readonly IDisplayManager<TagsPart> _displayManager;
        private readonly ISession _session;
        private readonly INotifier _notifier;

        public AdminController(
            IDisplayManager<TagsPart> displayManager,
            ISession session,
            IContentManager contentManager,
            ISiteService siteService,
            IAuthorizationService authorizationService,
            IShapeFactory shapeFactory,
            IStringLocalizer<AdminController> stringLocalizer,
            IContentItemDisplayManager contentItemDisplayManager,
            IContentDefinitionManager contentDefinitionManager,
            INotifier notifier,
            IHtmlLocalizer<AdminController> h)
        {
            _contentManager = contentManager;
            _displayManager = displayManager;
            _authorizationService = authorizationService;
            _contentItemDisplayManager = contentItemDisplayManager;
            _contentDefinitionManager = contentDefinitionManager;
            _siteService = siteService;
            New = shapeFactory;
            _session = session;
            _notifier = notifier;
            T = stringLocalizer;
            H = h;
        }

        public IHtmlLocalizer H { get; set; }
        public IStringLocalizer T { get; set; }
        public dynamic New { get; set; }

        public async Task<IActionResult> Index(TagsIndexOptions options, PagerParameters pagerParameters)
        {
            if (!await _authorizationService.AuthorizeAsync(User, Permissions.ManageTags))
            {
                return Unauthorized();
            }

            var siteSettings = await _siteService.GetSiteSettingsAsync();
            var pager = new Pager(pagerParameters, siteSettings.PageSize);

            // default options
            if (options == null)
            {
                options = new TagsIndexOptions();
            }

            /*
            var tags = await _tagsManager.ListTagsAsync();

            if (!string.IsNullOrWhiteSpace(options.Search))
            {
                tags = tags.Where(q => q.Name.IndexOf(options.Search, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            var results = tags
                .Skip(pager.GetStartIndex())
                .Take(pager.PageSize)
                .ToList();

            // Maintain previous route data when generating page links
            var routeData = new RouteData();
            routeData.Values.Add("Options.Search", options.Search);

            var pagerShape = (await New.Pager(pager)).TotalItemCount(tags.Count()).RouteData(routeData);

            var model = new TagsIndexViewModel
            {
                Tags = new List<TagsEntry>(),
                Options = options,
                Pager = pagerShape,
                TagsSourceNames = _tagsSources.Select(x => x.Name).ToList()
            };

            foreach (var query in results)
            {
                model.Tags.Add(new TagsEntry
                {
                    Tag = tag,
                    Shape = await _displayManager.BuildDisplayAsync(query, this, "SummaryAdmin")
                });
            }
            */


            //return View(model);
            return View();
        }
    }
}
