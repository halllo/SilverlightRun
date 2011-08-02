
namespace SilverlightRun.Util.Web
{
    /// <summary>
    /// Provides string normalization functionality.
    /// </summary>
    public static class WebString
    {
        public static string Cleanse(this string desc)
        {
            var exp = ContentScraper.For(desc)
                .Replace("<.*?>", "")
                .Replace("\r", "")
                .Replace("\n", "")
                .Replace("&quot;", "\"")
                .Replace("&nbsp;", "")
                .Until("<");
            return exp.Result.Trim();
        }

        public static string NormWhiteSpaces(this string desc)
        {
            string normedDesc = desc;
            while (normedDesc.Contains("  ")) normedDesc = normedDesc.Replace("  ", " ");
            return normedDesc.Trim();
        }
    }
}
