using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Settings;
using OrchardCore.Tags.Models;
using OrchardCore.Tags.ViewModels;

namespace OrchardCore.Tags.Drivers
{
    public class TagsSettingsDisplayDriver : SectionDisplayDriver<ISite, TagsSettings>
    {
        public const string GroupId = "TagsSettings";

        public override IDisplayResult Edit(TagsSettings section)
        {
            return Initialize<TagsSettingsViewModel>("TagsSettings_Edit", model => {
                model.Tags = section.Tags;
            }).Location("Content:5").OnGroup(GroupId);
        }

        public override async Task<IDisplayResult> UpdateAsync(TagsSettings section, BuildEditorContext context)
        {
            if (context.GroupId == GroupId)
            {
                var viewModel = new TagsSettingsViewModel();
                viewModel.Tags = section.Tags;

                await context.Updater.TryUpdateModelAsync(viewModel, Prefix, t => t.Tags);

                section.Tags = viewModel.Tags;
            }
            return await EditAsync(section, context);
        }

        public override IDisplayResult Display(TagsSettings section, BuildDisplayContext context)
        {
            return Initialize<TagsSettingsViewModel>("TagsSettings", model =>
            {
                model.Tags = section.Tags;
            })
            .Location("Content")
            .Location("SummaryAdmin", "");
            ;
        }
    }
}
