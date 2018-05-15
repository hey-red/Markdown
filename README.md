# This is legacy project. Use [Markdig](https://github.com/lunet-io/markdig) instead.

Markdown for C#
---------------
[![licence badge]][licence]
[![stars badge]][stars]
[![forks badge]][forks]
[![issues badge]][issues]

[licence badge]:https://img.shields.io/badge/license-MIT-blue.svg
[stars badge]:https://img.shields.io/github/stars/hey-red/Markdown.svg
[forks badge]:https://img.shields.io/github/forks/hey-red/Markdown.svg
[issues badge]:https://img.shields.io/github/issues/hey-red/Markdown.svg

[licence]:https://github.com/hey-red/Markdown/blob/master/LICENSE.md
[stars]:https://github.com/hey-red/Markdown/stargazers
[forks]:https://github.com/hey-red/Markdown/network
[issues]:https://github.com/hey-red/Markdown/issues

Open source C# implementation of Markdown processor, as featured on Stack Overflow.

This port is based heavily on the original Perl 1.0.1 and Perl 1.0.2b8 implementations of Markdown, with bits and pieces of the apparently much better maintained PHP Markdown folded into it. There are a few Stack Overflow specific modifications (which are all configurable, and all off by default). I'd like to ensure that this version stays within shouting distance of the Markdown "specification", such as it is...

## Install
via [NuGet](https://www.nuget.org/packages/Markdown):
```
PM> Install-Package Markdown
```

## Usage
```C#
using HeyRed.MarkdownSharp;

// Create new markdown instance
Markdown mark = new Markdown();

// Run parser
string text = mark.Transform(text);
```

## Options
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

## Extensions
```C#
using HeyRed.MarkdownSharp;
using YourMarkdownExtension.Extension;

Markdown mark = new Markdown();

mark.AddExtension(new Extension());

mark.Transform(text);
```

To create your own extensions you need to implement ```IMarkdownExtension```.

## Markdown Syntax
[Markdown: Syntax Daring Fireball](https://daringfireball.net/projects/markdown/syntax)

## License
[MIT](LICENSE)
