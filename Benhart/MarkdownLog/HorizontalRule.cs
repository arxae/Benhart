using System;

namespace BenhartLog.MarkdownLog
{
    public class HorizontalRule : MarkdownElement
    {
        public override string ToMarkdown()
        {
            return new string('-', 80) + Environment.NewLine;
        }
    }
}