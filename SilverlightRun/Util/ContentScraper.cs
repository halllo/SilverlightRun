using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SilverlightRun.Util
{
    /// <summary>
    /// Basic content parsing.
    /// </summary>
    public class ContentScraper
    {
        public static ContentScraper For(string expression)
        {
            return new ContentScraper(expression);
        }

        private ContentScraper(string expression)
        {
            this._Expression = expression;
            this.IgnoreCase = true;
        }

        private string _Expression;
        private string Expression { get { return this._Expression; } }
        public bool IgnoreCase { private get; set; }

        private RegexOptions GetOptions()
        {
            return this.IgnoreCase ? RegexOptions.IgnoreCase : RegexOptions.None;
        }

        #region Operating
        public IEnumerable<ContentScraper> Split(string pattern)
        {
            return from s in Regex.Split(this.Expression, pattern, this.GetOptions())
                   select new ContentScraper(s);
        }

        public ContentScraper SplitOut(string pattern, Func<IEnumerable<ContentScraper>, int> indexer)
        {
            var splitted = this.Split(pattern);
            return splitted.Skip(indexer(splitted)).First();
        }

        public ContentScraper SplitOut(string pattern, int index)
        {
            return SplitOut(pattern, (s) => index);
        }

        public bool Matches(string pattern)
        {
            return Regex.IsMatch(this.Expression, pattern, this.GetOptions());
        }

        public ContentScraper Replace(string pattern, string replacement)
        {
            return new ContentScraper(Regex.Replace(this.Expression, pattern, replacement, this.GetOptions()));
        }
        #endregion

        #region Resulting
        public string Result
        {
            get { return this.Expression; }
        }

        public ContentScraper Until(int index)
        {
            if (index < 0) index = this.Expression.Length;
            return new ContentScraper(this.Expression.Substring(0, index));
        }

        public ContentScraper Until(string p)
        {
            return Until(this.Expression.IndexOf(p));
        }

        public ContentScraper UntilLast(string p)
        {
            return Until(this.Expression.LastIndexOf(p));
        }

        public ContentScraper UntilEarliest(string p1, string p2)
        {
            int untilIndex = 0;
            if (this.Expression.Contains(p1) && this.Expression.Contains(p2))
                untilIndex = Math.Min(this.Expression.IndexOf(p1), this.Expression.IndexOf(p2));
            else
                untilIndex = Math.Max(this.Expression.IndexOf(p1), this.Expression.IndexOf(p2));
            return new ContentScraper(this.Expression.Substring(0, untilIndex));
        }

        public ContentScraper StartingAt(string p)
        {
            return StartingAt(this.Expression.IndexOf(p));
        }

        public ContentScraper StartingAtLast(string p)
        {
            return StartingAt(this.Expression.LastIndexOf(p));
        }

        public ContentScraper StartingAt(int p)
        {
            return new ContentScraper(this.Expression.Substring(p));
        }

        public ContentScraper StartingAfter(string p)
        {
            return this.StartingAt(p).StartingAt(p.Length);
        }

        public ContentScraper ShortenTo(int p)
        {
            return new ContentScraper(this.Expression.Substring(0, p));
        }
        #endregion
    }
}
