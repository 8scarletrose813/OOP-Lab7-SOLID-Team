package variant7_reportexporter;

public class JsonReportFormatter implements ReportFormatter{
    @Override
    public String formatReport(ReportData data) {
        StringBuilder builder = new StringBuilder();
        builder.append("{ ");
        builder.append("  \"title\": \"").append(data.getTitle()).append("\", ");
        builder.append("  \"generatedAt\": \"").append(data.getGeneratedAt()).append("\", ");
        builder.append("  \"totalRows\": ").append(data.getRows().size()).append(" ");
        builder.append("} ");
        return builder.toString();
    }
}
