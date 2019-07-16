# Tags (OrchardCore.Tags)

This modules provides a Tags content part that is used to define keywords associated with content items.

## Shapes

### Site Settings

Provides a way to return all tags in the system.

OrchardCore.Settings.ISiteService _site
var tags = (await _site.GetSiteSettingsAsync()).As<TagsSettings>().Tags;

## Orchard Helpers

### QueryTaggedContentItemsAsync

Provides a way to query content items that are tagged with specific tags.

## Liquid Tags

### tags

The `tags` filter loads the specified tags on a specified content item.

#### Example 

The following example lists all the tags assigned for the BlogPost
content type, then renders them.

```liquid
{% assign tag = Model.ContentItem.Content.BlogPost.Tags | tags %}
{% for t in tag %}
  {{ t }}
{% endfor %}
```

## Tags Index

The `TagsIndex` SQL table containes a list of all content items that are associated 
with a tag. Each record corresponds to a selected tag for a field.

| Column | Type | Description |
| --------- | ---- |------------ |
| ContentItemId | `string` | The content item id of the Tag |
| Tags | `string` | The tags on the item |
| Published | `bool` | The item is published or not |
