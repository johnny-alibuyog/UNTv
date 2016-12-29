using HtmlAgilityPack;
using System;
using System.Linq;
using System.Net;

namespace UNTv.WP81.Common.Extentions
{
    public static class StringExtention
    {
        public static string ToTitleCase(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;

            var words = value.Trim().ToLower().Split(' ')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.ToProperCase())
                .ToList();

            return string.Join(" ", words);
        }

        public static string ToProperCase(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;

            var workingCopy = value.Trim().ToLower();
            var firstCharacter = char.ToUpper(workingCopy.First());
            var succedingCharacters = workingCopy.Substring(1);
            return firstCharacter + succedingCharacters;
        }

        public static bool EqualNoCase(this string value, string content)
        {
            if (value != null)
            {
                return value.Equals(content, StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                return false;
            }
        }

        public static string Truncate(this string value, int length, bool ellipsis = false)
        {
            if (!String.IsNullOrEmpty(value))
            {
                value = value.Trim();
                if (value.Length > length)
                {
                    if (ellipsis)
                    {
                        return value.Substring(0, length) + "...";
                    }
                    else
                    {
                        return value.Substring(0, length);
                    }
                }
            }
            return value ?? String.Empty;
        }

        public static string DecodeHtml(this string htmltext)
        {
            htmltext = htmltext.Replace("<p>", "").Replace("</p>", "\r\n\r\n");

            var decoded = String.Empty;

            if (htmltext.IndexOf('<') > -1 || htmltext.IndexOf('>') > -1 || htmltext.IndexOf('&') > -1)
            {
                try
                {
                    var document = new HtmlDocument();

                    var decode = document.CreateElement("div");
                    htmltext = htmltext.Replace(".<", ". <").Replace("?<", "? <").Replace("!<", "! <").Replace("&#039;", "'");
                    decode.InnerHtml = htmltext;

                    var allElements = decode.Descendants().ToArray();
                    for (int n = allElements.Length - 1; n >= 0; n--)
                    {
                        if (allElements[n].NodeType == HtmlNodeType.Comment || allElements[n].Name.EqualNoCase("style") || allElements[n].Name.EqualNoCase("script"))
                        {
                            allElements[n].Remove();
                        }
                    }
                    decoded = WebUtility.HtmlDecode(decode.InnerText);
                }
                catch { }
            }
            else
            {
                decoded = htmltext;
            }
            return decoded;
        }
    }
}
