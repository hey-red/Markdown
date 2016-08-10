using Xunit;
using HeyRed.MarkdownSharp;

namespace HeyRed.MarkdownSharpTests
{
    public class MarkdownConstructorTests
    {
        [Fact]
        public void EmailAddressMustBeSurroundedByAngleBracketsDefaultsToTrue()
        {
            var markdown = new Markdown();
            Assert.Equal(markdown.EmailAddressMustBeSurroundedByAngleBrackets, true);
        }
    }
}