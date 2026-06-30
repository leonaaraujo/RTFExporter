# Getting Started

This guide walks you through installing **RTFExporter** and generating rich, formatted RTF documents in your application.

---

## Installation

Install the package via NuGet Package Manager or the .NET CLI:

### .NET CLI
```bash
dotnet add package RTFExporter
```

### Package Manager Console
```powershell
Install-Package RTFExporter
```

### PackageReference (csproj)
```xml
<PackageReference Include="RTFExporter" Version="1.1.4" />
```

---

## Approach 1: Automatic Resource Management (`IDisposable`)

The simplest way to create an RTF document and write it to disk is using the `IDisposable` pattern. When instantiated with a file path inside a `using` statement, `RTFDocument` automatically serializes and flushes content to disk when the block exits.

```csharp
using System;
using RTFExporter;

namespace DocumentExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Initialize document with output file path
            using (RTFDocument doc = new RTFDocument("ProjectReport.rtf"))
            {
                // Set document author metadata
                doc.author = "Jane Doe";

                // 2. Add a centered Title Paragraph
                RTFParagraph title = doc.AppendParagraph(Alignment.Center);
                title.style.spaceAfter = 300; // 300 twips spacing after title

                title.AppendText("Q3 Technical Report", new RTFTextStyle(
                    italic: false,
                    bold: true,
                    fontSize: 20,
                    fontFamily: "Arial",
                    color: Color.blue
                ));

                // 3. Add regular body paragraph with indentation
                RTFParagraph body = doc.AppendParagraph();
                body.style.indent = new Indent(firstLine: 0.5f, left: 0f, right: 0f);

                body.AppendText("During Q3, our system performance increased significantly. ");
                
                // Add inline formatted text to the same paragraph
                RTFText boldHighlight = body.AppendText("Latency decreased by 45 ms ");
                boldHighlight.style.bold = true;
                boldHighlight.style.color = Color.green;

                body.AppendText("across all edge nodes.");
            }
            
            Console.WriteLine("ProjectReport.rtf created successfully!");
        }
    }
}
```

---

## Approach 2: Exporting Directly to String

If you are developing web applications, API endpoints, or database exporters, you may want the generated RTF syntax directly as a C# `string` rather than saving to disk. Use `RTFParser.ToString(doc)` for in-memory generation:

```csharp
using RTFExporter;

public string GenerateInvoiceRtf(string customerName, decimal totalAmount)
{
    // Create an in-memory RTF document (no file path)
    RTFDocument doc = new RTFDocument();
    
    RTFParagraph header = doc.AppendParagraph(Alignment.Right);
    header.AppendText("INVOICE STATEMENT\n", new RTFTextStyle(false, true, 16, "Calibri", Color.black));
    
    RTFParagraph body = doc.AppendParagraph();
    body.AppendText($"Customer: {customerName}\n");
    
    RTFText total = body.AppendText($"Total Due: ${totalAmount:F2}");
    total.style.bold = true;
    total.style.color = new Color(180, 0, 0); // Custom RGB dark red

    // Serialize to raw RTF syntax payload
    return RTFParser.ToString(doc);
}
```

---

## Approach 3: Working with Custom Streams

You can bind `RTFDocument` directly to open `FileStream` or custom `Stream` wrappers:

```csharp
using System.IO;
using RTFExporter;

public void ExportToExistingStream(FileStream stream)
{
    using (RTFDocument doc = new RTFDocument(stream, width: 8.5f, height: 11f, orientation: Orientation.Portrait, units: Units.Inch))
    {
        RTFParagraph p = doc.AppendParagraph();
        p.AppendText("Direct stream write complete.");
    }
}
```

---

## Next Steps

Now that you have created your first document, dive into the [Styling & Formatting Guide](styling-guide.md) to discover advanced layout configurations, custom RGB colors, and typography settings.