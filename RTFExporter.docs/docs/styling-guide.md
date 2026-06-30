# Styling & Formatting Guide

**RTFExporter** provides comprehensive typographic and layout control. This guide covers page geometry, paragraph alignments, indentation, custom RGB colors, and character formatting.

---

## Page Geometry & Dimensions

When initializing an `RTFDocument`, you can configure physical page dimensions, measurement units, and orientation.

```csharp
// Create an A4 Landscape document measured in Millimeters
RTFDocument doc = new RTFDocument(
    path: "A4_Landscape.rtf",
    width: 297f,                  // 297 mm width
    height: 210f,                 // 210 mm height
    orientation: Orientation.Landscape,
    units: Units.Millimeters
);
```

### Document Margins
Adjust page margins at any time using `SetMargin(left, right, top, bottom)`. Values correspond to the document's active `Units`:

```csharp
// Set 0.75 inch margins on all sides
doc.SetMargin(0.75f, 0.75f, 0.75f, 0.75f);
```

---

## Paragraph Styling (`RTFParagraphStyle`)

Paragraphs support horizontal alignment, indentation structures, and vertical spacing above and below block boundaries.

### 1. Horizontal Alignment
Use the `Alignment` enum when creating or modifying paragraphs:

```csharp
doc.AppendParagraph(Alignment.Left);
doc.AppendParagraph(Alignment.Center);
doc.AppendParagraph(Alignment.Right);
doc.AppendParagraph(Alignment.Justified);
```

### 2. Paragraph Indentation (`Indent`)
Configure first-line indents (e.g., standard academic paragraph indents) or block indentation (e.g., blockquotes) using the `Indent` struct:

```csharp
// Indent struct: (firstLine, leftMargin, rightMargin)
Indent blockquoteIndent = new Indent(firstLine: 0f, left: 1.0f, right: 1.0f);

RTFParagraph quote = doc.AppendParagraph(Alignment.Left, blockquoteIndent);
quote.AppendText("\"Simplicity is prerequisite for reliability.\" - Edsger W. Dijkstra", new RTFTextStyle(
    italic: true, bold: false, fontSize: 11, fontFamily: "Georgia", color: Color.black
));
```

### 3. Vertical Spacing (`spaceBefore` & `spaceAfter`)
Control vertical separation between paragraphs using twips ($1/20$ of a point):

```csharp
RTFParagraph p = doc.AppendParagraph();
p.style.spaceBefore = 150; // Add 150 twips (~7.5pt) above paragraph
p.style.spaceAfter = 250;  // Add 250 twips (~12.5pt) below paragraph
```

---

## Text Styling & Colors (`RTFTextStyle`)

Character-level formatting is controlled via `RTFTextStyle` or fluent helper methods on `RTFText`.

### 1. Using RGB Colors
RTFExporter comes with predefined static colors on `Color`, or you can instantiate custom 24-bit RGB values:

```csharp
// Predefined colors
Color black = Color.black;
Color red = Color.red;
Color blue = Color.blue;

// Custom brand color (Hex #621EE5 -> R:98, G:30, B:229)
Color brandPurple = new Color(98, 30, 229);
```

### 2. Comprehensive Character Styles
The complete `RTFTextStyle` constructor lets you specify every supported RTF text decoration:

```csharp
RTFTextStyle fancyStyle = new RTFTextStyle(
    italic: true,
    bold: true,
    smallCaps: false,
    strikeThrough: false,
    allCaps: false,
    outline: false,
    fontSize: 14,          // 14pt typographical size
    fontFamily: "Verdana", // Font name
    color: brandPurple,
    underline: Underline.Double
);

p.AppendText("Formatted Text Segment", fancyStyle);
```

### 3. Underline Varieties
RTFExporter supports 8 distinct underline styles via the `Underline` enum:

| Enum Value | RTF Code | Description |
| :--- | :--- | :--- |
| `Underline.None` | `\ul0` | No underline |
| `Underline.Basic` | `\ul` | Single standard line |
| `Underline.Double` | `\uldb` | Double underline |
| `Underline.Thick` | `\ulth` | Thick underline |
| `Underline.WordsOnly` | `\ulw` | Underlines words, skips whitespace |
| `Underline.Wave` | `\ulwave` | Wavy underline (often used for spellcheck/warnings) |
| `Underline.Dotted` | `\uld` | Dotted line |
| `Underline.Dash` | `\uldash` | Dashed line |
| `Underline.DotDash` | `\uldashd` | Alternating dot and dash pattern |

---

## Fluent Style Chaining on `RTFText`

You can also use fluent `.SetStyle(...)` methods directly on `RTFText` instances:

```csharp
RTFParagraph p = doc.AppendParagraph();

// Fluent color, size, and font configuration
p.AppendText("Notice: ")
 .SetStyle(Color.red, fontSize: 13, fontFamily: "Arial");

// Fluent decoration configuration
p.AppendText("Please review terms and conditions carefully.")
 .SetStyle(italic: false, bold: true, underline: Underline.Wave);
```
