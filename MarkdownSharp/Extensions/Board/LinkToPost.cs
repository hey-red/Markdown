/**
 * This file is part of the MarkdownSharp package
 * For the full copyright and license information,
 * view the LICENSE file that was distributed with this source code.
 */

using System;
using System.Text.RegularExpressions;


namespace MarkdownSharp.Extensions.Board
{
    /// <summary>
    /// Create link to post id
    /// Supported format: 
    /// >>1 or 
    /// >>1|2 (link to file in post)
    /// </summary>
    public class LinkToPost : IExtensionInterface
    {
        private static Regex _postLinks = new Regex(@"
                    (?:>>|&gt;&gt;)
                    (\d+)
                    \|?(\d)?", RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);


        public string Transform(string text)
        {
            return _postLinks.Replace(text, new MatchEvaluator(LinkEvaluator));
        }


        private string LinkEvaluator(Match match)
        {
            string postID = match.Groups[1].Value;
            string fileID = match.Groups[2].Value;

            if (!String.IsNullOrEmpty(fileID))
            {
                return String.Format("[>>{0}|{1}](/{0}#f{1})", postID, fileID);
            }
            return String.Format("[>>{0}](/{0})", postID, postID);
        }
    }
}
