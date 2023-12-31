using MauiAppDemo.Models;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiAppDemo.Services
{
    public static class PDFDemoService
    {
        public static void SavePDF()
        {
            var filePath = Utils.GetUserDirectory();
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                if (json.Trim().Length > 0)
                {
                    var deserializedData = JsonSerializer.Deserialize<List<User>>(json);
                    if (deserializedData != null)
                    {
                        var appPath = Utils.GetAppDirectory();
                        Document.Create(container =>
                        {
                            container.Page(page =>
                            {
                                page.Header().Text("Users");

                                page.Content().Table(table =>
                                {
                                    table.ColumnsDefinition(column =>
                                    {
                                        column.RelativeColumn();
                                        column.RelativeColumn();
                                    });

                                    table.Header(header =>
                                    {
                                        header.Cell().Text("Username");
                                        header.Cell().Text("Role");
                                    });

                                    foreach (var item in deserializedData)
                                    {
                                        table.Cell().Text(item.Username);
                                        table.Cell().Text(item.Role.ToString());
                                    }
                                });

                                page.Footer().Text(text =>
                                {
                                    text.Span("Page :");
                                    text.CurrentPageNumber();
                                });
                            });
                        }).GeneratePdf(Path.Combine(appPath, "Users.pdf"));
                    }
                }
            }
        }
    }
}
