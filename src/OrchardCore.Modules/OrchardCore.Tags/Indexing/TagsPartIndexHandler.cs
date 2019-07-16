using System.Linq;
using System.Threading.Tasks;
using OrchardCore.Indexing;
using OrchardCore.Tags.Models;

namespace OrchardCore.Tags.Indexing
{
    public class TagsPartIndexHandler : ContentPartIndexHandler<TagsPart>
    {
        public override Task BuildIndexAsync(TagsPart part, BuildPartIndexContext context)
        {
            var options = context.Settings.ToOptions() 
                & ~DocumentIndexOptions.Sanitize
                & ~DocumentIndexOptions.Analyze
                ;

            foreach (var key in context.Keys)
            {
                context.DocumentIndex.Set(key, string.Join(", ", part.Tags), options);
            }

            return Task.CompletedTask;
        }
    }
}
