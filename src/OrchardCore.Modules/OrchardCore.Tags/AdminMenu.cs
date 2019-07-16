using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using OrchardCore.Tags.Drivers;
using System;
using System.Threading.Tasks;

namespace OrchardCore.Tags
{
    public class AdminMenu : INavigationProvider
    {
        public AdminMenu(IStringLocalizer<AdminMenu> localizer)
        {
            T = localizer;
        }

        public IStringLocalizer T { get; set; }

        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (!String.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
            {
                return Task.CompletedTask;
            }

            builder.Add(T["Configuration"], configuration => configuration
                  .Add(T["Settings"], settings => settings
                      .Add(T["Tags"], T["Tags"], tags => tags
                          .Permission(Permissions.ManageTags)
                          .Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = TagsSettingsDisplayDriver.GroupId })
                          .LocalNav()
                      )));

            return Task.CompletedTask;
        }
    }
}
