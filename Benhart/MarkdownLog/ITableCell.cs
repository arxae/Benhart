namespace BenhartLog.MarkdownLog
{
    public interface ITableCell
    {
        int RequiredWidth { get; }
        string BuildCodeFormattedString(TableCellRenderSpecification spec);
    }
}