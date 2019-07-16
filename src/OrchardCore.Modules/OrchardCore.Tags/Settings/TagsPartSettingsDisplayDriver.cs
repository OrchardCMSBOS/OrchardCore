using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Liquid;
using OrchardCore.Tags.Models;
using OrchardCore.Tags.ViewModels;

namespace OrchardCore.Tags.Settings
{
    public class TagsPartSettingsDisplayDriver : ContentTypePartDefinitionDisplayDriver
    {
        private readonly ILiquidTemplateManager _templateManager;

        public TagsPartSettingsDisplayDriver(ILiquidTemplateManager templateManager, IStringLocalizer<TagsPartSettingsDisplayDriver> localizer)
        {
            _templateManager = templateManager;
            T = localizer;
        }

        public IStringLocalizer T { get; private set; }

        public override IDisplayResult Edit(ContentTypePartDefinition contentTypePartDefinition, IUpdateModel updater)
        {
            if (!String.Equals(nameof(TagsPart), contentTypePartDefinition.PartDefinition.Name, StringComparison.Ordinal))
            {
                return null;
            }

            return Initialize<TagsPartSettingsViewModel>("TagsPartSettings_Edit", model =>
            {
                var settings = contentTypePartDefinition.Settings.ToObject<TagsPartSettings>();

                model.AllowMultiple = settings.AllowMultiple;
            }).Location("Content");
        }

        public override async Task<IDisplayResult> UpdateAsync(ContentTypePartDefinition contentTypePartDefinition, UpdateTypePartEditorContext context)
        {
            if (!String.Equals(nameof(TagsPart), contentTypePartDefinition.PartDefinition.Name, StringComparison.Ordinal))
            {
                return null;
            }

            var model = new TagsPartSettingsViewModel();

            await context.Updater.TryUpdateModelAsync(model, Prefix, 
                m => m.AllowMultiple);

            context.Builder.WithSetting(nameof(TagsPartSettings.AllowMultiple), model.AllowMultiple.ToString());

            return Edit(contentTypePartDefinition, context.Updater);
        }
    }
}