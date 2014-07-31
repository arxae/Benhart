Benhart
=======
Benhart is a logwindow that can be attached to any WPF window (for now). It was inspired by the PCSX2 logwindow.

Initializing the window is easy

``` csharp
using BenhartLog;

Benhart.AttachTo(Window); // To attach to a WPF window
// or
Benhart.OpenAt(int left, int top); // To open at a specific location
``` 

This will open up a Benhart log window. Writing to it is easy as well.

``` csharp
Benhart.Log(LogLevel, text); // Write using a specific severity
Benhart.Log(text, LogStyle); // Write using a custom log style
// Or any of the shorthand methods
Benhart.Message(text);
Benhart.Debug(text);
Benhart.Info(text);
Benhart.Warning(text);
Benhart.Error(text);
``` 

You can also add values to the watch window by using
``` csharp
Benhart.SetWatch(name, value);
```
Updating the value can be done by the same method, just use the same name

Using the BenhartMarkdown class, you can output markdown. This markdown can be converted to HTML with your favorite markdown parser.
In every entry, title is optional but will default to what is outputed (eg: "List: " for lists)
``` csharp
BenhartMarkdown.Table(LogLevel, IEnumerable, title)
BenhartMarkdown.NumberedList(LogLevel, IEnumerable, title)
BenhartMarkdown.BulletList(LogLevel, IEnumerable, title)
BenhartMarkdown.Paragraph(LogLevel, text, title)
```
If you just want to copy/paste the output for HTML, then you should keep in mind the time output. Normally, Benhart outputs the time with each call, this is not Markdown and will not be parsed. To circumvent this, manually remove the time entries or turn of time output completely by setting Benhart.ShowTime to false.	

The display and window can be configured using these properties:
``` csharp
Benhart.Visbility
Benhart.ShowInTaskbar
Benhart.Left
Benhart.Top
Benhart.Width
Benhart.Height
Benhart.ShowTime
Benhart.TimeFormat
Benhart.TimeMessageSepparator
Benhart.LogWindowHeader
``` 