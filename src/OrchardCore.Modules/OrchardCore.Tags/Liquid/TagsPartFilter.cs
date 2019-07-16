using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fluid;
using Fluid.Values;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using OrchardCore.ContentManagement;
using OrchardCore.Liquid;
using OrchardCore.Tags.Models;

namespace OrchardCore.Tags.Liquid
{
    public class TagsPartFilter : ILiquidFilter
    {
        public async ValueTask<FluidValue> ProcessAsync(FluidValue input, FilterArguments arguments, TemplateContext ctx)
        {
            if (!ctx.AmbientValues.TryGetValue("Services", out var services))
            {
                throw new ArgumentException("Services missing while invoking 'tags'");
            }

            var contentManager = ((IServiceProvider)services).GetRequiredService<IContentManager>();

            if (input.ToObjectValue() is TagsPart)
            { var taggedItem = await contentManager.GetAsync(input.ToStringValue()); }

            var tags = new List<ContentItem>();

            return FluidValue.Create(tags);
        }
    }
}
