package variant7_reportexporter;

public class MarkdownReportFormatter implements ReportFormatter{
    @Override
    public String formatReport(ReportData data) {
        StringBuilder builder = new StringBuilder();
        builder.append("# ").append(data.getTitle()).append(" ");
        builder.append("**Generated:** ").append(data.getGeneratedAt()).append(" ");
        builder.append("**Total Rows:** ").append(data.getRows().size());
        return builder.toString();
    }
}