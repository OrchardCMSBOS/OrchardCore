using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "Tags",
    Author = "The Orchard Team",
    Website = "https://orchardproject.net",
    Version = "2.0.0",
    Description = "The tags module is providing basic tagging for arbitrary content types.",
    Dependencies = new [] { "OrchardCore.ContentTypes" },
    Category = "Content Management"
)]
