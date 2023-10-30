using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;

QuestPDF.Settings.License = LicenseType.Community;

Document.Create(document =>
{
    document.Page(page =>
    {
        page.Size(PageSizes.A4);

        page.Margin(20, Unit.Millimetre);

        page.Header()
            .Row(row =>
            {
                row.RelativeItem()
                    // .DebugArea()
                    // .PaddingLeft(25)
                    .Column(column =>
                    {
                        column.Item().Text(Placeholders.Name()).FontSize(30);
                        column.Item().Text(Placeholders.Label());
                    });

                row.ConstantItem(125)
                    // .DebugArea()
                    .Image(Placeholders.Image(125, 80));
            });

        page.Content().Element(Content);

        page.Footer()
            .AlignRight()
            .Text(x =>
            {
                x.Span("Sida ");
                x.CurrentPageNumber();
                x.Span("/");
                x.TotalPages();
            });
    });
}).ShowInPreviewer();

static void AddressThings(IContainer container)
{
    container
        .DebugArea()
        .Text(Placeholders.Label());
}

static void Content(IContainer container)
{
    container
        .Column(column =>
        {
            column
                .Item()
                .PaddingVertical(10)
                .DebugArea()
                .Element(AddressThings);

            column        // .DebugArea()
                .Item()
                .PaddingVertical(10)
                .Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(50);
                        columns.RelativeColumn();
                        columns.ConstantColumn(50);
                        columns.ConstantColumn(75);
                        columns.ConstantColumn(75);
                    });

                    table.Header(header =>
                    {
                        header.Cell().Text("#").SemiBold();
                        header.Cell().Text("Description").SemiBold();
                        header.Cell().AlignCenter().Text("Quantity").SemiBold();
                        header.Cell().AlignRight().Text("Price").SemiBold();
                        header.Cell().AlignRight().Text("Total").SemiBold();
                    });

                    foreach (var item in Enumerable.Range(1, 50))
                    {
                        var quantity = Placeholders.Random.Next(1, 10);
                        var price = Placeholders.Random.NextDouble() * 250;
                        var total = quantity * price;

                        table.Cell().Text($"{item}");
                        table.Cell().Column(column =>
                        {
                            column.Item().Text(Placeholders.Label());
                            column.Item().Text(Placeholders.Sentence()).FontColor(Colors.Grey.Medium).Italic();
                        });
                        table.Cell().AlignCenter().Text($"{quantity}");
                        table.Cell().AlignRight().Text($"{price:F2} kr");
                        table.Cell().AlignRight().Text($"{total:F2} kr");
                    }
                });
        });

}