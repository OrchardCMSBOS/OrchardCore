using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.Tags.Models;

namespace OrchardCore.Tags.ViewModels
{
    public class TagsPartViewModel
    {
        public string Tags { get; set; }

        [BindNever]
        public TagsPart TagsPart { get; set; }

        [BindNever]
        public TagsPartSettings Settings { get; set; }
    }
}
