using HeyRed.MarkdownSharp;
using Xunit;

namespace HeyRed.MarkdownSharpTests
{
    public class LinkTests
    {
        private Markdown _instance = new Markdown();

        [Fact]
        public void Link()
        {
            string input = "Have you visited [example](http://www.example.com) before?";
            string expected = "<p>Have you visited <a href=\"http://www.example.com\">example</a> before?</p>";

            string actual = _instance.Transform(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void LinkWithTargetBlank()
        {
            string input = "Have you visited [example](http://www.example.com)+ before?";
            string expected = "<p>Have you visited <a href=\"http://www.example.com\" target=\"_blank\">example</a> before?</p>";

            _instance.AllowTargetBlank = true;
            string actual = _instance.Transform(input);
            _instance.AllowTargetBlank = false;

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
        public void ReferenceLinkWithoutShortcut()
        {
            string input = "This is [a link][].\n\n  [a link]: http://www.example.com";
            string expected = "<p>This is <a href=\"http://www.example.com\">a link</a>.</p>";

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
        public void ProtocolRelativeLink()
        {
            string input = "Have you visited [example](//www.example.com) before?";
            string expected = "<p>Have you visited <a href=\"//www.example.com\">example</a> before?</p>";

            string actual = _instance.Transform(input);

            Assert.Equal(expected, actual);
        }
    }
}
