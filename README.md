# RTF-Exporter
A C# library to generate .RTF text files from any object text data. No fancy dependencies or restrictive licenses.


---


## Simple usage


IDisposable style:


```C#
using RTFExporter;

public class Example {

    using (RTFDocument doc = new RTFDocument("example.rtf")) {
        var p = doc.AppendParagraph();
        var t = p.AppendText("Yo my vag is Godfather 1 and yo vag is Godfather 3");

        t.style.bold = true;
        t.style.color = new Color(255, 0, 0);
        t.style.fontFamily = "Courier";
    }

}
```


String style:


```C#
using RTFExporter;

public class Example {

    RTFDocument doc = new RTFDocument();
    RTFParagraph p = new RTFParagraph(doc);

    RTFText t1 = new RTFText(p, "Kitty on fleek, pretty on fleek\n");
    t.SetStyle(new Color(255, 0, 0), 18, "Helv");

    RTFText t2 = new RTFText(p, "Pretty gang always keep them niggas on geek");
    t2.style = t1.style;

    string output = RTFParser.ToString(doc);

}
```


---


## License  (WTFPL-2.0)


<a href="http://www.wtfpl.net/"><img src="http://www.wtfpl.net/wp-content/uploads/2012/12/wtfpl-badge-4.png" width="80" height="15" alt="WTFPL" /></a>
