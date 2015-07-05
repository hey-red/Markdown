/**
 * This file is part of the MarkdownSharp package
 * For the full copyright and license information,
 * view the LICENSE file that was distributed with this source code.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;


namespace MarkdownSharp.Extensions.Youtube
{
    /// <summary>
    /// Add title to youtube link
    /// </summary>
    public class LinkToTitle : IExtensionInterface
    {
        /// <summary>
        /// Array of links: videoID/title
        /// </summary>
        private string[,] _links;

        /// <summary>
        /// Google api key
        /// </summary>
        private string _apiKey;
        private int _maxLinks;

        private static Regex _youtubeLink = new Regex(@"
                    (?:https?\:\/\/)
                    (?:www\.)?
                    (?:youtu\.be|youtube\.com)\/
                    (?:embed\/|v\/|watch\?v=)?
                    ([\w\-]{10,12})", RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);


        /// <summary>
        /// FiXME: max ids?
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="maxLinks"></param>
        public LinkToTitle(string apiKey, int maxLinks = 10)
        {
            if (maxLinks < 1)
            {
                throw new ArgumentException("Max links should be equal or greater than 1.");
            }
            _apiKey = apiKey;
            _maxLinks = maxLinks;
            _links = new string[_maxLinks, 2];
        }


        public string Transform(string text)
        {
            int linksCount = 0;
            foreach (Match match in _youtubeLink.Matches(text))
            {
                if (linksCount == _maxLinks) break;
                if (videoIdAlreadyExist(match.Groups[1].Value)) continue;

                // Set video id
                _links[linksCount, 0] = match.Groups[1].Value;
                linksCount++;
            }

            if (linksCount > 0) {
                string ids = "";
                for (int i = 0; i < linksCount; i++)
                {
                    ids += (i == linksCount-1) ? 
                        _links[i, 0]:
                        _links[i, 0] + ",";
                }

                // Load titles
                string res = RequestToGoogleApi(ids);
                if (!String.IsNullOrEmpty(res))
                {
                    ParseApiResponse(res);
                }
            }

            return _youtubeLink.Replace(text, new MatchEvaluator(LinkEvaluator));
        }


        private bool videoIdAlreadyExist(string id)
        {
            for (int i = 0; i < _maxLinks; i++)
            {
                if (_links[i, 0] == id) return true;
            }
            return false;
        }


        /// <summary>
        /// Get videos titles from youtube API
        /// More info: https://developers.google.com/youtube/v3/
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>Return null string if request failed</returns>
        private string RequestToGoogleApi(string ids) {
            string res = null;
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    res = client.DownloadString(
                        String.Format(
                            "https://www.googleapis.com/youtube/v3/videos?id={0}&key={1}"+
                            "&part=snippet&fields=items(id,snippet(title))",
                            ids, _apiKey
                        )
                    ); 
                }
            }
            catch {}
            return res;
        }


        /// <summary>
        /// Parse API JSON response
        /// </summary>
        /// <param name="res"></param>
        private void ParseApiResponse(string res) {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var json = serializer.Deserialize<Dictionary<string, object>>(res);

            foreach (Dictionary<string, object> item in (ArrayList)json["items"])
            {
                for (int i = 0; i < _maxLinks; i++)
                {
                    if (_links[i, 0] == (string)item["id"])
                    {
                        var snippet = (Dictionary<string, object>)item["snippet"];
                        // Set video title
                        _links[i, 1] = (string)snippet["title"];
                    }
                }
            }
        }


        private string LinkEvaluator(Match match)
        {
            for (int x = 0; x < _maxLinks; x++)
            {
                if (match.Groups[1].Value == _links[x, 0] && 
                    _links[x, 1] != null)
                {
                    return String.Format(
                        "[YouTube: {0}](https://youtube.com/watch?v={1})",
                        _links[x, 1], _links[x, 0]
                    );
                }
            }
            return match.Groups[0].Value;
        }
    }
}
