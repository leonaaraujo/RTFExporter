# RTFExporter


[![NuGet Version](https://img.shields.io/nuget/v/RTFExporter)][nuget]
![C# Version](https://img.shields.io/badge/C%23-4.0-621ee5)
![Framework Version](https://img.shields.io/badge/framework-netstandard2.0-621ee5)
![.NETFramework Version](https://img.shields.io/badge/.NET_Framework-4.6.1-621ee5)


A C# library to generate .RTF text files from any string object data, stylized and ready for any text processor. No fancy dependencies or restrictive licenses.


---


## Simple usage


IDisposable style:


```C#
using RTFExporter;

public class Example {

    public void IDisposableExample() {

        using (RTFDocument doc = new RTFDocument("example.rtf")) {
            var p = doc.AppendParagraph();

            p.style.alignment = Alignment.Center;
            p.style.indent = new Indent(1, 0, 0);
            p.style.spaceAfter = 400;

            var t = p.AppendText("One: Don't pick up the phone\n");
            t.content += "You know he's only callin' 'cause he's drunk and alone";

            t.style.bold = true;
            t.style.color = new Color(255, 0, 0);
            t.style.fontFamily = "Courier";
        }

    }

}
```


String style:


```C#
using RTFExporter;

public class Example {

    public void StringExample() {

        RTFDocument doc = new RTFDocument();
        RTFParagraph p = new RTFParagraph(doc);

        RTFText t1 = new RTFText(p, "Two: Don't let him in\n");
        t1.SetStyle(new Color(255, 0, 0), 18, "Helv");

        RTFText t2 = new RTFText(p, "You'll have to kick him out again");
        t2.style = t1.style;

        string output = RTFParser.ToString(doc);

    }

}
```


---


## Features


- Document
    - Set page size
    - Set orientation
    - Set margin
    - Set units (inch, mm, cm)


- Paragraph style
    - Set indent
    - Set text alignment
    - Set spacing


- Text style
    - Set color
    - Set font family
    - Set font size
    - Set style: bold, italic, small caps, all caps, strike through and outline
    - Set 8 different types of underline


---


## Missing

- Parse a rtf file to RTFDocument object
- Support to non-latin characters
- Use stylesheets
- Lists
- And a lot more of RTF stuff


---


## License  (WTFPL-2.0)


[![WTFPL Badge](http://www.wtfpl.net/wp-content/uploads/2012/12/wtfpl-badge-4.png)](http://www.wtfpl.net/)

[nuget]: https://www.nuget.org/packages/RTFExporter
