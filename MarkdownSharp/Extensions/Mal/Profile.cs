/**
 * This file is part of the MarkdownSharp package
 * For the full copyright and license information,
 * view the LICENSE file that was distributed with this source code.
 */

using System;
using System.Net;
using System.Text.RegularExpressions;


namespace MarkdownSharp.Extensions.Mal
{
    /// <summary>
    /// Create short link for http://myanimelist.net
    /// ex: http://myanimelist.net/profile/ritsufag => mal://ritsufag
    /// </summary>
    public class Profile : IExtensionInterface
    {
        private static Regex _malArticles = new Regex(@"
                    (?:http\:\/\/)
                    (?:www\.)?
                    myanimelist\.net\/profile\/
                    ([\w-]{2,16})", RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);


        public string Transform(string text)
        {
            return _malArticles.Replace(text, new MatchEvaluator(ProfileEvaluator));
        }


        private string ProfileEvaluator(Match match)
        {
            string userName = match.Groups[1].Value;
            return String.Format(
                "[mal://{0}](http://myanimelist.net/profile/{1})",
                userName, userName
            );
        }
    }
}
