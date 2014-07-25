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