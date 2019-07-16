using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.Tags.Indexing;

namespace OrchardCore.Tags
{
    public class Migrations : DataMigration
    {
        IContentDefinitionManager _contentDefinitionManager;

        public Migrations(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        public int Create()
        {
            _contentDefinitionManager.AlterPartDefinition("TagsPart", builder => builder
            .Attachable()
            .WithDescription("Provides a custom tagging mechanism for your content items."));

            SchemaBuilder.CreateMapIndexTable(nameof(TagsPartIndex), table => table
                .Column<string>("ContentItemId", c => c.WithLength(26))
                .Column<string>("Tags", col => col.WithLength(5000))
                .Column<bool>("Published")
            );

            SchemaBuilder.AlterTable(nameof(TagsPartIndex), table => table
                .CreateIndex("IDX_TagsPartIndex_ContentItemId", "ContentItemId")
            );

            return 1;
        }
    }
}
