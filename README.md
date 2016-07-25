# Markdown

Open source C# implementation of Markdown processor, as featured on Stack Overflow.

This port is based heavily on the original Perl 1.0.1 and Perl 1.0.2b8 implementations of Markdown, with bits and pieces of the apparently much better maintained PHP Markdown folded into it. There are a few Stack Overflow specific modifications (which are all configurable, and all off by default). I'd like to ensure that this version stays within shouting distance of the Markdown "specification", such as it is...

# Install
```
PM> Install-Package Markdown
```

# Usage
```C#
using HeyRed.MarkdownSharp;

// Create new markdown instance
Markdown mark = new Markdown();

// Run parser
string text = mark.Transform(text);
```

# Options
```C#
var options = new MarkdownOptions 
{
    AutoHyperlink = true,
    AutoNewlines = true,
    LinkEmails = true,
    QuoteSingleLine = true,
    StrictBoldItalic = true
}

Markdown mark = new Markdown(options);
mark.Transform(text);
```
See more options and docs [in MarkdownOptions](src/Markdown/MarkdownOptions.cs)

# Extensions
```C#
using HeyRed.MarkdownSharp;
using YourMarkdownExtension.Extension;

Markdown mark = new Markdown();

mark.AddExtension(new Extension());

mark.Transform(text);
```

To create your own extensions you need to implement ```IMarkdownExtension```.
