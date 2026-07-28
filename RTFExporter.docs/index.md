---
_layout: landing
---

# RTFExporter

**A lightweight, zero-dependency C# library for generating styled .RTF documents from code.**

[![NuGet Version](https://img.shields.io/nuget/v/RTFExporter.svg)](https://www.nuget.org/packages/RTFExporter)
![Framework](https://img.shields.io/badge/Framework-.NET%20Standard%202.0-blue.svg)
![License](https://img.shields.io/badge/License-WTFPL%202.0-green.svg)

---

## Why RTFExporter?

Generating formatted word processor documents programmatically often forces developers into bloated dependencies, complex OpenXML wrappers, or restrictive commercial licensing. **RTFExporter** solves this by providing an intuitive, object-oriented C# API that builds **Rich Text Format (RTF 1.5)** files with zero external dependencies.

Whether you need to generate automated business reports, export text logs with syntax highlighting, or build downloadable documents in ASP.NET Core microservices, RTFExporter delivers maximum compatibility and performance.

---

## Key Features

- **Zero Dependencies**: Pure C# implementation targeting `.NET Standard 2.0` (compatible with .NET Framework 4.6.1+, .NET Core 2.0+, and modern .NET 5/6/8/10).
- **IDisposable Resource Safety**: Seamlessly manage file streams with C# `using` blocks that automatically serialize and flush on disposal.
- **Rich Page Setup**: Configure page dimensions, portrait or landscape orientation, custom margins, and measurement units (`Inches`, `Millimeters`, or `Centimeters`).
- **Comprehensive Paragraph Styling**: Precise control over horizontal alignments (`Left`, `Center`, `Right`, `Justified`), multi-level indentation (`First Line`, `Left`, `Right`), and vertical paragraph spacing.
- **Full Typography & Color Support**: Manage RGB font colors, font families, font sizes, and character decorations (`Bold`, `Italic`, `Small Caps`, `All Caps`, `Strikethrough`, `Outline`, and **8 Underline styles**).
- **Flexible Export Targets**: Save directly to disk paths, write to existing `Stream` objects, or export directly to string payloads for web APIs.

---

## Quick Example

```csharp
using RTFExporter;

// Create a new RTF document saved directly to disk
using (RTFDocument doc = new RTFDocument("Report.rtf"))
{
    // Append a centered title paragraph
    RTFParagraph titlePar = doc.AppendParagraph(Alignment.Center);
    titlePar.AppendText("Monthly Executive Summary", new RTFTextStyle(
        italic: false, 
        bold: true, 
        fontSize: 18, 
        fontFamily: "Calibri", 
        color: Color.Blue
    ));

    // Append body content with indentation and spacing
    RTFParagraph bodyPar = doc.AppendParagraph(new Indent(firstLine: 0.5f, left: 0f, right: 0f));
    bodyPar.Style.SpaceBefore = 200; // 200 twips vertical spacing
    
    bodyPar.AppendText("Total revenue increased by 14.2% compared to last quarter. ");
    
    RTFText highlight = bodyPar.AppendText("Action required immediately.");
    highlight.Style.Color = Color.Red;
    highlight.Style.Underline = Underline.Wave;
}
```

---

## Next Steps

Explore the documentation to master document generation:

* [**Conceptual Introduction**](docs/introduction.md) — Learn how RTF syntax works and RTFExporter's architectural design.
* [**Getting Started Guide**](docs/getting-started.md) — Step-by-step instructions for installation and creating your first documents.
* [**Styling & Formatting Guide**](docs/styling-guide.md) — Master page margins, paragraph indentation, colors, and typography.
* [**API Reference**](xref:RTFExporter) — Detailed class and method documentation.