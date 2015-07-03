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
    /// ex: http://myanimelist.net/people/413/Kitamura_Eri => mal://Kitamura_Eri
    /// </summary>
    public class Articles : IExtensionInterface
    {
        private static Regex _malArticles = new Regex(@"
                    (?:http\:\/\/)
                    (?:www\.)?
                    myanimelist\.net\/
                    (anime|manga|character|people)\/
                    ([\d]+)\/
                    ([^\^\s\<\>]+)", RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);


        public string Transform(string text)
        {
            return _malArticles.Replace(text, new MatchEvaluator(ArticleEvaluator));
        }


        private string ArticleEvaluator(Match match)
        {
            string categories = match.Groups[1].Value;                  // people|manga..
            string num = match.Groups[2].Value;                         // num section
            string title = WebUtility.UrlDecode(match.Groups[3].Value); // title e.g Kitamura_Eri

            return String.Format(
                "[mal://{0}](http://myanimelist.net/{1}/{2}/{3})", 
                title, categories, num, title
            );
        }
    }
}
