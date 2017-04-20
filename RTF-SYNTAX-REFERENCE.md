#### IF YOU WANNA GOING DEEP I RECOMMEND THOSE LINKS:


[https://github.com/jokecamp/RtfWebVisualizer](https://github.com/jokecamp/RtfWebVisualizer)  
[http://www.pindari.com/rtf1.html](http://www.pindari.com/rtf1.html)  
[http://www.biblioscape.com/rtf15_spec.htm](http://www.biblioscape.com/rtf15_spec.htm)
[https://msdn.microsoft.com/en-us/library/office/aa140302](https://msdn.microsoft.com/en-us/library/office/aa140302)


---


#### INITIALIZING THE FILE


{\rtf\ansi\deff0


means:


rtf -> rich text format  
ansi -> using ANSI  
deff0 -> define font: 0  


---


#### SET DOCUMENT SIZE AND ORIENTATION


\landscape  
\paperw15840\paperh12240  


\portrait  
\paperw12240\paperh15840  


means:


paperw -> paper width  
paperh -> paper height  


notes:
- the size 15840 is equal to 11 inchs and 12240 to 8,5 inchs (letter size)
- use only integer values


---


#### SET MARGINS


\margl720\margr720\margt720\margb720


means:


margl -> margin left  
margr -> margin right  
margt -> margin top  
margb -> margin bottom  


notes:
- the size 720 is equal to 0,5 inch
- use only integer values


---


#### SET DOCUMENT COLORS


{\colortbl;  
\red255\green0\blue0;  
\red0\green255\blue0;  
}  


means:


colortbl -> color table  
red255\green0\blue0 -> rgb color byte value  


how to use colors:


\cf1  
Text  
\cf2Text  


cf1 -> all text after this will use the color 1 of the table as foreground  
cf2 -> all text after this will use the color 2 of the table as foreground  


notes:
- the default is black
- could be declared just one time, only the first works
- always start in 1, cf0 doesn't exists
- use only integer values


---


#### SET DOCUMENT FONTS


{\fonttbl  
{\f0 Courier;}  
{\f1 Arial;}  
{\f2 Helv;}  
{\f3 Tms Rmn;}  
{\f4 Verdana;}  
{\f5 Symbol;}  
}  


means:


fonttbl -> font table  
f0 -> font 0  


notes:
- the default is Calibri
- you can use \fN to set the text font
- this table could be declared several times in the file, the text uses the last declaration values
- if font doesn't exist and is not setted, will use Calibri with a fake name
- the default font always will be \f0, if start the table with other number it will use Calibri, if no one is setted
- if you use a declaration like: "\f0 \f1" the text will use the last one ever if this font doesn't exists


---


#### BREAKLINE


\line  


note:
- same as the escape sequence \n in C
- if don't have a space between \line and the text the command is ignored


---


#### BREAKPAGE


\page  


note:
- if don't have a space between \page and the text the command is ignored


---


#### TAB


\tab  
\tx1440 \tab  


means:


tx -> tab x size


note:
- same as the escape sequence \t in C
- if don't have a space between \tab and the text the command is ignored
- \tx1440 is equal to 1 inch and 24,5 mm or 2,45 cm
- \tx can be used in a row to define differents sizes of tab. Ex.: \tx720 \tx1440 \tx720 \tab \tab \tab
- \tx row can declared just one time, the others will be ignored
- use only integer values


---


#### FONTSIZE


\fs20


means:


fs20 -> font size 20


note:
- \fs20 in points is 10pt
- use only integer values


---


#### PARAGRAPH


\par


means:


par -> end of a paragraph  


note:
- if don't have a space between \par and the text the command is ignored


---


#### TEXT STYLE


\i -> italic on  
\i0 -> italic off  
\b -> bold on  
\b0 -> bold off  
\scaps -> small caps on  
\scaps0 -> small caps off  
\strike -> strike through on  
\strike0 -> strike through off  
\caps -> capitals on  
\caps0 -> all capitals off  
\outl -> outline on  
\outl0 -> outline off  
\ul -> Underline on  
\uldb -> Double Underline on  
\ulth -> Thick Underline on  
\ulw -> Underline words only on  
\ulwave -> Wave Underline on  
\uld -> Dotted Underline on  
\uldash -> Dash Underline on  
\uldashd -> Dot Dash Underline on  
\ul0 -> Any underline off  


---


#### ALIGNMENT


\ql	-> left  
\qr -> right  
\qc -> centered  
\qj -> justified  


---


#### RESET VALUES


\pard -> reset tables configuration (\tx)  
\plain -> reset text size, style and font family to default  


---


#### FILE INFO


{\info  
{\author John Doe} <- author name  
{\creatim\yr1990\mo7\dy30\hr10\min48} <- time stamp  
}  
