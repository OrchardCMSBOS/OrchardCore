using System.Collections.Generic;
using OrchardCore.Security.Permissions;

namespace OrchardCore.Tags
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission ManageTags = new Permission("ManageTags", "Manage tags");

        public IEnumerable<Permission> GetPermissions()
        {
            return new[] { ManageTags };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name = "Administrator",
                    Permissions = new[] { ManageTags }
                }
            };
        }
    }
}