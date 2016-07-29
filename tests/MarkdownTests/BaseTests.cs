using HeyRed.MarkdownSharp;
using Xunit;

namespace HeyRed.MarkdownSharpTests
{
    public class BaseTests
    {
        private Markdown _instance = new Markdown();

        [Fact]
        public void Bold()
        {
            string input = "This is **bold**. This is also __bold__.";
            string expected = "<p>This is <strong>bold</strong>. This is also <strong>bold</strong>.</p>";

            string actual = _instance.Transform(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Italic()
        {
            string input = "This is *italic*. This is also _italic_.";
            string expected = "<p>This is <em>italic</em>. This is also <em>italic</em>.</p>";

            string actual = _instance.Transform(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Blockquote()
        {
            string input = "Here is a quote\n\n> Sample blockquote\n";
            string expected = "<p>Here is a quote</p>\n\n<blockquote>\n  <p>Sample blockquote</p>\n</blockquote>";

            string actual = _instance.Transform(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BlockquoteSingleLine()
        {
            string input = "Here is a quote\n\n> Sample blockquote\n another text.";
            string expected = "<p>Here is a quote</p>\n\n<blockquote>\n  <p>Sample blockquote</p>\n</blockquote>\n\n<p>another text.</p>";

            _instance.QuoteSingleLine = true;
            string actual = _instance.Transform(input);
            _instance.QuoteSingleLine = false;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void NumberList()
        {
            string input = "A numbered list:\n\n1. a\n2. b\n3. c\n";
            string expected = "<p>A numbered list:</p>\n\n<ol>\n<li>a</li>\n<li>b</li>\n<li>c</li>\n</ol>";

            string actual = _instance.Transform(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BulletList()
        {
            string input = "A bulleted list:\n\n- a\n- b\n- c\n";
            string expected = "<p>A bulleted list:</p>\n\n<ul>\n<li>a</li>\n<li>b</li>\n<li>c</li>\n</ul>";

            string actual = _instance.Transform(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Header1()
        {
            string input = "#Header 1\nHeader 1\n========";
            string expected = "<h1>Header 1</h1>\n\n<h1>Header 1</h1>";

            string actual = _instance.Transform(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Header2()
        {
            string input = "##Header 2\nHeader 2\n--------";
            string expected = "<h2>Header 2</h2>\n\n<h2>Header 2</h2>";

            string actual = _instance.Transform(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CodeBlock()
        {
            string input = "code sample:\n\n    <head>\n    <title>page title</title>\n    </head>\n";
            string expected = "<p>code sample:</p>\n\n<pre><code>&lt;head&gt;\n&lt;title&gt;page title&lt;/title&gt;\n&lt;/head&gt;\n</code></pre>";

            string actual = _instance.Transform(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CodeBlockWithoutEncoding()
        {
            string input = "code sample:\n\n    <head>\n    <title>page title</title>\n    </head>\n";
            string expected = "<p>code sample:</p>\n\n<pre><code><head>\n<title>page title</title>\n</head>\n</code></pre>";

            _instance.DisableEncodeCodeBlock = true;
            string actual = _instance.Transform(input);
            _instance.DisableEncodeCodeBlock = false;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CodeInline()
        {
            string input = "HTML contains the `<blink>` tag";
            string expected = "<p>HTML contains the <code>&lt;blink&gt;</code> tag</p>";

            string actual = _instance.Transform(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void HorizontalRule()
        {
            string input = "* * *\n\n***\n\n*****\n\n- - -\n\n---------------------------------------\n\n";
            string expected = "<hr />\n\n<hr />\n\n<hr />\n\n<hr />\n\n<hr />";

            string actual = _instance.Transform(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void LineBreak()
        {
            string input = "Line break  \nNew line";
            string expected = "<p>Line break<br />\nNew line</p>";

            string actual = _instance.Transform(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void LineBreakWithAutoNewLines()
        {
            string input = "Line break\n New line";
            string expected = "<p>Line break<br />\n New line</p>";

            _instance.AutoNewLines = true;
            string actual = _instance.Transform(input);
            _instance.AutoNewLines = false;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void HtmlPassthrough()
        {
            string input = "<div>\nHello World!\n</div>\n";
            string expected = "<div>\nHello World!\n</div>";

            string actual = _instance.Transform(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Escaping()
        {
            string input = @"\`foo\`";
            string expected = "<p>`foo`</p>";

            string actual = _instance.Transform(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Link()
        {
            string input = "Have you visited [example](http://www.example.com) before?";
            string expected = "<p>Have you visited <a href=\"http://www.example.com\">example</a> before?</p>";

            string actual = _instance.Transform(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void LinkWithTitle()
        {
            string input = "Have you visited [example](http://www.example.com \"Title\") before?";
            string expected = "<p>Have you visited <a href=\"http://www.example.com\" title=\"Title\">example</a> before?</p>";

            string actual = _instance.Transform(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void LocalLink()
        {
            string input = "Have you visited [example](/example) before?";
            string expected = "<p>Have you visited <a href=\"/example\">example</a> before?</p>";

            string actual = _instance.Transform(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void LocalLinkWithTitle()
        {
            string input = "Have you visited [example](/example \"Title\") before?";
            string expected = "<p>Have you visited <a href=\"/example\" title=\"Title\">example</a> before?</p>";

            string actual = _instance.Transform(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void LinkWithoutDescription()
        {
            string input = "Have you visited [](http://www.example.com) before?";
            string expected = "<p>Have you visited <a href=\"http://www.example.com\"></a> before?</p>";

            _instance.AllowEmptyLinkText = true;
            string actual = _instance.Transform(input);
            _instance.AllowEmptyLinkText = false;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BracketLink()
        {
            string input = "Have you visited <http://www.example.com> before?";
            string expected = "<p>Have you visited <a href=\"http://www.example.com\">http://www.example.com</a> before?</p>";

            string actual = _instance.Transform(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ReferenceLink()
        {
            string input = "This is [a link][1].\n\n  [1]: http://www.example.com";
            string expected = "<p>This is <a href=\"http://www.example.com\">a link</a>.</p>";

            string actual = _instance.Transform(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ReferenceLinkWithTitle()
        {
            string input = "This is [a link][1].\n\n  [1]: http://www.example.com \"Title\"";
            string expected = "<p>This is <a href=\"http://www.example.com\" title=\"Title\">a link</a>.</p>";

            string actual = _instance.Transform(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void LinkWithoutAutoHyperLink()
        {
            string input = "Have you visited http://www.example.com before?";
            string expected = "<p>Have you visited http://www.example.com before?</p>";

            string actual = _instance.Transform(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void LinkWithAutoHyperLink()
        {
            string input = "Have you visited http://www.example.com before?";
            string expected = "<p>Have you visited <a href=\"http://www.example.com\">http://www.example.com</a> before?</p>";

            _instance.AutoHyperlink = true;
            string actual = _instance.Transform(input);
            _instance.AutoHyperlink = false;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Image()
        {
            string input = "An image goes here: ![alt text][1]\n\n  [1]: http://www.google.com/intl/en_ALL/images/logo.gif";
            string expected = "<p>An image goes here: <img src=\"http://www.google.com/intl/en_ALL/images/logo.gif\" alt=\"alt text\" /></p>";

            string actual = _instance.Transform(input);

            Assert.Equal(expected, actual);
        }
    }
}
