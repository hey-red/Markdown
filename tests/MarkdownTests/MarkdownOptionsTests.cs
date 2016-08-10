using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using HeyRed.MarkdownSharp;

namespace HeyRed.MarkdownSharpTests
{
    public class MarkdownOptionsTests
    {
        [Fact]
        public void EmailAddressMustBeSurroundedByAngleBracketsDefaultsToTrue()
        {
            var markdownOptions = new MarkdownOptions();
            Assert.Equal(markdownOptions.EmailAddressMustBeSurroundedByAngleBrackets, true);
        }
    }
}
