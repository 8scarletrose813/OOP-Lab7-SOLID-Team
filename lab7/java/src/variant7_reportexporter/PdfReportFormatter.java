package variant7_reportexporter;

import java.time.format.DateTimeFormatter;

public class PdfReportFormatter implements ReportFormatter{
    @Override
    public String formatReport(ReportData data) {
        StringBuilder builder = new StringBuilder();
        builder.append("%PDF-1.7 HEADER ");
        builder.append("TITLE: ").append(data.getTitle()).append(" ");
        builder.append("GENERATED: ").append(data.getGeneratedAt().format(DateTimeFormatter.ISO_LOCAL_DATE)).append(" ");
        builder.append("[PDF Table Stream Rendered with ").append(data.getRows().size()).append(" rows] ");
        return builder.toString();
    }
}
