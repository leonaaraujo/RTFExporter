# Introduction & Concepts

Welcome to the conceptual documentation for **RTFExporter**. This guide explains the core ideas behind Rich Text Format (RTF), why it remains a crucial format in modern software development, and how RTFExporter abstracts the underlying syntax into a clean C# object hierarchy.

---

## What is Rich Text Format (RTF)?

Developed by Microsoft in 1987, **Rich Text Format (RTF)** is a universal file specification for document exchange across word processors and operating systems. Unlike plain text (`.txt`), RTF supports rich typographic styling, colors, font families, margins, and complex layout blocks.

Unlike modern OpenXML formats (`.docx`), which are zipped archives of complex XML schemas and directory hierarchies, **RTF files are plain 7-bit ASCII text streams** formatted with control words and symbols. For example, a minimal RTF document looks like this under the hood:

```rtf
{\rtf1\ansi\deff0
{\fonttbl{\f0 Calibri;}}
{\colortbl;\red0\green0\blue0;\red255\green0\blue0;}
\paperw12240\paperh15840\margl1440\margr1440
\pard\f0\fs24 This is standard black text. \cf2\b This text is bold and red!\b0\cf1\par
}
```

### Advantages of RTF Generation
1. **Universal Compatibility**: Every major operating system opens RTF files out of the box (Microsoft WordPad, MS Word, macOS TextEdit, LibreOffice Writer, Google Docs).
2. **Speed & Efficiency**: Because RTF is a continuous stream of plain text control codes, generating it in memory is orders of magnitude faster than building zip archives or rendering PDF layout engines.
3. **Streamable Payloads**: Ideal for serverless endpoints, email attachments, and web APIs where lightweight string generation is critical.

---

## Architectural Overview of RTFExporter

**RTFExporter** bridges object-oriented C# concepts directly to RTF control word generation without requiring third-party layout engines or COM interop dependencies.

The library is organized around three primary domain entities:

```
+-------------------------------------------------------------+
|                         RTFDocument                         |
|  (Manages Color Table, Font Table, Page Setup, Metadata)    |
+-------------------------------------------------------------+
                               |
                               | 1 : N
                               v
+-------------------------------------------------------------+
|                        RTFParagraph                         |
|  (Manages Indentation, Alignment, Spacing before/after)     |
+-------------------------------------------------------------+
                               |
                               | 1 : N
                               v
+-------------------------------------------------------------+
|                           RTFText                           |
|  (Manages Raw Content string and Character Styling options) |
+-------------------------------------------------------------+
```

### 1. `RTFDocument` (Root Container)
The `RTFDocument` class represents the entire physical document. It maintains:
- **Global Tables**: Automatically tracks every distinct `Color` and `fontFamily` used in child elements and compiles them into the mandatory `\colortbl` and `\fonttbl` headers upon export.
- **Page Geometry**: Controls physical dimensions (`width`, `height`), `Orientation` (`Portrait` or `Landscape`), measurement `Units` (`Inch`, `Millimeters`, `Centimeters`), and margins.
- **Resource Management**: Implements `IDisposable`. When instantiated with a file path or stream inside a `using` block, disposing the document automatically invokes `RTFParser.ToString(this)` and flushes the output stream.

### 2. `RTFParagraph` (Structural Block)
In RTF, paragraphs are separated by the `\par` control word. Every paragraph belongs to an `RTFDocument` and contains a sequential list of `RTFText` runs.
- Paragraphs hold an `RTFParagraphStyle` object specifying horizontal alignment (`\ql`, `\qr`, `\qc`, `\qj`), first-line indent (`\fi`), block margins (`\li`, `\ri`), and vertical twip spacing (`\sb`, `\sa`).

### 3. `RTFText` (Formatted Character Segment)
Any piece of text within a paragraph is represented by `RTFText`. Multiple text runs within the same paragraph allow you to change styles on the fly without breaking the paragraph line.
- Each `RTFText` segment carries an `RTFTextStyle` object detailing typography points (`fontSize`), font family names, RGB colors, bold/italic toggles, and underline decorations.

---

## Understanding Measurement Units and Twips

Internally, word processors and RTF specifications measure vertical spacing and font sizes in specialized typographical units:
- **Twip**: A twentieth of a typographical point ($1/1440$ of an inch). When you configure paragraph vertical spacing (`spaceBefore` or `spaceAfter`), the integer values represent twips (e.g., `200` twips $\approx 10$ points of vertical space).
- **Points**: Font sizes (`fontSize`) in `RTFTextStyle` are specified in standard typographical points (e.g., `12` for 12pt text). RTFExporter automatically scales these during parsing into half-points (`\fs24`) as required by the RTF specification.

---

## Next Steps
Now that you understand the concepts behind RTFExporter, proceed to the [Getting Started Guide](getting-started.md) to start building your first document.