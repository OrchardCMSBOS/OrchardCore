using System;
using Fluid;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Apis;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.Data;
using OrchardCore.Data.Migration;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Indexing;
using OrchardCore.Liquid;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using OrchardCore.Security.Permissions;
using OrchardCore.Tags.Drivers;
using OrchardCore.Tags.GraphQL;
using OrchardCore.Tags.Handlers;
using OrchardCore.Tags.Indexing;
using OrchardCore.Tags.Liquid;
using OrchardCore.Tags.Models;
using OrchardCore.Tags.Settings;
using YesSql.Indexes;
using OrchardCore.Settings;

namespace OrchardCore.Tags
{
    public class Startup : StartupBase
    {
        static Startup()
        {

        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDataMigration, Migrations>();
            services.AddScoped<IPermissionProvider, Permissions>();
            services.AddScoped<INavigationProvider, AdminMenu>();

            // Tags Part
            services.AddScoped<IContentPartDisplayDriver, TagsPartDisplay>();
            services.AddSingleton<ContentPart, TagsPart>();
            services.AddScoped<IContentPartHandler, TagsPartHandler>();
            services.AddScoped<IContentTypePartDefinitionDisplayDriver, TagsPartSettingsDisplayDriver>();
            services.AddScoped<IContentPartIndexHandler, TagsPartIndexHandler>();
            services.AddSingleton<IIndexProvider, TagsPartIndexProvider>();
            services.AddScoped<IDisplayDriver<ISite>, TagsSettingsDisplayDriver>();

        }

        public override void Configure(IApplicationBuilder app, IRouteBuilder routes, IServiceProvider serviceProvider)
        {

        }
    }

    [RequireFeatures("OrchardCore.Liquid")]
    public class LiquidStartup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddLiquidFilter<TagsPartFilter>("tags");
        }
    }

    [RequireFeatures("OrchardCore.Apis.GraphQL")]
    public class GraphQLStartup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddObjectGraphType<TagsPart, TagsPartQueryObjectType>();
        }
    }
}
