package variant7_reportexporter;

public class ExcelReportFormatter implements ReportFormatter{
    @Override
    public String formatReport(ReportData data) {
        StringBuilder builder = new StringBuilder();
        builder.append("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"> ");
        builder.append("  <Worksheet ss:Name=\"").append(data.getTitle()).append("\"> ");
        builder.append("<!-- Rendered ").append(data.getRows().size()).append(" rows --> ");
        builder.append("  </Worksheet> ");
        builder.append("</Workbook> ");
        return builder.toString();
    }
}
