using HeyRed.MarkdownSharp;
using Xunit;

namespace HeyRed.MarkdownSharpTests
{
    public class ImageTests
    {
        private Markdown _instance = new Markdown();

        [Fact]
        public void Image()
        {
            string input = "An image goes here: ![alt text](http://www.google.com/intl/en_ALL/images/logo.gif)";
            string expected = "<p>An image goes here: <img src=\"http://www.google.com/intl/en_ALL/images/logo.gif\" alt=\"alt text\" /></p>";

            string actual = _instance.Transform(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ImageWithTitle()
        {
            string input = "An image goes here: ![alt text](http://www.google.com/intl/en_ALL/images/logo.gif \"Title\")";
            string expected = "<p>An image goes here: <img src=\"http://www.google.com/intl/en_ALL/images/logo.gif\" alt=\"alt text\" title=\"Title\" /></p>";

            string actual = _instance.Transform(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void LocalImage()
        {
            string input = "An image goes here: ![alt text](/path/to/image.jpeg)";
            string expected = "<p>An image goes here: <img src=\"/path/to/image.jpeg\" alt=\"alt text\" /></p>";

            string actual = _instance.Transform(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void LocalImageWithTitle()
        {
            string input = "An image goes here: ![alt text](/path/to/image.jpeg \"Title\")";
            string expected = "<p>An image goes here: <img src=\"/path/to/image.jpeg\" alt=\"alt text\" title=\"Title\" /></p>";

            string actual = _instance.Transform(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ReferenceImage()
        {
            string input = "An image goes here: ![alt text][1]\n\n  [1]: http://www.google.com/intl/en_ALL/images/logo.gif";
            string expected = "<p>An image goes here: <img src=\"http://www.google.com/intl/en_ALL/images/logo.gif\" alt=\"alt text\" /></p>";

            string actual = _instance.Transform(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ReferenceImageWithTitle()
        {
            string input = "An image goes here: ![alt text](http://www.google.com/intl/en_ALL/images/logo.gif \"Title\")";
            string expected = "<p>An image goes here: <img src=\"http://www.google.com/intl/en_ALL/images/logo.gif\" alt=\"alt text\" title=\"Title\" /></p>";

            string actual = _instance.Transform(input);

            Assert.Equal(expected, actual);
        }
    }
}
